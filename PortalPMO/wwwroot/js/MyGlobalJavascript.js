//Global Function untuk get Partial View
function LoadPartialViewData(urlController, callback, SendData) {
    //console.log(SendData);
    $.ajax({
        type: 'GET',
        url: urlController,
        data: SendData,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            callback(data);
        }
    });
}






