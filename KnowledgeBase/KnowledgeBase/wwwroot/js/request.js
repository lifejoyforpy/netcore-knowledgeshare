﻿//const Retry = require('./retry.js');
//const Retry = new Retry();
//const HOST = `${config.service.host}`;

/*
 *    REST API
 */
//export
const request = (method = 'GET') => (url, data = {}, options = {}) => {
    $("#fakeLoader").fakeLoader({ spinner: "spinner2" });
    if (options.toast) {
        //wx.showToast({
        //    title: options.toast.title || '加载中',
        //    icon: 'loading',
        //    duration: options.toast.duration || 100000,
        //})
    }
    let contentType = '';
    contentType = method == 'POST' ? 'application/x-www-form-urlencoded' : 'application/json';
    let cacheKey;
    return new Promise((resolve, reject) => {
        let retry = new Retry({
            timeout: 20000,           // 超时时间，默认不会超时
            interval: 200,           // 每次执行的时间间隔，默认是0
            max: options.max || 3,        // 最大重试次数，默认是无穷大
            done: function (data) {
                // 成功
                $("#fakeLoader").fakeHide();
                resolve(data);
            },
            fail: function (err) {
                // 失败
                $("#fakeLoader").fakeHide();
                reject(err);
            }
        });

        retry.start((done, retryfun) => {
            $.ajax({
                url: url, //仅为示例，并非真实的接口地址
                data: data,
                method: method,
                contentType: contentType,
                success: function (res) {
                    if (!res.status || res.status == 0) {
                        retry.stop();//停止重试
                        $("#fakeLoader").fakeHide();
                        reject(res.Msg);
                    }
                    else {
                        done(res.data);
                    }
                },
                error: function (err) {
                    retryfun();
                },
                complete() {
                    console.log('request complete');
                }
            })
        })
    })
}

//const GET = request('GET');
//const POST = request('POST');
//const PUT = request('PUT');
//const DEL = request('DELETE');
var API = {
    GET: request('GET'),
    POST: request('POST')
}

/**
 * 上传方法
 * filePath: 上传的文件路径
 * fileName： 上传到cos后的文件名
 */

//const UPLOAD = (filePath) => {
//    const cosUrl = `${HOST}/api/source/uploadFile`;
//    return new Promise((resolve, reject) => {
//        // 鉴权获取签名
//        // 头部带上签名，上传文件至COS
//        wx.uploadFile({
//            url: cosUrl,
//            filePath: filePath,
//            header: {
//                "Content-Type": "multipart/form-data"
//            },
//            name: 'uploadfile_ant',
//            formData: { op: 'upload' },
//            success: (uploadRes) => {
//                console.log(uploadRes);
//                if (JSON.parse(uploadRes.data).flag == 1) {
//                    resolve(JSON.parse(uploadRes.data));
//                }
//                else {
//                    reject(JSON.parse(uploadRes.data));
//                }

//                // do something
//            },
//            fail: (err) => {
//                console.error(err);
//                reject(err);
//            }
//        });
//    })
//}

//export default {
//    GET,
//    PUT,
//    POST,
//    DEL
//};