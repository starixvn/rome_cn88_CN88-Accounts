describe("SubAccount", () => {
    const template = `<body>
	                    <div id="message-container"></div>
	                    <button class="btn-submit add-new" id="btnNewSubAccount"></button>
	                    <table class="tbl-style2 tbl-subaccount" id="tbl-subaccount" data-totalrecords="0" data-sortedcolumn="UserName" data-descendingsort="false" style="padding: 0px;"></table>
	                    <input id="NewSubAccount" name="NewSubAccount" type="hidden" value="New Sub Account">
	                    <input id="EditSubAccount" name="EditSubAccount" type="hidden" value="Edit Sub Account">
	                    <input id="ResetPassword" name="ResetPassword" type="hidden" value="Reset Password">
	                    <input id="ResetSecurityCode" name="ResetSecurityCode" type="hidden" value="Reset Security Code">
	                    <input id="UnlockAccountConfirmMessage" name="UnlockAccountConfirmMessage" type="hidden" value="Do you want to unlock this account?">
	                    <input id="UnlockSubAccount" name="UnlockSubAccount" type="hidden" value="Unlock Sub Account">
	                    <input id="DeleteSubAccountConfirmMessage" name="DeleteSubAccountConfirmMessage" type="hidden" value="Are you sure you want to delete this account?">
	                    <input id="DeleteSubAccount" name="DeleteSubAccount" type="hidden" value="Delete Sub Account">
	                    <input id="OK" name="OK" type="hidden" value="OK">
	                    <input id="Cancel" name="Cancel" type="hidden" value="Cancel">
	                    <input id="NewSubAccountLink" name="NewSubAccountLink" type="hidden" value="NewSubAccountLink">
	                    <input id="EditSubAccountLink" name="EditSubAccountLink" type="hidden" value="EditSubAccountLink">
	                    <input id="ResetPasswordLink" name="ResetPasswordLink" type="hidden" value="ResetPasswordLink">
	                    <input id="ResetSecurityCodeLink" name="ResetSecurityCodeLink" type="hidden" value="ResetSecurityCodeLink">
	                    <input id="UnlockSubAccountLink" name="UnlockSubAccountLink" type="hidden" value="UnlockSubAccountLink">
	                    <input id="DeleteSubAccountLink" name="DeleteSubAccountLink" type="hidden" value="DeleteSubAccountLink">
	                    <input id="VerifySecurityCodeLink" name="VerifySecurityCodeLink" type="hidden" value="VerifySecurityCodeLink">
	                    <input data-val="true" data-val-required="The IsDevice field is required." id="IsDevice" name="IsDevice" type="hidden" value="False">
	                    <input id="createdDateColIndex" name="createdDateColIndex" type="hidden" value="7">
                    </body>`;

    const jQueryFake = $;
    const topWindow = top;
    const sortComponent = new SortComponent($, new LinkBuilderComponent($));

    let subAccount: SubAccount;

    beforeEach(() => {
        setFixtures(template);
        subAccount = new SubAccount(jQueryFake, topWindow, sortComponent);
        spyOn(subAccount, "cacheElements").and.callThrough();
        spyOn(subAccount, "bindEvents").and.callThrough();
    });

    describe("_init_Always", () => {
        beforeEach(() => {
            subAccount.init();
        });

        it("_Calls cacheDOMElements", () => {
            expect(subAccount.cacheElements).toHaveBeenCalled();
        });

        it("_Calls bindEvents", () => {
            expect(subAccount.bindEvents).toHaveBeenCalled();
        });
    });

    it("_cacheElements_Always_All neccessary elements have been cached", () => {
        subAccount.cacheElements();

        expect(subAccount.tableSubAccount).toBeDefined();
        expect(subAccount.resources).toBeDefined();
        expect(subAccount.urls).toBeDefined();
    });

    it("_renderSortComponent_Always_render sortComponent", () => {
        spyOn(subAccount.sortComponent, "render").and.callThrough();
        subAccount.cacheElements();
        subAccount.renderSortComponent();

        expect(subAccount.sortComponent.render).toHaveBeenCalled();
    });
});