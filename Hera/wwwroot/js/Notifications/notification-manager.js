window.addEventListener("load", () => {
    LoadEvents();
});

function LoadEvents() {
    var elem = document.getElementById('span-notification-count');
    var icon = document.getElementById('i-notification-icon');
    var btn = document.getElementById('btn-notifications');
    $(elem).hide();

    var activateNotifications = (count) => {
        if (count) {
            $(elem).show();
            icon.innerText = 'notifications_active';
            elem.innerText = count;
        }
        else {
            icon.innerText = 'notifications_none';
            $(elem).hide();
        }
    };
    var chkNotifications = () => {
        $.post("/api/Notifications/Count", (data) => {
            activateNotifications(data.count);
        });
    };
    var loaded = false;
    var loadNotifications =
        getLoadNotificationsHandler(activateNotifications);
    btn.onclick = () => {
        loadNotifications();
    };

    chkNotifications();
    setInterval(chkNotifications, 15000);

}

function getLoadNotificationsHandler(activateNotifications) {
    var ul = document.getElementById('ul-notifications');
    var fStartLoad = (start) => {
        $(ul).empty();
        if (start)
            ul.appendChild(getLoadingli());
    };

    return () => {
        fStartLoad(true);
        $.post("/api/Notifications/Resume", (data) => {
            fStartLoad(false);
            activateNotifications(0);
            for (var i in data) {
                ul.appendChild(createNotificationli(data[i]));
            }
            ul.appendChild(createNotNotificationli());
            
        });

    };
}

function createNotificationli(notification) {
    var li = document.createElement('li');
    var a = document.createElement('a');
    a.setAttribute('href', '' + notification.action);
    a.appendChild(document.createTextNode(notification.message + '(' + notification.count + ')'));
    li.appendChild(a);

    return li;
}
function createNotNotificationli() {
    var li = document.createElement('li');
    var p = document.createElement('a');
    
    p.setAttribute('class', 'text-center');
    p.setAttribute('href','/Notifications')
    var i = document.createElement('i');
    i.setAttribute('class', 'material-icons');
    i.innerHTML = 'notifications';
    p.appendChild(i);
    p.appendChild(document.createTextNode('Todas las notificaciones'));
    li.appendChild(p);
    return li;
}

function getLoadingli() {
    var li = document.createElement('li');
    li.setAttribute('class', 'text-center');

    var img = document.createElement('img');
    img.setAttribute('src', '/images/Ellipsis.svg');
    li.appendChild(img);
    return li;
}


