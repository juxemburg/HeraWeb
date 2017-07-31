
function createSvg(series) {
    var ns = "http://www.w3.org/2000/svg";
    var svg = document.createElementNS(ns,'svg');
    svg.setAttributeNS(null,'width', '100%');
    svg.setAttributeNS(null,'height', '100%');
    var x = 20;
    var y = 20;
    var n = 0;
    for (var i in series) {
        svg.appendChild(createCircle(ns,n, x, y));
        svg.appendChild(createCircleText(ns,series[i], x, y));
        n++;
        x += 120;
    }
    return svg;
}
function createCircle(ns,n, x, y) {
    
    var chr = String.fromCharCode(97 + n);
    var clss = "ct-legend-" + chr;

    var circle = document.createElementNS(ns,'circle');
    circle.setAttributeNS(null, 'class', clss);
    circle.setAttributeNS(null, 'r', '10');
    circle.setAttributeNS(null, 'cx', '' + x);
    circle.setAttributeNS(null, 'cy', '' + y);
    return circle;
    
}
function createCircleText(ns,legend, x, y) {
    var text = document.createElementNS(ns,'text');
    text.setAttributeNS(null,'class', 'ct-label ct-legend-label');
    text.setAttributeNS(null,'x', '' + (x + 20));
    text.setAttributeNS(null,'y', '' + (y + 6));
    text.appendChild(document.createTextNode(legend));
    return text;
}

function CtExtension(selector) {
    var elem = $(selector);
    $(elem).height(60);
    this.Legend = (series) => {
        $(elem).empty();
        var svg = createSvg(series);
        $(elem).append(svg);
    };

    return this;
}