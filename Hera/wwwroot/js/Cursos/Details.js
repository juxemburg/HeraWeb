
loadAutocomplete('input-desafioNombre',
    "/api/Autocomplete/desafios/", () => {
        var value = $("#input-desafioNombre").getSelectedItemData();

        $("#input-desafioId").val(value.id);
        $("#input-desafioDescripcion").val(value.descripcion);
        $("#input-desafioAutor").val(value.autor);
        $("#div-lnk-desafio").show();
        $("#lnk-desafio").attr("href", "/Desafios/Details/" + value.id);
    });



