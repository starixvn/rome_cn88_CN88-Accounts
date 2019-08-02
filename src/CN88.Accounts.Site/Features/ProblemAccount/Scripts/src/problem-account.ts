class ProblemAccount {
    $: JQueryStatic;
    dateRangePicker: DateRangePickerComponent;

    constructor($: JQueryStatic, dateRangePicker: DateRangePickerComponent) {
        this.$ = $;
        this.dateRangePicker = dateRangePicker;
    }

    init(): void {
        this.$.blockUI();
        this.dateRangePicker.render();
        this.$.unblockUI();
    }
}

$(document).ready(() => {
    new ProblemAccount($, new DateRangePickerComponent($)).init();
});
