function loadAutocomplete(inputName, url, onSelect) {
    var inputSelector = "#" + inputName;
    var loadingFunction;
    var options = {
        url: (search) => {
            loadingFunction(true);
            return url + "" + search;
        },
        theme: "bootstrap",
        getValue: data => {
            return data.nombre;
        },
        list: {
            onSelectItemEvent: onSelect,
            onLoadEvent: () => { loadingFunction(false); }
        }
    };
    $(inputSelector).easyAutocomplete(options);
    var elem = setLoading(inputName);
    loadingFunction = getEventActivationHandler(elem);
}
function setLoading(inputName) {
    var elem = document.getElementById("eac-container-" + inputName);
    var img = document.createElement("img");
    img.setAttribute('src', '/images/Ellipsis.svg');
    img.setAttribute('id', 'loading-' + inputName);
    img.style.display = "none";
    elem.appendChild(img);
    return img;
}
function getEventActivationHandler(element) {

    return function (value) {
        element.style.display = (value) ? "block" : "none";
    };
}