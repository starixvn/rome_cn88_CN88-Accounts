class ProblemAccount {
    $: JQueryStatic;
    topWindow: Window;
    dateRangePicker: DateRangePickerComponent;
    reviewStatusDropdown: SingleSelectionDropdownComponent;
    pager: PagerComponent;

    $tooltips: JQuery;
    $reportForm: JQuery;
    $tblProblemAccount: JQuery;
    $updateStatusLink: JQuery;
    $exportExcelForm: JQuery;
    $excelIcon: JQuery;

    resoures = {
        updateStatus: ""
    };

    constructor(
        $: JQueryStatic,
        topWindow: Window,
        dateRangePicker: DateRangePickerComponent,
        reviewStatusDropdown: SingleSelectionDropdownComponent,
        pager: PagerComponent) {
        this.$ = $;
        this.topWindow = topWindow;
        this.dateRangePicker = dateRangePicker;
        this.reviewStatusDropdown = reviewStatusDropdown;
        this.pager = pager;
    }

    init(): void {
        this.$.blockUI();
        this.cacheDOMElements();
        this.bindEvents();

        this.dateRangePicker.render();
        this.topWindow["popupManager"].init(this.$, this.topWindow);
        this.reviewStatusDropdown.render(new SingleSelectionDropdownOption("dropdown-review-status", "SelectedReviewStatus"));
        this.$tooltips.tooltip();
        this.pager.render();

        this.$.unblockUI();
    }

    cacheDOMElements(): void {
        this.$tooltips = this.$('[data-toggle="tooltip"]');
        this.$reportForm = this.$("#report-form");
        this.$tblProblemAccount = this.$("#tblProblemAccount");
        this.$updateStatusLink = this.$("#UpdateStatusLink");
        this.$exportExcelForm = this.$("#export-excel-form");
        this.$excelIcon = this.$("#icon-excel");

        this.resoures.updateStatus = this.$("#UpdateStatus").val();
    }

    bindEvents(): void {
        this.$reportForm.submit(this.onSubmit);
        this.$tblProblemAccount.find(".status-link").click(this.onStatusLinkClick);
        this.$excelIcon.click(() => { this.$exportExcelForm.submit(); });
        this.topWindow["popupManager"].onClosing = () => {
            this.topWindow.$("#main").attr("src", (index, attr) => { return attr; });
        };
    }

    onStatusLinkClick = (e: JQueryEventObject) => {
        e.preventDefault();
        e.stopPropagation();

        const $link = this.$(e.currentTarget);
        const url = $link.attr("href");
        const title = `${this.resoures.updateStatus} - ${$link.data("username")}`;

        this.openPopup(url, title, "modal modal-status");
    };

    openPopup(url: string, title = "", popupCss = "", width = 490, height = 210): void {
        this.topWindow["popupManager"].open({ title, url, width, height, popupCss });
    }

    onSubmit = () => {
        this.reviewStatusDropdown.updateSelectedValue();
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
