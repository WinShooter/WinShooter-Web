window.onerror = function (errorMsg, url, lineNumber, column, errorObj) {

    if (arguments.callee.caller) {
        errorObj += arguments.callee.caller;
    }

    $.post("/JsErrorLogger",
            {
                error: errorMsg,
                url: url,
                line: lineNumber,
                column: column,
                stacktrace: errorObj
            }
    );
}