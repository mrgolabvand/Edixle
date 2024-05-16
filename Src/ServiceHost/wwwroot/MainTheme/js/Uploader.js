"use strict";
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/UploaderHub")
    .build();
this.connection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });

connection.on('ProgressBar',
    function (progress) {
        debugger;
        var div = document.getElementById('uploadFileDiv');
        if (progress < 100) {
            div.style.display = "block";
        } else {
            div.style.display = "none";
            history.go();
        }
        var progressBar = document.getElementById('progressBar');
        progressBar.style.width = progress + "%";
        progressBar.innerText = progress + "%";
    });

connection.on('ServerVideoProcess',
    function (isSuccess) {
        debugger;
        if (isSuccess === true) {
            alert("پردازش ویدیو انجام شد.");
        } else {
            alert("درحال پردازش ویدیو در سرور. ممکن است کمی طول بکشد.\n(پس از اتمام پردازش پیام دیگری نمایش داده میشود.)");
        }
    });
