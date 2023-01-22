var router = {
    _baseUrl: null,
    init: function (baseUrl) {
        this._baseUrl = baseUrl;
    },
    action: function (controller, action, queryParameters) {
        var url = router._baseUrl + "/" + controller + "/" + action;
        if (queryParameters) {
            url += "?";
            for (var key in queryParameters) {
                var value = queryParameters[key];
                if (value)
                    url += key + "=" + value + "&";
            }
            url = url.substring(0, url.length - 1);
        }
        return url;
    }
}