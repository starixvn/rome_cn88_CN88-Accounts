class ProblemAccount extends BaseProblemAccount {
    constructor(
        $: JQueryStatic,
        topWindow: Window,
        dateRangePicker: DateRangePickerComponent,
        reviewStatusDropdown: SingleSelectionDropdownComponent,
        pager: PagerComponent) {
        super($, topWindow, dateRangePicker, reviewStatusDropdown, pager);
    }
}

$(document).ready(() => {
    const problemAccount = new ProblemAccount(
        $,
        window.top,
        new DateRangePickerComponent($),
        new SingleSelectionDropdownComponent($),
        new PagerComponent($));

    problemAccount.init();
});
