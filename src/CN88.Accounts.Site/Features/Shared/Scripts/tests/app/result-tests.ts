describe("Result", () => {
    let result: Result;

    describe("_Constructor", () => {
        it("_CodeIsZero_ResultIsSuccessful", () => {
            // act
            result = new Result(0, "");

            // assert
            expect(result.IsSuccessful).toBeTruthy();
        });

        it("_CodeIsNotZero_ResultIsFalse", () => {
            // act
            result = new Result(-1, "");

            // assert
            expect(result.IsFailed).toBeTruthy();
        });

        it("_CodeIsWarningCode_ResultIsWarning", () => {
            // act
            result = new Result(-1000, "");

            // assert
            expect(result.IsWarning).toBeTruthy();
        });
    });
});
