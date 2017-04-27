
loadAutocomplete('input-desafioNombre',
    "/api/Autocomplete/desafios/", () => {
        var value = $("#input-desafioNombre").getSelectedItemData();

        $("#input-desafioId").val(value.id);
        $("#input-desafioDescripcion").val(value.descripcion);
        $("#input-desafioDificultad").val(value.dificultad);
    });



