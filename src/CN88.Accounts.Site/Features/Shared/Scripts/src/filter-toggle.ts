"use strict";

class ToggleHandlerOption {
    constructor(
        public toggleIconId = "filter-toggle-icon",
        public sourceId = "report-form",
        public targetId = "tbl-report-stack",
        public hideSourceElement = true) {
    }
}

class ToggleHandler {
    $: JQueryStatic;
    $filterToggleIcon: JQuery;
    $sourceElement: JQuery;
    $targetElement: JQuery;
    options = new ToggleHandlerOption();

    private readonly iconFilterDownClass = "icon-filter-down";
    private readonly iconFilterUpClass = "icon-filter-up";
    private readonly sourceSlideDuration = 200;
    private readonly targetSlideDuration = 210;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    render = (options: ToggleHandlerOption = new ToggleHandlerOption()) => {
        this.options = this.$.extend({}, this.options, options) as ToggleHandlerOption;

        this.$filterToggleIcon = this.$("#" + this.options.toggleIconId);
        this.$sourceElement = this.$("#" + this.options.sourceId);
        this.$targetElement = this.$("#" + this.options.targetId);

        if (this.options.hideSourceElement) {
            this.$sourceElement.hide();
        }

        this.$filterToggleIcon.on("click touchend", this.onFilterToggleIconClick);
    };

    onFilterToggleIconClick = (e: JQueryEventObject) => {
        e.preventDefault();
        const isCollapsed = this.$filterToggleIcon.hasClass(this.iconFilterDownClass);

        if (isCollapsed) {
            this.expandToggle();
        }
        else {
            this.collapseToggle();
        }
    };

    expandToggle(): void {
        this.$sourceElement
            .slideDown(this.sourceSlideDuration);
        this.$targetElement
            .slideUp(this.targetSlideDuration);
        this.$filterToggleIcon
            .removeClass(this.iconFilterDownClass)
            .addClass(this.iconFilterUpClass);
    }

    collapseToggle(): void {
        this.$sourceElement
            .slideUp(this.sourceSlideDuration);
        this.$targetElement
            .slideDown(this.targetSlideDuration);
        this.$filterToggleIcon
            .removeClass(this.iconFilterUpClass)
            .addClass(this.iconFilterDownClass);
    }
}
