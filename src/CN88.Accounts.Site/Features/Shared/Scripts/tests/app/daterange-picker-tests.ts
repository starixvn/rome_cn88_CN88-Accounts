describe("DateRangePicker", () => {
    let dateRangePickerComponent: DateRangePickerComponent;
    const template = `<div id="daterange-picker" class="daterange-picker"
                        data-currentdate="2019-08-26"
                        data-mindate="1970-01-01"
                        data-maxdate="2019-08-26"
                        data-selecteddaterange="0">
                    </div>`;
    beforeEach(() => {
        // arrange
        setFixtures(template);
        dateRangePickerComponent = new DateRangePickerComponent($);
        spyOn($.fn, "nexDatePicker");
    });

    it("_render_Binds nexDatePicker to date range element", () => {
        // act
        dateRangePickerComponent.render();

        // assert
        expect($.fn.nexDatePicker).toHaveBeenCalledWith({
            datePickerFormat: {
                dateFormat: "YYYY-MM-DD"
            },
            selectedItem: 0,
            minDate: new Date("1970-01-01"),
            maxDate: new Date("2019-08-26"),
            currentDate: new Date("2019-08-26"),
            onSelectDatePicker: jasmine.any(Function)
        });
    });
});
