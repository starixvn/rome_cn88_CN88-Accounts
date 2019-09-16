interface Window {
    $: JQueryStatic;
}

class CN88Accounts {
    format = {
        shortDate_DatePicker: "YYYY-MM-DD"
    };

    defaultPageList = [10, 20, 50, 100];

    replaceQueryStringValue = (queryString: string, paramName: string, paramValue) => {
        if (typeof queryString === "undefined"
            || queryString === null) {
            return "";
        }

        const regex = new RegExp("([?&])" + paramName + "=[^&]*", "i");

        const newQueryString = regex.test(queryString)
            ? queryString.replace(regex, "$1" + paramName + "=" + paramValue)
            : queryString + (queryString.indexOf("?") >= 0 ? "&" : "?") + paramName + "=" + paramValue;

        return newQueryString;
    };

    convertAttributeToBool = (attrValue: string) => {
        return !!attrValue && attrValue.toString().toLowerCase() === "true";
    };
}

const cn88Accounts = new CN88Accounts();