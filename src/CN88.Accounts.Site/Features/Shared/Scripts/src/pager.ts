class PagerOption {
    constructor(
        public onSelectPage: () => void = null,
        public pagerId = "pager",
        public formId = "report-form",
        public pageIndexId = "PageIndex",
        public pageSizeId = "PageSize",
        public pageList: number[] = [10, 50, 100, 500]) {
    }
}

class PagerComponent {
    $: JQueryStatic;
    $pageIndex: JQuery;
    $pageSize: JQuery;
    $reportForm: JQuery;

    totalRecords: number;
    pageSize: number;
    pageIndex: number;

    pageList: number[] = [];

    pagerOption = new PagerOption();

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    render = (pagerOption: PagerOption = new PagerOption(this.onChange)) => {
        this.pagerOption = this.$.extend({}, this.pagerOption, pagerOption) as PagerOption;
        const pager = this.$("#" + this.pagerOption.pagerId);

        this.$reportForm = this.$("#" + this.pagerOption.formId);
        this.totalRecords = pager.data("totalrecords");
        this.pageSize = pager.data("pagesize");
        this.pageIndex = pager.data("pageindex");
        this.$pageIndex =
            this.$(`<input type="hidden" id="${this.pagerOption.pageIndexId}" name="${this.pagerOption.pageIndexId}" value="${this.pageIndex}" />`);
        this.$pageSize =
            this.$(`<input type="hidden" id="${this.pagerOption.pageSizeId}" name="${this.pagerOption.pageSizeId}" value="${this.pageSize}" />`);
        this.pageList = this.pagerOption.pageList;

        this.$reportForm.append(this.$pageIndex).append(this.$pageSize);

        if (this.isShow()) {
            pager.pagination({
                pageList: this.pageList,
                total: this.totalRecords,
                pageSize: this.pageSize,
                pageNumber: this.pageIndex,
                showRefresh: false,
                onSelectPage: this.onSelectPage,
                beforePageText: pager.data("labelpage"),
                afterPageText: `${pager.data("labelof")} {pages}`,
                displayMsg: pager.data("labeldisplayitems")
            });

            pager.removeClass("hide");
        } else {
            pager.addClass("hide");
        }

        return pager;
    };

    onSelectPage = (pageNumber: number, pageSize: number) => {
        this.updateHiddenFields(pageNumber, pageSize);

        if (this.pagerOption.onSelectPage) {
            this.pagerOption.onSelectPage();
        }
    };

    onChange = () => {
        this.$reportForm.trigger("submit");
    };

    updateHiddenFields = (pageNumber: number, pageSize: number) => {
        this.$pageIndex.val(pageNumber);
        this.$pageSize.val(pageSize);
    };

    isShow = () => {
        // total records less than min page size => do not need to show pager
        //return this.pageList[0] <= this.totalRecords;
        return true;
    };
}
