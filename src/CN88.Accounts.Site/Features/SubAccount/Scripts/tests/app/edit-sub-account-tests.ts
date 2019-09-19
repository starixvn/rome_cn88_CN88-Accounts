describe("EditSubAccount", () => {
    const jQueryFake = $;
    const topWindow = top;
    let editSubAccount: EditSubAccount;

    const template = `<body>
                        <div id="message-container" class="message-container"></div>
                        <form id="formSubAccount">
                        <input id="subAccountId" name="subAccountId" type="hidden" value="1262251">
                        <input type="text" class="control" maxlength="50" value="" id="name" autocomplete="off">
                        <input type="checkbox" value="[D]" id="totalBets" checked="checked">
                        <input type="checkbox" value="[E]" id="reports" checked="checked">
                        <input id="RequiredNameError" name="RequiredNameError" type="hidden" value="Name is required.">
                        <table id="tblManagedAccount" class="tbl-style2 tbl-managed-account">
                            <thead>
                                <tr>
                                    <th class="col-all-check">
                                        <div class="all-check-func">
                                            <input type="checkbox" id="ckboxSelectAll" name="ckboxSelectAll" value="" checked="">
                                        </div>
                                    </th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                        </form>
                    </body>`;

    beforeEach(() => {
        setFixtures(template);
        spyOn(BaseSubAccount.prototype, "cacheElements").and.callThrough();
        spyOn(BaseSubAccount.prototype, "getFormData").and.callThrough();
        editSubAccount = new EditSubAccount(jQueryFake, topWindow);
    });

    it("Inherit BaseSubAccount_Always_EditSubAccount is BaseSubAccount", () => {
        expect(editSubAccount instanceof BaseSubAccount).toBeTruthy();
    });

    it("cacheElements_Always_calls super.cacheElements()", () => {
        // act
        editSubAccount.cacheElements();

        // assert
        expect(BaseSubAccount.prototype.cacheElements).toHaveBeenCalled();
    });

    it("getFormData_Always_calls super.getFormData()", () => {
        // arrange
        editSubAccount.cacheElements();
        spyOn(editSubAccount.$checkboxSelectAll, "is").and.callThrough();

        // act
        editSubAccount.getFormData();

        // assert
        expect(BaseSubAccount.prototype.getFormData).toHaveBeenCalled();
    });
});