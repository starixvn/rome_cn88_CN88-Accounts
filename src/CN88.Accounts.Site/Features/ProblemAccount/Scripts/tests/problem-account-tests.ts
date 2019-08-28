describe("ProblemAccount", () => {
    const jQueryFake = $;
    const topWindow = window.top;
    const dateRangePicker = new DateRangePickerComponent($);
    const reviewStatusDropdown = new SingleSelectionDropdownComponent($);
    const pager = new PagerComponent($);

    let problemAccount: ProblemAccount;

    const template = `<form class="form-inline filter" role="form" id="report-form">
                        <input id="CustLevelId" name="CustLevelId" type="hidden" value="3">
                        <div id="daterange-picker" class="daterange-picker" data-currentdate="8/28/2019 12:00:00 AM" data-mindate="0001-01-01" data-maxdate="2019-08-28">
                        </div>
                        <div class="form-group from-date-box">
                            <label>Date Notified From</label>
                            <div class="control date">
                                <div id="from-date-container" class="input-group date fromdate">
                                    <input type="text" id="from-date" name="FromDate" readonly="readonly" autocomplete="off" class="ui_nexdatepicker_datetextbox ui_nexdatepicker_fromdate form-control" value="2019-08-27">
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
                                    <input type="text" id="to-date" name="ToDate" readonly="readonly" autocomplete="off" class="ui_nexdatepicker_datetextbox ui_nexdatepicker_todate form-control" value="2019-08-28">
                                    <span class="input-group-addon">
                                        <span class="icon-calendar" id="tdate_trigger"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Review Status</label>
                            <select id="dropdown-review-status" class="hide">
                                    <option value="2">All</option>
                                    <option value="1">Reviewed</option>
                                    <option value="0" selected="&quot;selected&quot;">Not Reviewed</option>
                            </select><div class="btn-group"><button type="button" class="multiselect dropdown-toggle btn btn-default" data-toggle="dropdown" title="Not Reviewed"><span class="multiselect-selected-text">Not Reviewed</span> <b class="caret"></b></button><ul class="multiselect-container dropdown-menu" style="max-height: 300px; overflow: hidden auto;"><li><a tabindex="0"><label class="radio"><input type="radio" value="2"> All</label></a></li><li><a tabindex="0"><label class="radio"><input type="radio" value="1"> Reviewed</label></a></li><li class="active"><a tabindex="0"><label class="radio"><input type="radio" value="0"> Not Reviewed</label></a></li></ul></div>
                            <input type="hidden" name="SelectedReviewStatus" id="SelectedReviewStatus" value="0">
                        </div>
                        <div class="form-group">
                            <label>Username, Nickname Or Name contains</label>
                            <input type="text" id="SearchUsername" name="SearchUsername" class="control search-username">
                        </div>
                        <div class="form-group">
                            <input type="submit" class="btn-submit" value="Submit">
                        </div>
                    <input type="hidden" id="PageIndex" name="PageIndex" value="undefined"><input type="hidden" id="PageSize" name="PageSize" value="undefined"></form>
                    <form class="hide" role="form" id="export-excel-form" action="/site-accounts/(S(okwrpc1qrzbesnwvqjlcnlkb))/ProblemAccount/Excel">
                        <input id="CustLevelId" name="CustLevelId" type="hidden" value="3">
                        <input id="FromDate" name="FromDate" type="hidden" value="2019-08-27">
                        <input id="ToDate" name="ToDate" type="hidden" value="2019-08-28">
                        <input id="SelectedReviewStatus" name="SelectedReviewStatus" type="hidden" value="0">
                        <input id="SearchUsername" name="SearchUsername" type="hidden" value="">
                    </form>
                    <input type="button" id="btnReview" name="btnReview" class="btn-submit btn-review" value="Set to Reviewed">
                    <table id="tblProblemAccount" class="tbl-style2 table-problem-account">
                        <thead>
                            <tr>
                                <th class="col-no">No.</th>
                                <th class="col-username">Username</th>
                                <th class="col-name">Name</th>
                                <th class="col-nickname">Nickname</th>
                                <th class="col-numberofaccounts text-center">Problem Agent Accounts <span class="icon-info" data-toggle="tooltip" title="" data-original-title="Problem Agent Accounts are the newly marked ones in the selected report period."></span></th>
                                <th class="col-status text-center">Status</th>
                                <th class="col-datenotified">Date Notified</th>
                                <th class="col-reviewstatus">Review Status</th>
                                <th class="text-center col-review-action">
                                    <input type="checkbox" id="SelectAll" name="SelectAll" value="">
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="9" data-isnoinfo="true" align="center">No information is available</td>
                            </tr>
                        </tbody>
                    </table>
                    <input id="UpdateStatus" name="UpdateStatus" type="hidden" value="Update Status">
                    <input id="SetReviewedStatus" name="SetReviewedStatus" type="hidden" value="Set Reviewed Status">
                    <input id="OK" name="OK" type="hidden" value="OK">
                    <input id="SystemMessage" name="SystemMessage" type="hidden" value="System Message">
                    <input id="SetToReviewedValidationMessage" name="SetToReviewedValidationMessage" type="hidden" value="You do not have any Problem Accounts selected. Please select a Problem Account or select &quot;All&quot;">
                    <input id="UpdateReviewStatusUrl" name="UpdateReviewStatusUrl" type="hidden" value="ProblemAccount/UpdateReviewStatus">`;

    beforeEach(() => {
        problemAccount = new ProblemAccount(jQueryFake, topWindow, dateRangePicker, reviewStatusDropdown, pager);

        spyOn(jQueryFake, "blockUI").and.callThrough();
        spyOn(jQueryFake, "unblockUI").and.callThrough();
        spyOn(jQueryFake.fn, "tooltip").and.callThrough();
        spyOn(problemAccount, "cacheDOMElements").and.callThrough();
        spyOn(problemAccount, "renderComponents").and.callThrough();
        spyOn(problemAccount, "bindEvents").and.callThrough();
        spyOn(topWindow["popupManager"], "init").and.callThrough();

        //setFixtures(template);
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
            expect(problemAccount.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Calls renderComponents", () => {
            // assert
            expect(problemAccount.renderComponents).toHaveBeenCalled();
        });

        it("_Calls bindEvents", () => {
            // assert
            expect(problemAccount.bindEvents).toHaveBeenCalled();
        });

        it("_Calls unblockUI", () => {
            // assert
            expect(jQueryFake.unblockUI).toHaveBeenCalled();
        });
    });

    it("_cacheDOMElements_Always_All neccessary elements have been cached", () => {
        // act
        setFixtures(template);
        problemAccount.cacheDOMElements();

        // assert
        expect(problemAccount.$tooltips).toBeDefined();
        expect(problemAccount.$reportForm).toBeDefined();
        expect(problemAccount.$updateStatusLink).toBeDefined();
        expect(problemAccount.$exportExcelForm).toBeDefined();
        expect(problemAccount.$excelIcon).toBeDefined();
        expect(problemAccount.$btnReview).toBeDefined();
        expect(problemAccount.$checkboxSelectAll).toBeDefined();
        expect(problemAccount.$downlineCheckboxs).toBeDefined();

        expect(problemAccount.resources.ok).toBeDefined();
        expect(problemAccount.resources.updateStatus).toBeDefined();
        expect(problemAccount.resources.setReviewedStatus).toBeDefined();
        expect(problemAccount.resources.systemMessage).toBeDefined();
        expect(problemAccount.resources.setToReviewedValidationMessage).toBeDefined();
        expect(problemAccount.custLevelId).toBeDefined();
        expect(problemAccount.updateReviewStatusUrl).toBeDefined();
    });
});
