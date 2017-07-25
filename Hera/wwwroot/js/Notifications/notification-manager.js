window.addEventListener("load", () => {
    var elem = document.getElementById('span-notification-count');
    var icon = document.getElementById('i-notification-icon');
    $(elem).hide();
    var initialCount = 0;
    var activeNotifications = (count) => {
        if (count) {
            $(elem).show();
            icon.innerText = 'notifications_active';
            elem.innerText = count;
        }
        else
            $(elem).hide();
    };

    var chkNotifications = () => {
        $.post("/api/Notifications/Count", (data) => {
            activeNotifications(data.count);
        });
    };
    chkNotifications();
    setInterval(chkNotifications, 15000);
});




//initialCount += 5;
//elem.innerText = "" + initialCount;