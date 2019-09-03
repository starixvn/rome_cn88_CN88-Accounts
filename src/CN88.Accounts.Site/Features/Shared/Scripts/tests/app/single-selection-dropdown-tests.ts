describe("SingleSelectionDropdownComponent", () => {
    let singleSelectionDropdownComponent: SingleSelectionDropdownComponent;
    const template = {
        hasOptionsSelection: `<body>
                                <div id="dropdown-container">
                                    <select id="dropdown-single">
                                        <option value="Option 1">Option 1</option>
                                        <option value="Option 2" selected="selected">Option 2</option>
                                        <option value="Option 3">Option 3</option>
                                        <option value="Option 4">Option 4</option>
                                        <option value="Option 5">Option 5</option>
                                    </select>
                                    <input type="hidden" id="UserSelectedId" value="UserSelectedId" />
                                </div>
                            </body>`
    };

    beforeEach(() => {
        singleSelectionDropdownComponent = new SingleSelectionDropdownComponent($);
    });

    describe("_render_Always", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.hasOptionsSelection);

            spyOn($.fn, "multiselect");

            // act
            singleSelectionDropdownComponent.render();
        });

        it("_Init Correct Selected Value", () => {
            // arrange
            const expectedValue = "Option 2";

            // assert
            expect(singleSelectionDropdownComponent.selectedValue).toBe(expectedValue);
        });
        it("_Bind multi selection", () => {
            // assert
            expect($.fn.multiselect).toHaveBeenCalledWith({
                multiple: false,
                maxHeight: 300,
                numberDisplayed: 1,
                includeSelectAllOption: false,
                onChange: singleSelectionDropdownComponent.onchange
            });
        });
    });
    describe("_getSelectedItems", () => {
        it("_Not Have Option_Returns Empty", () => {
            // arrange
            setFixtures(`<body>
                            <select id="dropdown-single">
                            </select>
                            <input type="hidden" id="UserSelectedId" value="UserSelectedId" />
                        </body>`);
            singleSelectionDropdownComponent.render();

            // act
            const actualSelectedItem = singleSelectionDropdownComponent.getSelectedItem();

            // assert
            expect(actualSelectedItem).toBe("");
        });
        it("_Have Options_Returns Correct Value", () => {
            // arrange
            setFixtures(template.hasOptionsSelection);
            singleSelectionDropdownComponent.render();
            const expectedValue = "Option 2";

            // act
            const actualSelectedItem = singleSelectionDropdownComponent.getSelectedItem();

            // assert
            expect(actualSelectedItem).toBe(expectedValue);
        });
    });
    describe("_updateSelectedValue", () => {
        it("_Always -> Sets New Value to Hidden Field", () => {
            // arrange
            setFixtures(template.hasOptionsSelection);
            singleSelectionDropdownComponent.render();
            singleSelectionDropdownComponent.selectedValue = "Option 3";
            const expectValue = "Option 3";

            // act
            singleSelectionDropdownComponent.updateSelectedValue();

            // assert
            expect(singleSelectionDropdownComponent.$hiddenInput.val()).toBe(expectValue);
        });
    });

    describe("_onchange", () => {
        it("_Always -> Stores Changed Value", () => {
            // arrange
            setFixtures(template.hasOptionsSelection);
            singleSelectionDropdownComponent.render();
            const expectValue = "Option 3";

            singleSelectionDropdownComponent.options.onChange = jasmine.any(Function);
            spyOn(singleSelectionDropdownComponent.options, "onChange");

            // act
            $("#dropdown-container .dropdown-menu input[value=\"Option 3\"]").click();

            // assert
            expect(singleSelectionDropdownComponent.selectedValue).toBe(expectValue);
            expect(singleSelectionDropdownComponent.options.onChange).toHaveBeenCalled();
        });
    });

    describe("_changeSelection", () => {
        it("_Always -> Stores selection", () => {
            // arrange
            setFixtures(template.hasOptionsSelection);
            singleSelectionDropdownComponent.render();
            const expectValue = "Option 3";
            spyOn($.fn, "multiselect");

            // act
            singleSelectionDropdownComponent.changeSelection(expectValue);

            // assert
            expect(singleSelectionDropdownComponent.$dropdown.val()).toBe(expectValue);
            expect(singleSelectionDropdownComponent.$hiddenInput.val()).toBe(expectValue);
            expect($.fn.multiselect).toHaveBeenCalledWith("select", [expectValue]);
            expect($.fn.multiselect).toHaveBeenCalledWith("refresh");
        });
    });
});
