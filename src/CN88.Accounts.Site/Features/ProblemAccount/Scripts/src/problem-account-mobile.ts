class ProblemAccountMobile extends BaseProblemAccount {
    filterToggle: ToggleHandler;

    constructor(
        $: JQueryStatic,
        topWindow: Window,
        dateRangePicker: DateRangePickerComponent,
        reviewStatusDropdown: SingleSelectionDropdownComponent,
        filterToggle: ToggleHandler,
        pager: PagerComponent) {
        super($, topWindow, dateRangePicker, reviewStatusDropdown, pager);

        this.filterToggle = filterToggle;
    }

    cacheDOMElements(): void {
        this.$("#tblProblemAccount").stacktable();
        this.filterToggle.render(new ToggleHandlerOption("filter-toggle-icon", "report-form", "tblProblemAccount-stack,#note,#pager"));
        this.$("body").backtop();

        super.cacheDOMElements();
    }
}

$(document).ready(() => {
    const problemAccountMobile = new ProblemAccountMobile(
        $,
        window.top,
        new DateRangePickerComponent($),
        new SingleSelectionDropdownComponent($),
        new ToggleHandler($),
        new PagerComponent($));

    problemAccountMobile.init();
});
