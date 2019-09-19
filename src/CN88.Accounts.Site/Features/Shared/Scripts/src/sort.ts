class SortOption {
    constructor(
        public sortContainer = null,
        public canSort = true,
        public hasAllDataOnClient = false,
        public afterSortFunction = null,
        public headers = null,
        public reportForm = null) {
    }
}

class SortComponent {
    $: JQueryStatic;
    $sortContainer: JQuery;
    linkBuilderComponent: LinkBuilderComponent;
    $reportForm: JQuery;

    options = new SortOption();

    constructor(
        $: JQueryStatic,
        linkBuilderComponent: LinkBuilderComponent) {
        this.$ = $;
        this.linkBuilderComponent = linkBuilderComponent;
    }

    render = (options: SortOption = new SortOption()) => {
        this.options = this.$.extend({}, this.options, options) as SortOption;
        this.$sortContainer = this.options.sortContainer;
        this.$reportForm = this.options.reportForm;

        const sortColumns = this.$sortContainer.find("[data-sortedname]");

        if (!this.options.canSort) {
            sortColumns.removeAttr("data-sortedname").removeClass("sorting");
            return;
        }

        if (!this.options.hasAllDataOnClient) {
            this.$sortContainer
                .find(`[data-sortedname='${this.$sortContainer.data("sortedcolumn")}']`)
                .addClass(this.getSortClassWhenSortOnServer());

            sortColumns
                .off("click.sortcomponent")
                .on("click.sortcomponent", (e: JQueryEventObject) => {
                    const $sortColumn = this.$(e.target);

                    this.onSort($sortColumn.data("sortedname"), this.getSortClassWhenSortOnServer() === "sorting-asc");
                });
        }

        this.bindTableSorter(this.$sortContainer.data("sortedcolumn"));
    };

    getSortClassWhenSortOnServer = () => {
        const isDecendingSort = this.$sortContainer.data("descendingsort").toString() === "1"
            || this.$sortContainer.data("descendingsort").toLowerCase() === "true";

        return isDecendingSort ? "sorting-desc" : "sorting-asc";
    };

    onSort = (sortedColumnName: string, isDecendingSort: boolean) => {
        this.$sortContainer
            .data("descendingsort", isDecendingSort ? "1" : "0")
            .data("sortedcolumn", sortedColumnName);

        if (this.options.afterSortFunction &&
            typeof this.options.afterSortFunction === "function") {
            this.options.afterSortFunction();
        }
    };

    getSortInfo = () => {
        return {
            sortPropertyName: this.$sortContainer.data("sortedcolumn"),
            isDescendingSort: this.$sortContainer.data("descendingsort").toString() === "1"
                || this.$sortContainer.data("descendingsort").toString().toLowerCase() === "true"
        };
    };

    bindTableSorter = (sortedColumnName: string) => {
        if (!this.options.canSort || !this.options.hasAllDataOnClient) {
            return;
        }

        const disabledSortColumns = this.$sortContainer.find("th[data-disabledsort='true']");
        disabledSortColumns.each((index: number, elem: Element) => {
            const cellIndex = this.$(elem).index();
            this.options.headers[cellIndex] = { sorter: false };
        });

        let sortList = [];
        if (sortedColumnName && sortedColumnName !== "") {
            const sortedColumnHeader = this.$sortContainer.find("[data-sortedname='" + sortedColumnName + "']" +
                (this.$sortContainer.hasClass("stack-mode") ? ":not([class*='hidden-stack-mode'])" : ":not([class*='col-combine'])"))[0];
            const isDescendingSort = this.$sortContainer.data("descendingsort").toString() === "1"
                || this.$sortContainer.data("descendingsort").toString().toLowerCase() === "true";

            sortList = [[this.$(sortedColumnHeader).data("colindex"), isDescendingSort ? 1 : 0]];
        }

        this.$sortContainer.tablesorter(
            {
                defaultParser: 1,
                headers: this.options.headers,
                sortList: sortList,
                textExtraction: this.textExtraction,
                selectorHeaders: 'thead th.header'
            });

        this.$sortContainer.off("sortEnd").on("sortEnd", () => {
            let $sortedcolumn = this.$sortContainer
                .find("thead.tableFloatingHeaderOriginal th.sorting.sorting-asc:visible" +
                    ",thead.tableFloatingHeaderOriginal th.sorting.sorting-desc:visible");

            const modeFilterClass = this.$sortContainer.hasClass("stack-mode")
                ? ".hidden-stack-mode"
                : ".visible-stack-mode";
            $sortedcolumn = $sortedcolumn.not(modeFilterClass);

            if ($sortedcolumn.length === 1) {
                this.onSort($sortedcolumn.attr("data-sortedname"), $sortedcolumn.hasClass("sorting-desc"));
            }
        });
    };

    textExtraction = (node) => {
        const $node = this.$(node);

        if ($node.data("islink")) {
            return $node.find("a").html();
        }

        const html = $node.hasClass("combinedcell") ?
            $node.find("ul>li:first-child").html() :
            $node.html();

        return html;
    };

    afterSort = () => {
        if (this.options.hasAllDataOnClient) {
            let sortedColumn = this.$(".sorting-asc:visible");
            const direction = !sortedColumn.length;
            if (sortedColumn.length === 0) {
                sortedColumn = this.$(".sorting-desc");
            }

            const sortedColumnName = sortedColumn.data("sortedname");

            this.linkBuilderComponent.updateParam("SortPropertyName", sortedColumnName || sortedColumn.parent().data("sortedname"));
            this.linkBuilderComponent.updateParam("IsDescendingSort", direction.toString());
        } else {
            this.$reportForm.trigger("submit");
        }
    };
}