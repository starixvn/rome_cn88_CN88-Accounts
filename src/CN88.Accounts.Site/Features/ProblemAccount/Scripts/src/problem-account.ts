class ProblemAccount {
    $: JQueryStatic;
    dateRangePicker: DateRangePickerComponent;
    reviewStatusDropdown: SingleSelectionDropdownComponent;

    elements = {
        tooltips: null
    };

    constructor(
        $: JQueryStatic,
        dateRangePicker: DateRangePickerComponent,
        reviewStatusDropdown: SingleSelectionDropdownComponent) {
        this.$ = $;
        this.dateRangePicker = dateRangePicker;
        this.reviewStatusDropdown = reviewStatusDropdown;
    }

    init(): void {
        this.$.blockUI();
        this.cacheDOMElements();
        this.bindEvents();

        this.dateRangePicker.render();
        this.reviewStatusDropdown.render(new SingleSelectionDropdownOption("dropdown-review-status", "SelectedReviewStatus"));
        this.elements.tooltips.tooltip();

        this.$.unblockUI();
    }

    cacheDOMElements(): void {
        this.elements.tooltips = this.$('[data-toggle="tooltip"]');
    }

    bindEvents(): void {

    }
}

$(document).ready(() => {
    new ProblemAccount($, new DateRangePickerComponent($), new SingleSelectionDropdownComponent($)).init();
});
