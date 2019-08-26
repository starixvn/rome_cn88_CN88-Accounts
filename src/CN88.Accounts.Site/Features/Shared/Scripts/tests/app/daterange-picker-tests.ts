describe("DateRangePicker", () => {
    let dateRangePickerComponent: DateRangePickerComponent;
    const template = `<div id="daterange-picker" class="daterange-picker"
                         data-currentdate="2019-08-26"
                         data-mindate="0001-01-01"
                         data-maxdate="2019-08-26"
                         data-selecteddaterange="0">
                    </div>
                    <div class="form-group from-date-box">
                        <label>Date Notified From</label>
                        <div class="control date">
                            <div id="from-date-container" class="input-group date fromdate">
                                <input type="text" id="from-date" name="FromDate" readonly="readonly" autocomplete="off"
                                       class="ui_nexdatepicker_datetextbox ui_nexdatepicker_fromdate form-control" value="2019-08-13">
                                <span class="input-group-addon">
                                    <span class="icon-calendar" id="fdate_trigger"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group to-date-box">
                        <label>To</label>
                        <div class="control date">
                            <div id="to-date-container" class="input-group date todate">
                                <input type="text" id="to-date" name="ToDate" readonly="readonly" autocomplete="off"
                                       class="ui_nexdatepicker_datetextbox ui_nexdatepicker_todate form-control" value="2019-08-26">
                                <span class="input-group-addon">
                                    <span class="icon-calendar" id="tdate_trigger"></span>
                                </span>
                            </div>
                        </div>
                    </div>`;
    beforeEach(() => {
        setFixtures(template);

        dateRangePickerComponent = new DateRangePickerComponent($);
    });

    describe("_render", () => {
        beforeEach(() => {
            // arrange
            spyOn($.fn, "nexDatePicker");

            // act
            dateRangePickerComponent.render();
        });

        it("_Binds nexDatePicker to date range element", () => {
            // assert
            expect($.fn.nexDatePicker).toHaveBeenCalledWith({
                datePickerFormat: {
                    dateFormat: "YYYY-MM-DD"
                },
                selectedItem: 0,
                minDate: new Date("0001-01-01"),
                maxDate: new Date("2019-08-26"),
                currentDate: new Date("2019-08-26"),
                onSelectDatePicker: jasmine.any(Function)
            });
        });
    });
});
