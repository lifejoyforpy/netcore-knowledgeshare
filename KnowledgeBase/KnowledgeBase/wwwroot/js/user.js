
var getUserinfo = function () {
    var userdata = [];
    $.ajax({
        type: 'POST',
        url: '/Home/getUserinfo',
        success: function (result) {
            if (result.status == 1)
                userdata = result.data;
        }
    });
    return userdata;
}
var getDepartList = function () {
    var departdata = [];
    $.ajax({
        type: 'POST',
        url: '/Home/getDepartList',
        success: function (result) {
            if (result.status == 1)
                departdata = result.data;
        }
    });
    return departdata;
}
var getFactoryList = function () {
    var factorydata = [];
    $.ajax({
        type: 'POST',
        url: '/Home/getFactoryList',
        success: function (result) {
            if (result.status == 1)
                factorydata = result.data;
        }
    });
    return factorydata;
}

var User = {
getUserinfo: getUserinfo,
getDepartList: getDepartList,
getFactoryList: getFactoryList
}