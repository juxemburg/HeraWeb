window.onload = () => {
    var elem = document.getElementById('span-notification-count');
    var initialCount = 0;
    setInterval(() => {
        initialCount += 5;
        elem.innerText = ""+initialCount;
    }, 5000);
};