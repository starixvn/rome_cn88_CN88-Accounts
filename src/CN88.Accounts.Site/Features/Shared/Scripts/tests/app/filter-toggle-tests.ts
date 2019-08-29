describe("ToggleHandler", () => {
    let toggleHandler: ToggleHandler;

    const template = `<body>
                        <span class="icon-filter-down" id="filter-toggle-icon"></span>
                        <form class="form-inline filter" role="form" id="report-form"></form>
                        <table id="tbl-report-stack" class="tbl-style2 table-problem-account"></table>
                    </body>`;

    beforeEach(() => {
        setFixtures(template);
        toggleHandler = new ToggleHandler($);
    });

    describe("_render", () => {
        it("_always_cache elements", () => {
            // act
            toggleHandler.render();

            // assert
            expect(toggleHandler.options).toBeDefined();
            expect(toggleHandler.$filterToggleIcon).toBeDefined();
            expect(toggleHandler.$sourceElement).toBeDefined();
            expect(toggleHandler.$targetElement).toBeDefined();
        });
    });

    describe("_collapseToggle", () => {
        beforeEach(() => {
            toggleHandler.render();

            spyOn(toggleHandler.$sourceElement, "slideUp").and.callThrough();
            spyOn(toggleHandler.$targetElement, "slideDown").and.callThrough();
            spyOn(toggleHandler.$filterToggleIcon, "removeClass").and.callThrough();
            spyOn(toggleHandler.$filterToggleIcon, "addClass").and.callThrough();

            toggleHandler.collapseToggle();
        });

        it("_slideUp source element", () => {
            expect(toggleHandler.$sourceElement.slideUp).toHaveBeenCalledWith(200);
        });

        it("_slideDown target element", () => {
            expect(toggleHandler.$targetElement.slideDown).toHaveBeenCalledWith(210);
        });

        it("_add and remove class for filter toggle icon", () => {
            expect(toggleHandler.$filterToggleIcon.removeClass).toHaveBeenCalledWith("icon-filter-up");
            expect(toggleHandler.$filterToggleIcon.addClass).toHaveBeenCalledWith("icon-filter-down");
        });
    });

    describe("_expandToggle", () => {
        beforeEach(() => {
            toggleHandler.render();

            spyOn(toggleHandler.$sourceElement, "slideDown").and.callThrough();
            spyOn(toggleHandler.$targetElement, "slideUp").and.callThrough();
            spyOn(toggleHandler.$filterToggleIcon, "removeClass").and.callThrough();
            spyOn(toggleHandler.$filterToggleIcon, "addClass").and.callThrough();

            toggleHandler.expandToggle();
        });

        it("_slideDown source element", () => {
            expect(toggleHandler.$sourceElement.slideDown).toHaveBeenCalledWith(200);
        });

        it("_slideUp target element", () => {
            expect(toggleHandler.$targetElement.slideUp).toHaveBeenCalledWith(210);
        });

        it("_add and remove class for filter toggle icon", () => {
            expect(toggleHandler.$filterToggleIcon.removeClass).toHaveBeenCalledWith("icon-filter-down");
            expect(toggleHandler.$filterToggleIcon.addClass).toHaveBeenCalledWith("icon-filter-up");
        });
    });
});
