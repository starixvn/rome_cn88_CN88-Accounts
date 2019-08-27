class UpdateReviewStatus {
    $: JQueryStatic;
    topWindow: Window;

    $messageContainer: JQuery;
    $formUpdateReviewStatus: JQuery;
    $btnCancel: JQuery;

    message: Message;
    successMessage: string;

    constructor($: JQueryStatic, topWindow: Window, message: Message) {
        this.$ = $;
        this.topWindow = topWindow;
        this.message = message;
    }

    init(): void {
        this.$.blockUI();
        this.cacheDOMElements();
        this.bindEvents();
        this.$.unblockUI();
    }

    cacheDOMElements(): void {
        this.$messageContainer = this.$("#message-container");
        this.$formUpdateReviewStatus = this.$("#formUpdateReviewStatus");
        this.$btnCancel = this.$("#btnCancel");
        this.successMessage = this.$("#SuccessMessage").val();
    }

    bindEvents(): void {
        this.$btnCancel.on("click touchend", () => {
            this.topWindow["popupManager"].close();
        });

        this.$formUpdateReviewStatus.submit(this.onSubmit);

        this.$(".more-link").click(function () {
            $(this).parent().prev().toggle();
            $(this).prev().toggle();
            $(this).remove();

            return false;
        });
    }

    onSubmit = (e: JQueryEventObject) => {
        e.preventDefault();
        this.$.blockUI();

        this.$.ajax({
            type: "POST",
            url: this.$formUpdateReviewStatus.attr("action"),
            data: this.getParams(),
            dataType: "json",
            success: (data) => {
                const result = new Result(data.ErrCode, data.ErrMsg);

                if (result.IsSuccessful) {
                    this.message.showSuccess(this.$messageContainer, this.successMessage);
                    this.topWindow["indexManager"].closePopupAndReloadMainIframe();
                } else {
                    this.message.showMessage(this.$messageContainer, result);
                }
            },
            complete: this.$.unblockUI
        });
    };

    getParams() {
        return {
            custLevelId: this.$("#CustLevelId").val(),
            selectedDownlineIds: this.$("#SelectedDownlineIds").val(),
            isAffectedAllDownlines: this.$("input[name='IsAffectedAllDownlines']:checked").val()
        };
    }
}

$(document).ready(() => {
    const updateReviewStatus = new UpdateReviewStatus($, window.top, new Message());
    updateReviewStatus.init();
});
