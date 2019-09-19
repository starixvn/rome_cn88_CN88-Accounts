/// <reference path="base-sub-account.ts" />

class EditSubAccount extends BaseSubAccount {
    constructor($: JQueryStatic, top: Window) {
        super($, top);
    }

    public cacheElements(): void {
        super.cacheElements();
        this.elements.subAccountId = this.$("#subAccountId").val();
    }

    public getFormData() {
        const formData = super.getFormData();

        return {
            name: formData.name,
            pageAccess: formData.pageAccess,
            isSelectAllDownline: formData.isSelectAllDownline,
            downlineId: formData.downlineId,
            custId: this.elements.subAccountId
        };
    }
}

$(() => {
    new EditSubAccount($, top).init();
});