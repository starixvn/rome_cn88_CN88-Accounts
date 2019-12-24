class DateRangePickerOption {
    constructor(
        public selectorId = "daterange-picker",
        public onChange: Function = null) {
        this.selectorId = selectorId;
        this.onChange = onChange;
    }
}

class DateRangePickerComponent {
    $: JQueryStatic;
    options = new DateRangePickerOption();

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    render = (options: DateRangePickerOption = new DateRangePickerOption()) => {
        this.options = this.$.extend({}, this.options, options);

        const dateRangePicker = this.$("#" + this.options.selectorId);
        const currentDate = dateRangePicker.data("currentdate");
        const minDate = dateRangePicker.data("mindate") === "0001-01-01" ? null : dateRangePicker.data("mindate");
        const maxDate = dateRangePicker.data("maxdate");

        dateRangePicker.nexDatePicker({
            datePickerFormat: {
                dateFormat: cn88Accounts.format.shortDate_DatePicker
            },
            selectedItem: dateRangePicker.data("selecteddaterange"),
            minDate: new Date(minDate),
            maxDate: maxDate === "" ? null : new Date(maxDate),
            currentDate: new Date(currentDate),
            onSelectDatePicker: () => {
                if (this.options.onChange) {
                    this.options.onChange();
                }
            }
        });

        return dateRangePicker;
    };
}
