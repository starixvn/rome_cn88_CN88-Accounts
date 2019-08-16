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
    $btnReview: JQuery;
    $checkboxSelectAll: JQuery;
    $downlineCheckboxs: JQuery;

    custLevelId: number;
    updateReviewStatusUrl: string;

    resoures = {
        updateStatus: "",
        setReviewedStatus: ""
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
        this.$btnReview = this.$("#btnReview");
        this.$checkboxSelectAll = this.$("#SelectAll");
        this.$downlineCheckboxs = this.$("#tblProblemAccount tbody tr td input[type='checkbox']");

        this.resoures.updateStatus = this.$("#UpdateStatus").val();
        this.resoures.setReviewedStatus = this.$("#SetReviewedStatus").val();

        this.custLevelId = parseInt(this.$("#CustLevelId").val());
        this.updateReviewStatusUrl = this.$("#UpdateReviewStatusUrl").val();
    }

    bindEvents(): void {
        this.$reportForm.submit(this.onSubmit);
        this.$tblProblemAccount.find(".status-link").click(this.onStatusLinkClick);
        this.$excelIcon.click(() => { this.$exportExcelForm.submit(); });
        this.$checkboxSelectAll.change(this.onCheckboxSelectAllChange);
        this.$downlineCheckboxs.on("click touchend", this.onDownlineCheckboxesClick);
        this.$btnReview.on("click touchend", this.onSetToReviewClick);
    }

    onCheckboxSelectAllChange = () => {
        const isChecked = this.$checkboxSelectAll.is(":checked");
        this.$downlineCheckboxs.prop("checked", isChecked);
    };

    onDownlineCheckboxesClick = (e: JQueryEventObject) => {
        if (this.$(e.target).is(":checked")) {
            let isAllchecked = true;

            this.$downlineCheckboxs.each(function () {
                if (!this.checked) {
                    isAllchecked = false;
                }
            });

            if (isAllchecked) {
                this.$checkboxSelectAll.prop("checked", true);
            }
        } else {
            this.$checkboxSelectAll.prop("checked", false);
        }
    };

    onStatusLinkClick = (e: JQueryEventObject) => {
        e.preventDefault();
        e.stopPropagation();

        const $link = this.$(e.currentTarget);
        const url = $link.attr("href");
        const title = `${this.resoures.updateStatus} - ${$link.data("username")}`;

        this.openPopup(url, title, "modal modal-status");
    };

    onSubmit = () => {
        this.reviewStatusDropdown.updateSelectedValue();
    };

    onSetToReviewClick = () => {
        const seleledDownlines = this.getSelectedDownlines();
        const params = {
            custLevelId: this.custLevelId,
            selectedDownlineIds: seleledDownlines.custIds,
            selectedDownlineUsernames: seleledDownlines.usernames
        };
        const url = `${this.updateReviewStatusUrl}?${this.$.param(params)}`;

        this.openPopup(url, this.resoures.setReviewedStatus, "", 500, 300);
    };

    getSelectedDownlines() {
        const selectedCustIds = [];
        const selectedUsernames = [];

        this.$downlineCheckboxs.each((index: number, elem: Element) => {
            const $elem = this.$(elem);

            if ($elem.is(":checked")) {
                selectedCustIds.push([$elem.val()]);
                selectedUsernames.push([$elem.data("username")]);
            }
        });

        return {
            custIds: selectedCustIds.join(","),
            usernames: selectedUsernames.join(", ")
        };
    }

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
