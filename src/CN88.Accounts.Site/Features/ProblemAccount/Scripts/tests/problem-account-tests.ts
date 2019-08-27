describe("ProblemAccount", () => {
    const jQueryFake = $;
    const topWindow = window.top;
    const dateRangePicker = new DateRangePickerComponent($);
    const reviewStatusDropdown = new SingleSelectionDropdownComponent($);
    const pager = new PagerComponent($);

    let problemAccount: ProblemAccount;

    beforeEach(() => {
        problemAccount = new ProblemAccount(jQueryFake, topWindow, dateRangePicker, reviewStatusDropdown, pager);

        spyOn(jQueryFake, "blockUI").and.callThrough();
        spyOn(jQueryFake, "unblockUI").and.callThrough();
        spyOn(jQueryFake.fn, "tooltip").and.callThrough();
        spyOn(BaseProblemAccount.prototype, "cacheDOMElements").and.callThrough();
        spyOn(BaseProblemAccount.prototype, "renderComponents").and.callThrough();
        spyOn(BaseProblemAccount.prototype, "bindEvents").and.callThrough();
        spyOn(topWindow["popupManager"], "init").and.callThrough();
    });

    it("Inherit BaseProblemAccount_Always_ProblemAccount is BaseProblemAccount", () => {
        // assert
        expect(problemAccount instanceof BaseProblemAccount).toBeTruthy();
    });

    describe("_init_Always", () => {
        beforeEach(() => {
            // act
            problemAccount.init();
        });

        it("_Calls blockUI", () => {
            // assert
            expect(jQueryFake.blockUI).toHaveBeenCalled();
        });

        it("_Calls cacheDOMElements", () => {
            // assert
            expect(BaseProblemAccount.prototype.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Calls renderComponents", () => {
            // assert
            expect(BaseProblemAccount.prototype.renderComponents).toHaveBeenCalled();
        });

        it("_Calls bindEvents", () => {
            // assert
            expect(BaseProblemAccount.prototype.bindEvents).toHaveBeenCalled();
        });

        it("_Calls unblockUI", () => {
            // assert
            expect(jQueryFake.unblockUI).toHaveBeenCalled();
        });
    });
});
