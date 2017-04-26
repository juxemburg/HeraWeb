function loadAutocomplete(inputName, url, onSelect) {
    var inputSelector = "#" + inputName;
    var options = {
        url: (search) => {
            return url + "" + search;
        },

        theme: "bootstrap",

        getValue: data => {
            return data.nombre;
        },
        list: {

            onSelectItemEvent: onSelect
        }
    };
    $(inputSelector).easyAutocomplete(options);
}