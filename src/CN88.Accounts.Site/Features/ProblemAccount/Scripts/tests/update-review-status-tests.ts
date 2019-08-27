describe("UpdateReviewStatus", () => {
    const jQueryFake = $;
    const topWindow = window.top;
    const message = new Message();
    let updateReviewStatus: UpdateReviewStatus;

    beforeEach(() => {
        setFixtures(`<div id="message-container" class="message-container"></div>
                    <form id="formUpdateReviewStatus" action="/site-accounts/(S(vhfwrkn04fojn4gnndzxiqpk))/ProblemAccount/UpdateReviewStatus" method="post" class="form-horizontal">
                        <input id="CustLevelId" name="CustLevelId" type="hidden" value="3">
                        <input id="SelectedDownlineIds" name="SelectedDownlineIds" type="hidden" value="35709671,35709674">
                        <div class="list-button">
                            <input type="submit" class="btn-submit" value="Submit">
                            <input type="button" class="btn-cancel" id="btnCancel" value="Cancel">
                        </div>
                    </form>
                    <input id="SuccessMessage" name="SuccessMessage" type="hidden" value="Update Successfully!">`);
        updateReviewStatus = new UpdateReviewStatus(jQueryFake, topWindow, message);
    });

    describe("_init", () => {
        beforeEach(() => {
            // arrange
            spyOn(updateReviewStatus, "cacheDOMElements").and.callThrough();
            spyOn(updateReviewStatus, "bindEvents");

            // act
            updateReviewStatus.init();
        });

        it("_Calls cacheDOMElements", () => {
            // assert
            expect(updateReviewStatus.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Calls bindEvents", () => {
            // assert
            expect(updateReviewStatus.bindEvents).toHaveBeenCalled();
        });
    });

    describe("_cacheDOMElements", () => {
        it("_Always -> All neccessary elements have been cached", () => {
            // act
            updateReviewStatus.cacheDOMElements();

            // assert
            expect(updateReviewStatus.$messageContainer).toBeDefined();
            expect(updateReviewStatus.$formUpdateReviewStatus).toBeDefined();
            expect(updateReviewStatus.$btnCancel).toBeDefined();
            expect(updateReviewStatus.successMessage).toBeDefined();
        });
    });
});
