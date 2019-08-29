describe("ProblemAccount", () => {
    const jQueryFake = $;
    const topWindow = window.top;
    const dateRangePicker = new DateRangePickerComponent($);
    const reviewStatusDropdown = new SingleSelectionDropdownComponent($);
    const pager = new PagerComponent($);

    let problemAccount: ProblemAccount;

    beforeEach(() => {
        problemAccount = new ProblemAccount(jQueryFake, topWindow, dateRangePicker, reviewStatusDropdown, pager);
    });

    it("Inherit BaseProblemAccount_Always_ProblemAccount is BaseProblemAccount", () => {
        // assert
        expect(problemAccount instanceof BaseProblemAccount).toBeTruthy();
    });
});
