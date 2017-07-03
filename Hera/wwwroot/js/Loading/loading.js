

function setLoading(idSpinner) {
    toggleDisplay(idSpinner, 'block');

}
function stopLoading(idSpinner) {
    toggleDisplay(idSpinner, 'none');
}

function toggleDisplay(idElement, action) {
    var elem = document.getElementById(idElement);
    elem.style.display = action;
}