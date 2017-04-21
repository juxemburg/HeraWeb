
var xhr;
var autocom = new autoComplete({
    selector: 'input[name="Nombre"]',
    source: function (term, response) {
        try { xhr.abort(); } catch (e) { }
        xhr = $.get('/api/Cursos/autocomplete/' + term,
            (data) => { response(data); })
    },
    onSelect: function (e, term, item) {
        alert(item);
        alert(term);
        alert(e);
    }
});