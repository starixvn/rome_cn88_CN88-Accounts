describe("ProblemAccountMobile", () => {
    const jQueryFake = $;
    const topWindow = window.top;
    const dateRangePicker = new DateRangePickerComponent($);
    const reviewStatusDropdown = new SingleSelectionDropdownComponent($);
    const pager = new PagerComponent($);
    const filterToggle = new ToggleHandler($);

    let problemAccountMobile: ProblemAccountMobile;

    const template = `<body>
                        <table id="tblProblemAccount"></table>
                    </body>`;

    beforeEach(() => {
        setFixtures(template);
        problemAccountMobile = new ProblemAccountMobile(jQueryFake, topWindow, dateRangePicker, reviewStatusDropdown, filterToggle, pager);
    });

    it("Inherit BaseProblemAccount_Always_ProblemAccountMobile is BaseProblemAccount", () => {
        // assert
        expect(problemAccountMobile instanceof BaseProblemAccount).toBeTruthy();
    });

    describe("_cacheDOMElements_always", () => {
        beforeEach(() => {
            problemAccountMobile.cacheDOMElements();

            spyOn(problemAccountMobile.$tblProblemAccount, "stacktable").and.callThrough();
            spyOn(problemAccountMobile.$body, "backtop").and.callThrough();
            spyOn(problemAccountMobile.filterToggle, "render").and.callThrough();
        });

        it("_cached need elements", () => {
            expect(problemAccountMobile.$tblProblemAccount).toBeDefined();
            expect(problemAccountMobile.$body).toBeDefined();
        });
    });
});
