window.onload = function () {
    var botones = document.getElementsByClassName('btn-item');
    for (var i in botones) {
        botones[i].onclick = function (e) {
            var target = e.target;
            var input = document.getElementById('input-curso');
            input.value = target.id;
        }
    }
}