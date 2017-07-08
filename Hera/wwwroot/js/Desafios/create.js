window.onload = () => {
    var elemInput = document.getElementById('input-idSolucion');
    elemInput.onchange = (e) => {
        var target = e.target;
        var text = target.value;
        setLoading('div-loading')
        var uri = "/api/Desafios/GeneralValoration/" + text;
        $.get(uri, (data) => {
            $('#div-idSolucion').addClass('has-success');
            mapValoration(data);
            stopLoading('div-loading');
        }).fail((data) => {
            $('#div-idSolucion').removeClass('has-success');
            alert("Error: " + data.responseText);
            target.value = "";
            stopLoading('div-loading');
        });
    };

    document.getElementById('UrlSolucion').onchange =
        (e) => {
            var target = e.target;
            var str = target.value;
            var projId = str.substring(
                str.lastIndexOf("/") + 1, str.length);
            if (projId) {
                elemInput.value = projId;
                $(elemInput).trigger("change");
            }
        };
};



function mapValoration(valoration) {
    for (var prop in valoration) {
        var id = "#" + jsUcfirst(prop);
        $(id).prop('checked', valoration[prop]);
    }
}
function jsUcfirst(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

