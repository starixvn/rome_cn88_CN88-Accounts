class ProblemAccount {
    $: JQueryStatic;
    topWindow: Window;
    dateRangePicker: DateRangePickerComponent;
    reviewStatusDropdown: SingleSelectionDropdownComponent;
    pager: PagerComponent;

    $tooltips: JQuery;
    $tblProblemAccount: JQuery;
    $updateStatusLink: JQuery;

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
        this.$tblProblemAccount = this.$("#tblProblemAccount");
        this.$updateStatusLink = this.$("#UpdateStatusLink");

        this.resoures.updateStatus = this.$("#UpdateStatus").val();
    }

    bindEvents(): void {
        this.$tblProblemAccount.find(".status-link").click(this.onStatusLinkClick);
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
