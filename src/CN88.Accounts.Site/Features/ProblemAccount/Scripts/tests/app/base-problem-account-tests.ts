describe("BaseProblemAccount", () => {
    const jQueryFake = $;
    const topWindow = window.top;
    const dateRangePicker = new DateRangePickerComponent($);
    const reviewStatusDropdown = new SingleSelectionDropdownComponent($);
    const pager = new PagerComponent($);

    let baseProblemAccount: BaseProblemAccount;

    beforeEach(() => {
        jasmine.getFixtures().fixturesPath = "ProblemAccount/Scripts/tests/templates";
        loadFixtures("problem-account-template.html");

        baseProblemAccount = new BaseProblemAccount(jQueryFake, topWindow, dateRangePicker, reviewStatusDropdown, pager);

        spyOn(jQueryFake, "blockUI").and.callThrough();
        spyOn(jQueryFake, "unblockUI").and.callThrough();
        spyOn(jQueryFake.fn, "tooltip").and.callThrough();
        spyOn(baseProblemAccount, "cacheDOMElements").and.callThrough();
        spyOn(baseProblemAccount, "renderComponents").and.callThrough();
        spyOn(baseProblemAccount, "bindEvents").and.callThrough();
        spyOn(topWindow["popupManager"], "init").and.callThrough();

        spyOn(dateRangePicker, "render");
        spyOn(reviewStatusDropdown, "render");
        spyOn(pager, "render");
    });

    describe("_init_Always", () => {
        beforeEach(() => {
            // act
            baseProblemAccount.init();
        });

        it("_Calls blockUI", () => {
            // assert
            expect(jQueryFake.blockUI).toHaveBeenCalled();
        });

        it("_Calls cacheDOMElements", () => {
            // assert
            expect(baseProblemAccount.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Calls renderComponents", () => {
            // assert
            expect(baseProblemAccount.renderComponents).toHaveBeenCalled();
        });

        it("_Calls bindEvents", () => {
            // assert
            expect(baseProblemAccount.bindEvents).toHaveBeenCalled();
        });

        it("_Calls unblockUI", () => {
            // assert
            expect(jQueryFake.unblockUI).toHaveBeenCalled();
        });
    });

    it("_cacheDOMElements_Always_All neccessary elements have been cached", () => {
        // act
        baseProblemAccount.cacheDOMElements();

        // assert
        expect(baseProblemAccount.$tooltips).toBeDefined();
        expect(baseProblemAccount.$reportForm).toBeDefined();
        expect(baseProblemAccount.$updateStatusLink).toBeDefined();
        expect(baseProblemAccount.$exportExcelForm).toBeDefined();
        expect(baseProblemAccount.$excelIcon).toBeDefined();
        expect(baseProblemAccount.$btnReview).toBeDefined();
        expect(baseProblemAccount.$checkboxSelectAll).toBeDefined();
        expect(baseProblemAccount.$downlineCheckboxs).toBeDefined();

        expect(baseProblemAccount.resources.ok).toBeDefined();
        expect(baseProblemAccount.resources.updateStatus).toBeDefined();
        expect(baseProblemAccount.resources.setReviewedStatus).toBeDefined();
        expect(baseProblemAccount.resources.systemMessage).toBeDefined();
        expect(baseProblemAccount.resources.setToReviewedValidationMessage).toBeDefined();
        expect(baseProblemAccount.custLevelId).toBeDefined();
        expect(baseProblemAccount.updateReviewStatusUrl).toBeDefined();
    });

    describe("_renderComponents_always", () => {
        beforeEach(() => {
            // arrange
            baseProblemAccount.cacheDOMElements();

            // act
            baseProblemAccount.renderComponents();
        });

        it("_render dateRangePicker", () => {
            // assert
            expect(dateRangePicker.render).toHaveBeenCalled();
        });

        it("_init popupManager", () => {
            // assert
            expect(topWindow["popupManager"].init).toHaveBeenCalledWith(jQueryFake, topWindow);
        });

        it("_render reviewStatusDropdown", () => {
            // assert
            expect(reviewStatusDropdown.render).toHaveBeenCalledWith(new SingleSelectionDropdownOption("dropdown-review-status", "SelectedReviewStatus"));
        });

        it("_render pager", () => {
            // assert
            expect(pager.render).toHaveBeenCalled();
        });
    });
});
