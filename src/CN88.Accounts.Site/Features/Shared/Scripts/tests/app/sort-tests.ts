describe("SortComponent", () => {
    let sortComponent: SortComponent;
    let linkBuilderComponent: LinkBuilderComponent;

    beforeEach(() => {
        linkBuilderComponent = new LinkBuilderComponent($);
        sortComponent = new SortComponent($, linkBuilderComponent);
    });

    describe("_render", () => {
        it("_Cannot sort -> Remove icon sorting from sorting column", () => {
            // arrange
            setFixtures(`<body>
                            <table id="table-report">
                                <th class="sorting" data-sortedname="Column1">Column 1</th>
                                <th class="sorting" data-sortedname="Column2">Column 2</th>
                            </table>
                        </body>`);

            // act
            sortComponent.render(new SortOption($("#table-report"), false));

            // assert
            expect($(".sorting").length).toEqual(0);
        });

        describe("_Can sort/Have all data on client", () => {
            beforeEach(() => {
                // arrange
                setFixtures(`<body>
                            <table id="table-report">
                                <th class="sorting" data-sortedname="Column1">Column 1</th>
                                <th class="sorting" data-sortedname="Column2">Column 2</th>
                            </table>
                        </body>`);
                spyOn(sortComponent, "bindTableSorter");
                spyOn(sortComponent, "onSort");

                // act
                sortComponent.render(new SortOption($("#table-report"), true, true));
            });

            it("_Not Remove icon sorting from sorting column", () => {
                // assert
                expect($(".sorting").length).not.toEqual(0);
            });
            it("_Bind Table Sorter", () => {
                // assert
                expect(sortComponent.bindTableSorter).toHaveBeenCalled();
            });
            it("Not bind on sort event", () => {
                // assert
                expect(sortComponent.onSort).not.toHaveBeenCalled();
            });
        });

        describe("_Can sort/Not Have all data on client/Descending Sort", () => {
            beforeEach(() => {
                // arrange
                setFixtures(`<body>
                                <table id="table-report" data-descendingsort="True" data-sortedcolumn="Column1">
                                    <th class="sorting" data-sortedname="Column1">Column 1</th>
                                    <th class="sorting" data-sortedname="Column2">Column 2</th>
                                </table>
                            </body>`);
                spyOn(sortComponent, "bindTableSorter");
                spyOn(sortComponent, "onSort");

                // act
                sortComponent.render(new SortOption($("#table-report"), true, false));
            });
            it("_Not Remove icon sorting from sorting column", () => {
                // assert
                expect($(".sorting").length).not.toEqual(0);
            });
            it("_Bind Table Sorter", () => {
                // assert
                expect($("[data-sortedname=\"Column1\"]").hasClass("sorting-desc")).toBeTruthy();
                expect(sortComponent.bindTableSorter).toHaveBeenCalled();
            });
            it("_Bind on sort event", () => {
                // act
                $("[data-sortedname=\"Column2\"]").trigger("click");

                // assert
                expect(sortComponent.onSort).toHaveBeenCalled();
            });
        });

        describe("_Can sort/Not Have all data on client/Ascending Sort", () => {
            beforeEach(() => {
                // arrange
                setFixtures(`<body>
                                <table id="table-report" data-descendingsort="False" data-sortedcolumn="Column1">
                                    <th class="sorting" data-sortedname="Column1">Column 1</th>
                                    <th class="sorting" data-sortedname="Column2">Column 2</th>
                                </table>
                            </body>`);
                spyOn(sortComponent, "bindTableSorter");
                spyOn(sortComponent, "onSort");

                // act
                sortComponent.render(new SortOption($("#table-report"), true, false));
            });
            it("_Not Remove icon sorting from sorting column", () => {
                // assert
                expect($(".sorting").length).not.toEqual(0);
            });
            it("_Bind Table Sorter", () => {
                // assert
                expect(sortComponent.bindTableSorter).toHaveBeenCalled();

                expect($("[data-sortedname=\"Column1\"]").hasClass("sorting-asc")).toBeTruthy();
            });
            it("_Bind on sort event", () => {
                // act
                $("[data-sortedname=\"Column2\"]").trigger("click");

                // assert
                expect(sortComponent.onSort).toHaveBeenCalled();
            });
        });
    });

    describe("_getSortInfo", () => {
        it("_Descending Sort -> Returns Sort Info from table data", () => {
            // arrange
            setFixtures(`<body>
                            <table id="table-report" data-descendingsort="1" data-sortedcolumn="Column1">
                                <th class="sorting" data-sortedname="Column1">Column 1</th>
                                <th class="sorting" data-sortedname="Column2">Column 2</th>
                            </table>
                        </body>`);
            sortComponent.$sortContainer = $("#table-report");

            // act
            var actalSortInfo = sortComponent.getSortInfo();

            // assert
            expect(actalSortInfo).toEqual({
                sortPropertyName: "Column1",
                isDescendingSort: true
            });
        });

        it("_Ascending Sort -> Returns Sort Info from table data", () => {
            // arrange
            setFixtures(`<body>
                            <table id="table-report" data-descendingsort="0" data-sortedcolumn="Column1">
                                <th class="sorting" data-sortedname="Column1">Column 1</th>
                                <th class="sorting" data-sortedname="Column2">Column 2</th>
                            </table>
                        </body>`);
            sortComponent.$sortContainer = $("#table-report");

            // act
            var actalSortInfo = sortComponent.getSortInfo();

            // assert
            expect(actalSortInfo).toEqual({
                sortPropertyName: "Column1",
                isDescendingSort: false
            });
        });
    });
    describe("_bindTableSorter", () => {
        describe("_Cannot Sort", () => {
            beforeEach(() => {
                // arrange
                setFixtures(`<body>
                            <table id="table-report" data-descendingsort="0" data-sortedcolumn="Column1">
                                <th class="sorting" data-sortedname="Column1">Column 1</th>
                                <th class="sorting" data-sortedname="Column2">Column 2</th>
                            </table>
                        </body>`);

                sortComponent.options.canSort = false;
                sortComponent.$sortContainer = $("#table-report");
                sortComponent.options.afterSortFunction = () => null;
                spyOn($.fn, "tablesorter");
                spyOn(sortComponent.options, "afterSortFunction");

                // act
                sortComponent.bindTableSorter("Column1");
            });
            it("_Don't bind tablesorter", () => {
                // assert
                expect($.fn.tablesorter).not.toHaveBeenCalled();
            });
            it("_Don't bind event sort end", () => {
                // assert
                sortComponent.$sortContainer.trigger("sortEnd");
                expect(sortComponent.options.afterSortFunction).not.toHaveBeenCalled();
            });
        });
        describe("_Don't have all data on client", () => {
            beforeEach(() => {
                // arrange
                setFixtures(`<body>
                            <table id="table-report" data-descendingsort="0" data-sortedcolumn="Column1">
                                <th class="sorting" data-sortedname="Column1">Column 1</th>
                                <th class="sorting" data-sortedname="Column2">Column 2</th>
                            </table>
                        </body>`);

                sortComponent.options.canSort = true;
                sortComponent.options.hasAllDataOnClient = false;
                sortComponent.$sortContainer = $("#table-report");
                sortComponent.options.afterSortFunction = () => null;
                spyOn($.fn, "tablesorter");
                spyOn(sortComponent.options, "afterSortFunction");

                // act
                sortComponent.bindTableSorter("Column1");
            });
            it("_Don't bind tablesorter", () => {
                // assert
                expect($.fn.tablesorter).not.toHaveBeenCalled();
            });
            it("_Don't bind event sort end", () => {
                // assert
                sortComponent.$sortContainer.trigger("sortEnd");
                expect(sortComponent.options.afterSortFunction).not.toHaveBeenCalled();
            });
        });
        describe("_Can sort;Have all data on client", () => {
            beforeEach(() => {
                // arrange
                setFixtures(`<body>
                            <table id="table-report" data-descendingsort="0" data-sortedcolumn="Column1">
                                <th class="sorting" data-sortedname="Column1" data-colindex="0">Column 1</th>
                                <th class="sorting" data-sortedname="Column2" data-disabledsort="true">Column 2</th>
                            </table>
                        </body>`);

                sortComponent.render(new SortOption($("#table-report"), true, true, () => null, {}));
                spyOn($.fn, "tablesorter");
                spyOn(sortComponent.options, "afterSortFunction");

                // act
                sortComponent.bindTableSorter("Column1");
            });
            it("_Bind tablesorter", () => {
                // assert
                expect($.fn.tablesorter).toHaveBeenCalledWith({
                    defaultParser: 1,
                    headers: { 1: { sorter: false } },
                    sortList: [[0, 0]],
                    textExtraction: jasmine.any(Function),
                    selectorHeaders: 'thead th.header'
                });
            });
        });
    });

    describe("_textExtraction", () => {
        it("_Link Cell -> Returns HRef Data", () => {
            // arrange
            setFixtures(`<body>
                            <div id="extracted-element" data-islink="true">
                                <a>Cust Name</a>
                            </div>
                        </body>`);

            // act
            var actualData = sortComponent.textExtraction($("#extracted-element"));

            // assert
            expect(actualData).toBe("Cust Name");
        });
        it("_Combined Cell -> Returns First Child Data (Sort Combined Column mean Sort By Win Loss)", () => {
            // arrange
            setFixtures(`<body>
                            <div id="extracted-element" class="combinedcell">
                                <ul>
                                    <li>Win Loss</li>
                                    <li>Comm</li>
                                    <li>Total</li>
                                </ul>
                            </div>
                        </body>`);

            // act
            var actualData = sortComponent.textExtraction($("#extracted-element"));

            // assert
            expect(actualData).toBe("Win Loss");
        });
        it("_Not Combined Cell/Not Link Cell -> Returns Text in Node", () => {
            // arrange
            setFixtures(`<body>
                            <div id="extracted-element">Node Data</div>
                        </body>`);

            // act
            var actualData = sortComponent.textExtraction($("#extracted-element"));

            // assert
            expect(actualData).toBe("Node Data");
        });
    });

    describe("_afterSort", () => {
        it("_Not Have All Data on Client -> Submits Report", () => {
            // arrange
            setFixtures(`<body><form id="report-form" role="form"></form></body>`);
            sortComponent.options.hasAllDataOnClient = false;
            sortComponent.$reportForm = $("#report-form");

            spyOn(sortComponent.$reportForm, "trigger");

            // act
            sortComponent.afterSort();

            // assert
            expect(sortComponent.$reportForm.trigger).toHaveBeenCalledWith("submit");
        });
    });

    describe("_afterSort", () => {
        it("_Not Have All Data on Client -> Submits Report", () => {
            // arrange
            setFixtures(`<body><form id="report-form" role="form"></form></body>`);
            sortComponent.options.hasAllDataOnClient = false;
            sortComponent.$reportForm = $("#report-form");

            spyOn(sortComponent.$reportForm, "trigger");

            // act
            sortComponent.afterSort();

            // assert
            expect(sortComponent.$reportForm.trigger).toHaveBeenCalledWith("submit");
        });
    });
});