describe("NewSubAccount", () => {
    const jqueryFake = $;
    const topWindow = top;
    const encryptor = new Encryptor();
    let newSubAccount: NewSubAccount;

    const template = `<body>
                        <div id="message-container" class="message-container"></div>
                        <form id="formSubAccount">
                        <input id="subAccountId" name="subAccountId" type="hidden" value="1262251">
                        <input type="text" class="control" maxlength="50" value="" id="name" autocomplete="off">
                        <input type="checkbox" value="[D]" id="totalBets" checked="checked">
                        <input type="checkbox" value="[E]" id="reports" checked="checked">
                        <select id="suffix1" name="suffix1">
                            <option value="0">0</option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                        </select>
                        <select id="suffix2" name="suffix2">
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                            <option value="0">0</option>
                        </select>
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
                        <input id="HashKey" name="HashKey" type="hidden" value="d3o5">
                        <input id="RequiredNameError" name="RequiredNameError" type="hidden" value="Name is required.">
                        <input id="RequiredPasswordError" name="RequiredPasswordError" type="hidden" value="Sorry, password cannot be empty!!!">
                        <input id="PasswordWrongRuleError" name="PasswordWrongRuleError" type="hidden" value="Your password does not meet the minimum security requirement.">
                        <input id="ConfirmPasswordError" name="ConfirmPasswordError" type="hidden" value="The password was not correctly confirmed.">
                        <input id="SuccessMessage" name="SuccessMessage" type="hidden" value="Account successfully created.">
                    </body>`;

    beforeEach(() => {
        setFixtures(template);
        spyOn(BaseSubAccount.prototype, "cacheElements").and.callThrough();
        spyOn(BaseSubAccount.prototype, "getFormData").and.callThrough();
        newSubAccount = new NewSubAccount(jqueryFake, topWindow, encryptor);
    });

    it("Inherit BaseSubAccount_Always_EditSubAccount is BaseSubAccount", () => {
        expect(newSubAccount instanceof BaseSubAccount).toBeTruthy();
    });

    it("cacheElements_Always_calls super.cacheElements()", () => {
        // act
        newSubAccount.cacheElements();

        // assert
        expect(BaseSubAccount.prototype.cacheElements).toHaveBeenCalled();
    });

    it("getFormData_Always_calls super.getFormData()", () => {
        // arrange
        newSubAccount.cacheElements();
        spyOn(newSubAccount.$checkboxSelectAll, "is").and.callThrough();

        // act
        newSubAccount.getFormData();

        // assert
        expect(BaseSubAccount.prototype.getFormData).toHaveBeenCalled();
    });
});