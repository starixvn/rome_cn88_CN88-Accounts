describe("Message", () => {
    let result: Result;
    const message = new Message();
    let $target: JQuery;

    beforeEach(() => {
        setFixtures(`<div id="message-container"></div>`);

        spyOn(message, "showSuccess").and.callThrough();
        spyOn(message, "showError").and.callThrough();
        spyOn(message, "showWarn").and.callThrough();

        $target = $("#message-container");
    });

    describe("_showMessage", () => {
        it("_ResultIsSuccessful_CallsShowSuccess", () => {
            // arrange
            result = new Result(0, "Success message");

            // act
            message.showMessage($target, result);

            // assert
            expect(message.showSuccess).toHaveBeenCalledWith($target, "Success message");
        });

        it("_ResultIsWarning_CallsShowWarn", () => {
            // arrange
            result = new Result(-1000, "Warning message");

            // act
            message.showMessage($target, result);

            // assert
            expect(message.showWarn).toHaveBeenCalledWith($target, "Warning message");
        });

        it("_ResultIsFailed_CallsShowError", () => {
            // arrange
            result = new Result(-1, "Error message");

            // act
            message.showMessage($target, result);

            // assert
            expect(message.showError).toHaveBeenCalledWith($target, "Error message");
        });
    });
});
