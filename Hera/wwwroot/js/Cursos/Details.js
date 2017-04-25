


var options = {
    url: (search) => {
        return "/api/Autocomplete/desafios/" + search;
    },
    getValue: data => {
        return data.nombre;
    },
    list: {
        onSelectItemEvent: function () {
            var value = $("#input-desafioNombre").getSelectedItemData();

            $("#input-desafioId").val(value.id);
            document.getElementById('input-dirArchivo').value = '--';
            $("#input-desafioDificultad").val(value.dificultad);
        }
    }
};

$("#input-desafioNombre").easyAutocomplete(options);