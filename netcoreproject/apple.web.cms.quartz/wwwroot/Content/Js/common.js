//兼容ajaxIE浏览器下相同地址缓存问题
$.ajaxSetup({ cache: false });

function GetCodeFromUrl(NewUrl) {
    var start = NewUrl.lastIndexOf("/");
    var end = NewUrl.lastIndexOf(".");
    return NewUrl.substring(start + 1, end)
}
var vanim = 0;//动画效果
var WKFXCms = {
    ValidateIsNull: function (value) {//验证是否为空
        if (typeof value == "undefined" || value == null || value == "") {
            return false;
        } else {
            return true;
        }
    },
    ValidateReg: function (reg, val) {//正则匹配验证
        if (!reg.test(val))
            return false;
        else
            return true;
    },
    Form: function (verifydata, callback) {
        layui.use(['form'], function () {
            var form = layui.form;
            //验证是否为空
            form.verify(verifydata);
            //调用回调函数
            if (callback) {
                callback(form);
            }
        });
    },
    RenderForm: function () {
        layui.use(['form'], function () {
            var from = layui.form;
            from.render();
        });
    },//确认框
    Confirm: function (msg, callback, cancelback, closeback) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.confirm(msg, {
                title: '系统提示', zindex: 100, cancel: function (index, layero) {
                    if (closeback)
                        closeback(index);
                }
            }, function () {
                if (callback) {
                    callback(100);
                }
            }, function () {
                if (cancelback) {
                    cancelback(100);
                }
            });
        });
    },
    Prompt: function (type, startStr, title, area, callback) {////输入框类型，支持0（文本）默认1（密码）2（多行文本）
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.prompt({
                formType: type,
                value: startStr,
                title: title,
                area: area//自定义文本域宽高
            }, function (value, index, elem) {
                callback(value, index);

            });
        });
    },//提示框
    Alert: function (msg, callback) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.alert(msg, { title: '系统提示', zindex: 999, }, function (index) {
                if (callback)
                    callback(index);
            });
        });
    },//加载进度条
    Load: function (type) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.load(type);
        });
    },//信息提示
    Msg: function (msg, callback) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.msg(msg, { time: 800, zIndex: 9999 });

            if (callback) {
                callback();
            }
        });
    },//抖动消息提示
    Msg1: function (msg, icon) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.msg(msg, { icon: icon, shift: 6 });
        });
    },//推送消息
    MsgPush: function (msg, callback) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.open({
                type: 1,
                title: false,
                closeBtn: 0, //不显示关闭按钮
                shade: 0,
                area: ['180px', '50px'],
                offset: 'b', //右下角弹出
                time: 5000, //2秒后自动关闭
                anim: 6,
                moveType: 0,
                content: '<a href="javascript:vmessage();"><div id="" class="layui-layer-content" style="height: 40px;white-space: nowrap;"><img id="layui-layim-min" src="../Images/timg1.png" class="layui-nav-img" style="cursor: move;height: 40px;width:40px;margin:5px 10px;"><span style="font-size:15px;">' + msg + '</span></div></a>', //弹出的信息写在这个页面
                end: function () {
                    //结束后操作
                    if (callback)
                        callback();
                }
            });
        });
    },//tips层
    Tips: function (title, idorclass, num) {
        layer.tips(title, idorclass, { tips: [num, '#FF5722'] });
    },//打开满屏页面
    Open: function (id, url, title, btn, callback, callbackYes) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            var index = layer.open({
                id: id, //设定一个id，防止重复弹出,保证唯一
                type: 2,
                title: title,
                btn: btn,
                btnAlign: 'r',
                content: url,
                area: [$(window).width() + 'px', $(window).height() + 'px'],
                maxmin: false,
                anim: vanim,
                success: function (layero, index) {
                    setTimeout(function () {
                        layer.tips('点击此处返回列表', '.layui-layer-setwin .layui-layer-close', {
                            tips: 2
                        });
                    }, 500);
                    //调用回调函数
                    if (callback) {
                        callback(layero, index);
                    }
                },
                yes: function (index, layero) {
                    if (callbackYes) {
                        callbackYes(index, layero);
                    }
                },
                end: function () {
                    var iurl = $(window.parent.document).find("iframe").attr("src");
                    if (WKFXCms.ValidateIsNull(iurl)) {
                        var path = iurl.indexOf('?') != -1 ? iurl.split('?')[0] : iurl;
                        WKFXCms.AjaxGetAsync("/Common/IsShowArea", "path=" + path, function (result) {
                            $(window.top.document).find("#divArea").css("display", parseInt(result) == 1 ? "block" : "none");
                        });
                    }
                }
            });
            layer.full(index);
        });
    },
    JsonOpen: function (obj) {//打开json修改弹框
        layui.use(['layer'], function () {
            var layer = layui.layer;
            $(obj).addClass("edit-bg-css");
            var vdata = $.trim($(obj).text());
            var vhtml = '<textarea placeholder="请输入内容" class="layui-textarea neweditdata" onfocus="true">' + vdata.substring(1, vdata.length - 1) + '</textarea>';
            var index = layer.open({
                type: 1,
                skin: 'layui-layer-rim', //加上边框
                area: ['420px', '200px'], //宽高
                anim: 2,
                btn: ['保存', '取消'],
                content: vhtml,
                yes: function () {
                    $(obj).html('"' + $.trim($(".neweditdata").val()) + '<i class="layui-icon layui-icon-edit"></i>"').addClass("edit-bg-css-yellow");
                    layer.closeAll();
                },
                end: function () {
                    $(obj).removeClass("edit-bg-css");
                }
            });
        });
    },//右下角弹框
    OpenRight: function (content, color, times, callback) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.open({
                type: 1,
                title: '<i class="layui-icon">&#xe645;</i>消息 <span id="divTime" style="color:#999;">' + times / 1000 + '</span>',
                closeBtn: 1, //不显示关闭按钮
                shade: 0,
                area: ['340px', '200px'],
                offset: 'rb', //右下角弹出
                time: times, //2秒后自动关闭
                anim: 2,
                moveType: 0,
                content: '<div style="padding:20px;color:#' + color + '">' + content + '</div>', //弹出的信息写在这个页面
                end: function () {
                    //结束后操作
                    if (callback)
                        callback();
                }
            });
        });
    },//Ajax 同步Post
    AjaxPost: function (url, data, okcallback, beforecallback, errorcallback) {
        $.ajax({
            url: url,
            type: "POST",
            async: false,
            data: data,
            success: function (res) {
                layer.closeAll('loading');
                if (okcallback) {
                    okcallback(res);
                }
            },
            beforeSend: function () {
                layer.load(2);
                if (beforecallback) {
                    beforecallback();
                }
            },
            error: function (e) {
                layer.closeAll('loading');

                if (errorcallback) {
                    errorcallback();
                } else {
                    WKFXCms.Msg("系统繁忙，请稍候再试~");
                }
            }
        });
    },//Ajax 异步Post
    AjaxPostAsync: function (url, data, okcallback, beforecallback, errorcallback) {
        $.ajax({
            url: url,
            type: "POST",
            async: true,
            data: data,
            success: function (res) {
                if (okcallback) {
                    okcallback(res);
                }
            },
            beforeSend: function () {
                if (beforecallback) {
                    beforecallback();
                }
            },
            error: function (e) {
                if (errorcallback) {
                    errorcallback();
                } else {
                    WKFXCms.Msg("系统繁忙，请稍候再试~");
                }
            }
        });
    },//Ajax 同步Get
    AjaxGet: function (url, data, okcallback, beforecallback, errorcallback) {
        $.ajax({
            url: url,
            type: "GET",
            async: false,
            data: data,
            success: function (res) {
                if (okcallback) {
                    okcallback(res);
                }
            },
            beforeSend: function () {
                if (beforecallback) {
                    beforecallback();
                }
            },
            error: function (e) {
                if (errorcallback) {
                    errorcallback();
                } else {
                    WKFXCms.Msg("系统繁忙，请稍候再试~");
                }
            }
        });
    },//Get 异步请求
    AjaxGetAsync: function (url, data, okcallback, beforecallback, errorcallback) {
        $.ajax({
            url: url,
            type: "GET",
            async: true,
            data: data,
            success: function (res) {
                if (okcallback) {
                    okcallback(res);
                }
            },
            beforeSend: function () {
                if (beforecallback) {
                    beforecallback();
                }
            },
            error: function (e) {
                if (errorcallback) {
                    errorcallback();
                } else {
                    WKFXCms.Msg("系统繁忙，请稍候再试~");
                }
            }
        });
    },
    AjaxJson: function (url, type, data, okcallback, beforecallback, errorcallback) {
        $.ajax({
            url: url,
            type: type,
            dataType: "json",
            async: false,
            //contentType: "application/json",
            data: data,
            beforeSend: function () { if (beforecallback) { beforecallback(); } },
            success: function (res) { if (okcallback) okcallback(res); },
            error: function (e) {
                if (errorcallback) { errorcallback(); } else { WKFXCms.Msg("系统繁忙，请稍候再试~"); }
            }
        });
    },//弹窗(两者选择其一)
    OpenPart: function (type, id, url, title, wh, btn, callback, callbackYes) {
        layer.open({
            type: type
            , title: title //不显示标题栏
            , area: wh
            , id: id //设定一个id，防止重复弹出,保证唯一
            , btn: btn
            , btnAlign: 'r'
            , moveType: 1 //拖拽模式，0或者1
            , content: url
            , anim: vanim
            , maxmin: true
            , success: function (layero, index) {
                if (callback)
                    callback(layero, index);
            },
            yes: function (index, layero) {
                if (callbackYes) {
                    callbackYes(index, layero);
                }
                //layer.close(index); //如果设定了yes回调，需进行手工关闭
            }
        });
    },
    //打开满屏页面
    OpenFull: function (url, title, zindex, callback) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            var index = layer.open({
                type: 2,
                title: title,
                content: url,
                area: [$(window).width() + 'px', $(window).height() + 'px'],
                zIndex: zindex,
                anim: vanim,
                success: function (layero, index) {
                    setTimeout(function () {
                        layer.tips('点击此处返回列表', '.layui-layer-setwin .layui-layer-close', {
                            tips: 2
                        });
                    }, 500);

                    //调用回调函数
                    if (callback) {
                        callback(layero, index);
                    }
                }
            });
            layer.full(index);
        });
    },
    ParentOpenPart: function (type, id, url, title, wh, btn, callback, callbackYes) {
        parent.layer.open({
            type: type
            , title: title //不显示标题栏
            , area: wh
            , id: id //设定一个id，防止重复弹出,保证唯一
            , btn: btn
            , btnAlign: 'r'
            , moveType: 1 //拖拽模式，0或者1
            , content: url
            , anim: vanim
            , maxmin: true
            , success: function (layero, index) {
                if (callback)
                    callback(layero, index);
            },
            yes: function (index, layero) {
                if (callbackYes) {
                    callbackYes(index, layero);
                }
                //layer.close(index); //如果设定了yes回调，需进行手工关闭
            }
        });
    },//关闭层(弹出窗是新的页面的时候)
    CloseFrame: function () {
        //变量不允许使用name,会导致查找不到弹框
        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.close(index);
    },//在当前页中关闭弹窗
    Close: function (index) {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.close(index);
        });
    },//删除所有提示信息
    CloseAll: function () {
        layui.use(['layer'], function () {
            var layer = layui.layer;
            layer.closeAll();
        });
    },//子页面关闭时再父页面提示信息
    ParentMsg: function (msg) {
        parent.layer.msg(msg);
    },//子页面关闭时刷新父页面table
    ParentLoad: function (tableId) {
        parent.layui.table.reload(tableId, { page: { curr: parent.$(".layui-laypage-curr").text() } });
    },
    ParentsLoad: function (tableId) {
        parent.parent.layui.table.reload(tableId, { page: { curr: parent.parent.$(".layui-laypage-curr").text() } });
    },
    ToDateString: function (d, format) {
        if (!format)
            format = "yyyy-MM-dd HH:mm:ss";
        return toDateString(d, format);
    },
    //时间格式转换
    ToDate: function (date, format) {
        if (!format)
            format = "yyyy-MM-dd hh:mm:ss";
        if (date == null)
            return "";
        var mydate = new Date(date);
        //mydate.setHours(mydate.getHours() + 8);
        return mydate.format(format);
    },
    ToDayDate: function (date, format) {
        if (!format)
            format = "yyyy.MM.dd";
        if (date == null)
            return "";
        var mydate = new Date(date);
        //mydate.setHours(mydate.getHours() + 8);
        return mydate.format(format);
    },
    ToDayDateFormat: function (date, format) {
        if (!format)
            format = "yyyy-MM-dd";
        if (date == null)
            return "";
        var mydate = new Date(date);
        //mydate.setHours(mydate.getHours() + 8);
        return mydate.format(format);
    },
    //开始日期，结束日期
    DateDiffIncludeToday: function (startDateString, endDateString) {
        var separator = "-"; //日期分隔符
        var startDates = startDateString.split(separator);
        var endDates = endDateString.split(separator);
        var startDate = new Date(startDates[0], startDates[1] - 1, startDates[2]);
        var endDate = new Date(endDates[0], endDates[1] - 1, endDates[2]);
        return parseInt((endDate - startDate) / 1000 / 60 / 60 / 24) + 1;//把相差的毫秒数转换为天数
    },
    //上传图片
    UploadImg: function (elem, url, size, exts, donecallback) {
        layui.use('upload', function () {
            var upload = layui.upload;
            //设定文件大小限制
            upload.render({
                elem: elem
                , url: url
                , size: size //限制文件大小，单位 KB
                , exts: exts//'jpg|png|gif|jpeg'
                , before: function (obj) {
                    layer.load(); //上传loading
                }
                , done: function (res) {
                    layer.closeAll('loading');
                    if (donecallback)
                        donecallback(res);
                },
                error: function () {
                    layer.closeAll('loading');
                    WKFXCms.Msg("上传失败，请重试");
                }
            });
        });
    },//多图片上传
    UploadMulImg: function (elem, url, size, exts, donecallback) {
        layui.use('upload', function () {
            var upload = layui.upload;
            //设定文件大小限制
            upload.render({
                elem: elem
                , url: url
                , size: size //限制文件大小，单位 KB
                , exts: exts//'jpg|png|gif|jpeg'
                , multiple: true //是否多文件
                , before: function (obj) {
                    layer.load(); //上传loading
                }
                , done: function (res) {
                    layer.closeAll('loading');
                    if (donecallback)
                        donecallback(res);
                },
                error: function () {
                    layer.closeAll('loading');
                    WKFXCms.Msg("上传失败，请重试");
                }
            });
        });
    },//上传文件
    UploadFile: function (id, url, exts, size, donecallback) {
        layui.use('upload', function () {
            var upload = layui.upload;
            upload.render({
                elem: id
                , url: url
                , accept: 'file'
                , size: size //限制文件大小，单位 KB
                , exts: exts
                , choose: function (obj) {
                    obj.preview(function (index, file, result) {

                    });
                }
                , done: function (res, index, upload) {
                    layer.closeAll('loading');
                    if (donecallback)
                        donecallback(res);
                },
                before: function (obj) {
                    layer.load(); //上传loading
                },
                error: function (err) {
                    layer.closeAll('loading');
                    WKFXCms.Msg("上传失败，请重试");
                }
            });
        });
    },//保留两位小数，不带符号
    TwoDecimalNoMark: function (value) {
        return twoDecimal(value);
    },//时间转时间戳
    DateToString: function (date) {
        return dateToString(date);
    },//时间格式转换
    ToDate: function (date, format) {
        if (date == null)
            return "";
        if (!format)
            format = "yyyy-MM-dd hh:mm:ss";
        var mydate = new Date(date);
        //mydate.setHours(mydate.getHours() + 8);
        return mydate.format(format);
    },//去除所有空格
    Trim: function (str, is_global) {
        var result;
        result = str.replace(/(^\s+)|(\s+$)/g, "");
        if (is_global.toLowerCase() == "g") {
            result = result.replace(/\s/g, "");
        }
        return result;
    },//两个时间比较大小
    CompareDate: function (date) {
        var dB = new Date(date);//获取当前选择日期  
        var d = new Date();
        if (Date.parse(d) > Date.parse(dB)) {//时间戳对比  
            return 0;
        }
        return 1;
    },//判断是否包含某个字符串
    Contains: function (reg, val) {
        var rr = new RegExp(reg);
        if (rr.test(val))
            return true;
        else
            return false;
    },//页面启用禁用html代码
    Checkbox: function (isEnabled, idstr, quanxian, title) {
        if (quanxian === "True") {
            if (isEnabled) {
                return '<input type="checkbox" checked="" lay-skin="switch" lay-text="启用|禁用" lay-filter="ChkIsEnabled" aid="' + idstr + '" wtitle="' + title + '" />';
            } else {
                return '<input type="checkbox" lay-skin="switch" lay-text="启用|禁用" lay-filter="ChkIsEnabled" aid="' + idstr + '"  wtitle="' + title + '"  />';
            }
        } else {
            if (isEnabled) {
                return '<span class="layui-badge layui-bg-blue">已启用</span>';
            } else {
                return '<span class="layui-badge layui-bg-red">未启用</span>';
            }
        }
    },// name:修改的信息是什么, 父id可为空 columnName:栏目名称
    CheckboxFun: function (url, idname, statename, tablename, qita, columnName) {
        qita = qita || '';
        layui.use(['table', 'form'], function () {
            var table = layui.table;
            var form = layui.form;
            //启用、禁用
            form.on('switch(ChkIsEnabled)', function (data) {
                var sw = this;
                var vn = sw.checked ? "启用" : "禁用";
                var vis = sw.checked ? true : false;
                var vcid = $(sw).attr("aid");
                var title = $(sw).attr("wtitle");
                var x = sw.checked;
                WKFXCms.Confirm("您确定" + vn + "吗？", function () {
                    WKFXCms.AjaxPost(url, idname + "=" + vcid + "&" + statename + "=" + vis + qita, function (result) {
                        if (result.code === 1) {
                            var logs = {
                                "OperateType": columnName + "启用禁用", "PrimaryId": vcid, "ColumnName": title
                            };
                            var vJson = [{ idname: vcid, "Mean": columnName + "ID" }, { statename: vn, "Mean": "启用禁用状态" }];
                            WKFXCms.LogAction(logs, vJson, false);
                            table.reload(tablename);
                        }
                        else {
                            sw.checked = !x;
                            form.render("checkbox");
                        }
                        WKFXCms.Msg(result.msg);
                    });
                }, function () {
                    sw.checked = !x;
                    form.render("checkbox");
                }, function () {
                    sw.checked = !x;
                    form.render("checkbox");
                });
            });
        });
    },
    HTMLEncode: function (text) {//html编码
        text = text.replace(/&/g, "&");
        text = text.replace(/</g, "'<'");
        text = text.replace(/>/g, "'>'");
        return text;
    },
    IsInArray: function (arr, value) {//判断数组是否包含某个对象
        for (var i = 0; i < arr.length; i++) {
            if (value === arr[i]) {
                return true;
            }
        }
        return false;
    },
    ReplaceTran: function (con) {
        var vreg = new RegExp("<p class=\"TraCode\" .*?>(.+?)</p>", "ig");
        con = con.replace(vreg, "");
        return con;
    },
    FormSelectsOld: function (callback) {
        layui.use(['formSelects'], function () {
            var formSelects = layui.formSelects;
            //调用回调函数
            if (callback) {
                callback(formSelects);
            }
        });
    },//下拉框分页选择
    FormSelects: function (id, code, type, url, callback, success, beforeSuccess) {
        layui.use(['formSelects'], function () {
            var formselect = layui.formSelects;
            formselect.config(id, {
                type: type,                //请求方式: post, get, put, delete...
                header: {},                 //自定义请求头
                data: {},                   //自定义除搜索内容外的其他数据
                searchUrl: url,              //搜索地址, 默认使用xm-select-search的值, 此参数优先级高
                searchName: 'search',      //自定义搜索内容的key值
                searchVal: '',              //自定义搜索内容, 搜素一次后失效, 优先级高于搜索框中的值
                keyName: 'TraderName',            //自定义返回数据中name的key, 默认 name
                keyVal: 'TraderCode',            //自定义返回数据中value的key, 默认 value
                keySel: 'Selected',         //自定义返回数据中selected的key, 默认 selected
                keyDis: 'Disabled',         //自定义返回数据中disabled的key, 默认 disabled
                keyChildren: 'children',    //联动多选自定义children
                delay: 200,                 //搜索延迟时间, 默认停止输入500ms后开始搜索
                direction: 'auto',          //多选下拉方向, auto|up|down
                response: {
                    statusCode: 1,          //成功状态码
                    statusName: 'code',     //code key
                    msgName: 'msg',         //msg key
                    dataName: 'data'        //data key
                },
                success: function (id, url, searchVal, result) {      //使用远程方式的success回调
                    if (success)
                        success(id, url, searchVal, result);
                },
                error: function (id, url, searchVal, err) {           //使用远程方式的error回调
                    console.log(err);   //err对象
                },
                beforeSuccess: function (id, url, searchVal, result) {        //success之前的回调, 干嘛呢? 处理数据的, 如果后台不想修改数据, 你也不想修改源码, 那就用这种方式处理下数据结构吧
                    if (beforeSuccess)
                        beforeSuccess(id, url, searchVal, result);
                    return result;  //必须return一个结果, 这个结果要符合对应的数据结构
                },
                beforeSearch: function (id, url, searchVal) {         //搜索前调用此方法, return true将触发搜索, 否则不触发
                    if (!searchVal) {//如果搜索内容为空,就不触发搜索
                        return false;
                    }
                    return true;
                },
                clearInput: false,          //当有搜索内容时, 点击选项是否清空搜索内容, 默认不清空
            }, false).data(id, 'server', {
                url: url,
                data: { code: code }
            });
            formselect.render("selectType");
            if (callback)
                callback(formselect);
        });
    },//日志记录
    LogAction: function (data, json, isDiff, inputName) {
        try {
            inputName = inputName || "input-name";
            var vdiff = [];
            if (isDiff) {
                $(".layui-form-label").each(function () {
                    var vmean = $(this).attr(inputName);
                    if (WKFXCms.ValidateIsNull(vmean) && json.hasOwnProperty(vmean)) {
                        var ojson = {};
                        //判断值是否是字符串
                        if (typeof json[vmean] == 'string' && json[vmean].constructor == String)
                            ojson[vmean] = String(json[vmean]).replace(/[\n|\r|\"|\[|\]]/g, "").trim();
                        else
                            ojson[vmean] = json[vmean];
                        ojson.Mean = $.trim($(this).text());
                        vdiff.push(ojson);
                    }
                });
                data.Json = JSON.stringify(vdiff);
            }
            else
                data.Json = JSON.stringify(json);
            WKFXCms.AjaxJson("/Global/SetActionLog", "POST", data, function (result) {
                console.log(result);
            });
        }
        catch (ex) { console.log(ex) }
    }, GetQueryVariable: function (variable) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == variable) { return pair[1]; }
        }
        return (false);
    }
    , GetUrlQueryString: function (name, url) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = url.split("?")[1].match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
    , GuangGaoGuiZe: function (obj, isguonei, isapp) {
        var notice = "";
        var ApplicationType = obj.ApplicationType;
        if (obj.JumpType == "3") {
            if (isguonei) {
                //国内
                if (ApplicationType == 1) {
                    //if (!shikan.isnewsAppGuoNei(obj.Url)) {
                    //    notice = "不符合天眼新闻的链接规则!";
                    //}
                } else {
                    if (!shikan.isnews(obj.Url)) {
                        notice = "不符合天眼新闻的链接规则!";
                    }
                }
            }
            else {
                //不在区分APP或者PC的规则

                if (!shikan.isnewsApp(obj.Url) && !shikan.isnewsPC(obj.Url)) {
                    notice = "不符合天眼新闻的链接规则!";
                }
            }
        }
        else if (obj.JumpType == "4") {
            if (!shikan.isshikan(obj.Url)) {
                notice = "不符合天眼实勘的链接规则!";
            }
        }
        else if (obj.JumpType == "5") {
            //天眼志规则：国内版m，国际版APp，国际版PC
            if (ApplicationType == 7 && !shikan.istyzGuoJi(obj.Url)) {
                notice = "不符合wiki-app天眼志的链接规则!";
            } else if (ApplicationType == 1 && !shikan.istyz(obj.Url)) {
                notice = "不符合app天眼志的链接规则!";
            } else if (ApplicationType == 3 && !shikan.istyzpc(obj.Url)) {
                notice = "不符合pc天眼志的链接规则!";
            } else if (ApplicationType == 8 && !shikan.istyzpcwiki(obj.Url)) {
                notice = "不符合Wiki-pc天眼志的链接规则!";
            }
        } else if (obj.JumpType == "7") {
            //汇吧
            //https://apphtml.wikifx.com/MarketDetail/Index?code=202003133152323788&languageCode=zh-cn&countryCode=156&uid=0
            if (ApplicationType == 3 || ApplicationType == 8) {
                notice = "全球集市现在不支持PC!";
            }
            if (!shikan.ishbAppGuoJi(obj.Url)) {
                notice = "不符合全球集市APP的链接规则!";
            }
        }
        return notice;
    }, GuangGaoCode: function (obj, isguonei, isapp) {
        var models = {
            TCode: "",
            Language: "",
            Country: ""
        };
        var TCode = "";
        if (obj.JumpType == "5") {
            if (shikan.istyz(obj.Url) || shikan.istyzpc(obj.Url)) {
                models.TCode = GetCodeFromUrl(obj.Url);
            }
            if (shikan.istyzGuoJi(obj.Url)) {
                models.TCode = WKFXCms.GetUrlQueryString("pid", obj.Url);
                models.Language = WKFXCms.GetUrlQueryString("languageCode", obj.Url);
                models.Country = WKFXCms.GetUrlQueryString("countryCode", obj.Url);
            }
            if (shikan.istyzpcwiki(obj.Url)) {
                models.TCode = GetCodeFromUrl(obj.Url);
                var lcode = obj.Url.split("/")[3];
                var listcode = lcode.split("_");
                models.Country = listcode[0];
                models.Language = listcode[1];
            }
        } else if (obj.JumpType == "3") {
            if (shikan.isnews(obj.Url)) {
                models.TCode = GetCodeFromUrl(obj.Url);
            } else if (shikan.isnewsApp(obj.Url)) {
                models.TCode = WKFXCms.GetUrlQueryString("code", obj.Url);
                models.Language = WKFXCms.GetUrlQueryString("languageCode", obj.Url);
                models.Country = WKFXCms.GetUrlQueryString("countryCode", obj.Url);
            } else if (shikan.isnewsPC(obj.Url)) {
                models.TCode = GetCodeFromUrl(obj.Url);
                var lcode = obj.Url.split("/")[3];
                var listcode = lcode.split("_");
                models.Country = listcode[0];
                models.Language = listcode[1];
            } else if (shikan.isnewsAppGuoNei(obj.Url)) {
                models.TCode = GetCodeFromUrl(obj.Url);
            }
        } else if (obj.JumpType == "4") {
            //实勘处理
            if (!isguonei) {
                models.TCode = GetCodeFromUrl(obj.Url);
                // 国际    https://survey.wikifx.com/hk_zh-hk/5323866aa0.html
                //分享 国际 https://tshare.wikifx.com/hk_zh-hk/shikan/5323866aa0.html
                var lcode = obj.Url.split("/")[3];
                var listcode = lcode.split("_");
                models.Country = listcode[0];
                models.Language = listcode[1];
            } else {
                //国内的
                models.TCode = GetCodeFromUrl(obj.Url);
            }
        } else if (obj.JumpType == "7") {
            //汇吧
            if (!isguonei) {
                //https://apphtml.wikifx.com/MarketDetail/Index?code=202003133152323788&languageCode=zh-cn&countryCode=156&uid=0
                if (shikan.ishbAppGuoJi(obj.Url)) {
                    models.TCode = WKFXCms.GetUrlQueryString("code", obj.Url);
                    models.Language = WKFXCms.GetUrlQueryString("languageCode", obj.Url);
                    models.Country = WKFXCms.GetUrlQueryString("countryCode", obj.Url);
                }
            }
        }
        else {
            models.TCode = GetCodeFromUrl(obj.Url);
        }
        return models;
    }
};

//时间戳的处理
var toDateString = function (d, format) {
    var date = new Date(parseInt(d) || new Date())
        , ymd = [
            this.digit(date.getFullYear(), 4)
            , this.digit(date.getMonth() + 1)
            , this.digit(date.getDate())
        ]
        , hms = [
            this.digit(date.getHours())
            , this.digit(date.getMinutes())
            , this.digit(date.getSeconds())
        ];
    format = format || 'yyyy-MM-dd HH:mm:ss';

    return format.replace(/yyyy/g, ymd[0])
        .replace(/MM/g, ymd[1])
        .replace(/dd/g, ymd[2])
        .replace(/HH/g, hms[0])
        .replace(/mm/g, hms[1])
        .replace(/ss/g, hms[2]);
};
//数字前置补零
var digit = function (num, length, end) {
    var str = '';
    num = String(num);
    length = length || 2;
    for (var i = num.length; i < length; i++) {
        str += '0';
    }
    return num < Math.pow(10, length) ? str + (num | 0) : num;
};

//保留两位小数，不带符号
var twoDecimal = function (value) {
    var f = parseFloat(value);
    if (isNaN(f)) {
        return false;
    }
    var f = Math.round(value * 100) / 100;
    var s = f.toString();
    var rs = s.indexOf('.');
    if (rs < 0) {
        rs = s.length;
        s += '.';
    }
    while (s.length <= rs + 2) {
        s += '0';
    }
    return s;
};

//解决javascript小数相减会出现一长串的小数位数
String.prototype.toFloat = Number.prototype.toFloat = function (decimalPlace, isRoundUp) {
    decimalPlace = decimalPlace || 0,
        isRoundUp = Object.prototype.toString.call(isRoundUp).match(/^\[object\s(.*)\]$/)[1].toLowerCase() == 'boolean' ? isRoundUp : !0;
    try {
        var res = isRoundUp
            ? (this * 1).toFixed(decimalPlace)
            : this.toString().replace(new RegExp("([0-9]+\.[0-9]{" + decimalPlace + "})[0-9]*", "g"), "$1");
        return isNaN(res * 1) ? this : res;
    } catch (e) {
        return isNaN(this * 1) ? this : this * 1;//防止小数位数字越界
    }
}

//时间
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter  
        "S": this.getMilliseconds() //millisecond  
    }
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

//时间转时间戳
var dateToString = function (date) {
    var ts = Date.parse(new Date(date));
    ts = ts / 1000;
    return ts;
}

//移除数组值
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};

//移除数组对象
Array.prototype.removeobj = function (id, val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i][id] == val) {
            this.splice(i, 1);
            break;
        }
    }
};

/***********************通用值类型转换************************/
var FxSky = {
    RoleType: function (obj) { //角色类型转换
        switch (parseInt(obj)) {
            case 1:
                return '交易商';
            case 2:
                return '监管机构';
            case 3:
                return '代理商';
            case 6:
                return '从业者';
            case 9:
                return '交易者';
            default:
                return '未知';
        }
    },//通用图片显示
    ImgHtml: function (path, name, none, base64, urlsrc) {
        urlsrc = (urlsrc == null || urlsrc == undefined) ? path : urlsrc;
        var html = '<div class="img_div"><div class="p_delete ' + none + '" onclick="FxSky.DeleteImg(this);"></div><a href="' + urlsrc + '" target="_blank" title="点击查看"><img src="' + base64 + '" filepath="' + path + '" filename="' + name + '" class="img-wh"/></a></div>';
        return html;
    },//通用图片显示
    ImgHtmlShiKan: function (path, name, none, base64, urlsrc, delClass) {
        urlsrc = (urlsrc == null || urlsrc == undefined) ? path : urlsrc;
        var html = '<div class="img_div"><div delc="' + delClass + '" class="p_delete ' + none + '" onclick="FxSky.DeleteImgShiKan(this);"></div><a href="' + urlsrc + '" target="_blank" title="点击查看"><img src="' + base64 + '" filepath="' + path + '" filename="' + name + '" class="img-wh"/></a></div>';
        return html;
    },
    DeleteImgShiKan: function (id) {
        WKFXCms.Confirm('您确定删除该图片吗？', function () {
            $(id).parents(".img_div").remove();
            console.log($(id).attr("delc"));
            $("#" + $(id).attr("delc")).val("");
            WKFXCms.CloseAll();
        });
    },
    //上传时生成附件
    UploadAttachHtml: function (href, path, name, none) {
        var html = '<div class="attach_div"><div class="p_delete ' + none + '" onclick="FxSky.DeleteAttach(this);"></div><a href="' + href + '" target="_blank" title="点击查看" filepath="' + path + '" filename="' + name + '">' + name + '</a></div>';
        return html;
    },
    UploadAttachHtmlNew: function (href, path, name, none) {
        var html = '<div class="attach_div"><div class="p_delete ' + none + '" onclick="FxSky.DeleteAttach(this);"></div><a href="' + href + '" target="_blank" title="点击查看" filepath="' + path + '" filename="' + name + '">' + name + '</a></div>';
        return html;
    },
    IsState: function (obj) {//启用、禁用状态
        if (parseInt(obj) == 0) {
            return '<div><span class="layui-badge layui-bg-red">禁用</span></div>';
        }
        else
            return '<div><span class="layui-badge layui-bg-blue">启用</span></div>';
    },//获取附件数据集合，多图
    ImgsData: function (id) {
        var arrAtt = [];
        $("." + id).find('img').each(function () {
            var arry = [".png", ".jpg", ".jpeg", ".bmp", ".gif", ".srv"];
            var vm = {};
            vm.Path = $(this).attr("filepath");
            vm.AttachmentName = $(this).attr("filename");
            var index1 = vm.Path.lastIndexOf('.');
            var postf = vm.Path.substring(index1, vm.Path.length);
            for (var i = 0; i < arry.length; i++) {
                if (WKFXCms.Contains(arry[i].toLowerCase(), postf.toLowerCase())) {
                    vm.AttachmentType = arry[i].substring(1);
                    break;
                }
            }
            if (vm.Path != '')
                arrAtt.push(vm);
        });
        return arrAtt;
    },//获取附件数据集合，不是图
    AttachsData: function (id) {
        var arrAtt = [];
        $("." + id).find('a').each(function () {
            var vm = {};
            vm.Path = $(this).attr("filepath");
            vm.AttachmentName = $(this).attr("filename");
            var index1 = vm.Path.lastIndexOf('.');
            var postf = vm.Path.substring(index1 + 1, vm.Path.length);
            vm.AttachmentType = postf;
            if (vm.Path != '')
                arrAtt.push(vm);
        });
        return arrAtt;
    },
    QEData: function (id) {//获取QQ，邮箱集合数据
        var data = '';
        $("#" + id).find('.col-input-width-500').each(function () {
            var val = $(this).find("input[type='Text']").val();
            if (data == '')
                data = val;
            else
                data += ',' + val;
        });
        return data;
    },//加载显示logo、ico、hico图片,只有单张图片
    LoadImg: function (data, id, hid) {
        if (data != null && data.length == 1) {
            $("." + id).append(FxSky.ImgHtml(data[0].Path, '', 'col-display-none', data[0].Path)).addClass("col-top");
            $("#" + hid).val(data[0].Path).attr("filename", data[0].AttachmentName);
        }
    },//加载显示附件，多图
    LoadImgs: function (data, id) {
        if (data != null && data.length != 0) {
            for (var i = 0; i < data.length; i++) {
                $("." + id).append(FxSky.ImgHtml(data[i].Path, data[i].AttachmentName, '', data[i].Path)).addClass("col-top");
            }
        }
    },//加载显示附件，不是图片
    LoadAttachs: function (data, id) {
        if (data != null && data.length != 0) {
            for (var i = 0; i < data.length; i++) {
                $("." + id).append(FxSky.UploadAttachHtml(data[i].Path, data[i].Path, data[i].AttachmentName)).addClass("col-top");
            }
        }
    },//获取图片数据,单图
    ImgData: function (id) {
        var arrImg = [];
        var _vobj = $("#" + id);
        var vm = {};
        vm.AttachmentType = _vobj.attr("t");
        vm.Path = _vobj.val();
        vm.AttachmentName = _vobj.attr("filename");
        if (vm.Path != '')
            arrImg.push(vm);
        return arrImg;
    },
    DeleteImg: function (id) {//模拟删除图片
        WKFXCms.Confirm('您确定删除该图片吗？', function () {
            $(id).parents(".img_div").remove();
            WKFXCms.CloseAll();
        });
    },
    DeleteAttach: function (id) {//模拟删除附件
        WKFXCms.Confirm('您确定删除该附件吗？', function () {
            $(id).parents(".attach_div").remove();
            WKFXCms.CloseAll();
        });
    }
};

/********************通用正则*****************/
var voneReg = /^[0-9]+(.[0-9]{1,1})?$/;  //保留一位小数
var vtwoReg = /^[0-9]+(.[0-9]{1,2})?$/;  //保留两位小数
var vthreeReg = /^[0-9]+(.[0-9]{1,3})?$/;  //保留三位小数
var vfourReg = /^[0-9]+(.[0-9]{1,4})?$/;  //保留四位小数
var vfiveReg = /^[0-9]+(.[0-9]{1,5})?$/;  //保留五位小数
var vsixReg = /^[0-9]+(.[0-9]{1,6})?$/;  //保留六位小数
var vsevenReg = /^[0-9]+(.[0-9]{1,7})?$/;  //保留七位小数
var vzsReg = /^[0-9]+$/; //正整数
var cphoneReg = /^1(3|4|5|7|8)\d{9}$/;//手机号正则
//var vurlReg = /^((ht|f)tps?):\/\/[\w\-]+(\.[\w\-]+)+([\w\-\.,@?^=%&:\/~\+#]*[\w\-\@?^=%&\/~\+#])?$/; //url正则
//url正则
var vurlReg = /^(?:([A-Za-z]+):)?(\/{0,3})([0-9.\-A-Za-z]+)(?::(\d+))?(?:\/([^?#]*))?(?:\?([^#]*))?(?:#(.*))?$/;
var phoneArea = /^\d{3,4}$/; //区号
var vtelReg = /^\d{7,8}$/;//电话
var qqReg = /^[1-9]\d{4,11}$/;//qq
var emailReg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;//邮箱
var vmp4Reg = /^((ht|f)tps?):.*.mp4$/;//MP4
var uzw = /[\u4e00-\u9fa5]/;//中文
var uyw = /[a-zA-Z]/;//英文
var urlnewReg = /[a-zA-z]+:\/\/[^\\s]*/;

/********************排序*****************/
var paixun = {
    inintTable: function () {
        var fixHelperModified = function (e, tr) {
            var $originals = tr.children();
            var $helper = tr.clone();
            $helper.children().each(function (index) {
                $(this).width($originals.eq(index).width());
            });
            return $helper;
        },
            updateIndex = function (e, ui) {
                $('td.index', ui.item.parent()).each(function (i) {
                    $(this).html(i + 1);
                });
            };

        $(".layui-table-main table tbody").sortable({
            helper: fixHelperModified,
            stop: updateIndex
        }).disableSelection();
    }
};
/********************实勘*****************/
var shikan = {
    ishbAppGuoJi: function (value) {
        // https://apphtml.wikifx.com/MarketDetail/Index?code=202003133152323788&languageCode=zh-cn&countryCode=156&uid=0
        var lowervalue = value.toLowerCase();
        if (lowervalue.indexOf("apphtml.wikifx.com/marketdetail/index?") > -1 && lowervalue.indexOf("code=") > -1 && lowervalue.indexOf("languagecode=") > -1 && lowervalue.indexOf("countrycode=") > -1) {
            return true;
        } else {
            return false;
        }
    },
    isshikan: function (value) {
        var shikanRegex = /^http[s]*:\/\/((survey.fxeye.com)|(survey.wikifx.com))\/[a-zA-Z-_]+\/[a-zA-Z0-9]+.html$/;
        if (value) {
            if (shikanRegex.test(value)) {
                return true;
            } else if (shikan.isshikanM(value)) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    },
    isshikanM: function (value) {
        var shikanM = /^http[s]*:\/\/((tshare.wikifx.com)|(m.wikifx.com)|(m.fxeye.com))\/[a-zA-Z-_]+\/shikan\/[a-zA-Z0-9]+.html$/;
        if (value) {
            return shikanM.test(value);
        } else {
            return false;
        }
    },
    isnews: function (value) {
        var newsurl = /^http[s]?:\/\/www.fxeye.com\/[0-9a-z]{10,}\.html$/;
        return value.indexOf("www.fxeye.com") > -1 && newsurl.test(value);
    },
    isnewsAppGuoNei: function (value) {
        var newsurl = /^http[s]?:\/\/m.fxeye.com\/[0-9a-z]{10,}\.html$/;
        return value.indexOf("m.fxeye.com") > -1 && newsurl.test(value);
    },
    isnewsApp: function (value) {
        // https://apphtml.wikifx.com/news/newsdetail?countryCode=cn&languageCode=en&code=202001041274686570
        var lowervalue = value.toLowerCase();
        if (lowervalue.indexOf("apphtml.wikifx.com/news/newsdetail?") > -1 && lowervalue.indexOf("code=") > -1) {
            return true;
        } else {
            return false;
        }
    },
    isnewsPC: function (value) {
        //https://www.wikifx.com/cn_en/newsdetail/202001041274686570.html
        var newsurl = /^http[s]?:\/\/www.wikifx.com\/[a-zA-Z_-]{2,}\/newsdetail\/[0-9a-z]{10,}\.html$/;
        return value.indexOf("www.wikifx.com") > -1 && newsurl.test(value);
    },
    isjys: function (value) {
        return value.indexOf("www.fxeye.com") > -1 && value.indexOf("fxeye.com/dealer") > -1;
    },
    istyz: function (value) {
        if (value.indexOf("m.fxeye.com") > -1 && value.indexOf("m.fxeye.com/zhi") > -1) {
            return true;
        } else if (value.indexOf("m.wikifx.com") > -1 && value.indexOf("m.wikifx.com/zhi") > -1) {
            return true;
        } else {
            return false;
        }
    },
    istyzGuoJi: function (value) {
        //http://apphtml.wikifx.com/BookInt/BookShare?pid=S_JO00004111912115TYC&languageCode=zh-cn&countryCode=cn
        var lowervalue = value.toLowerCase();
        if (lowervalue.indexOf("apphtml.wikifx.com/bookint/bookshare") > -1 && lowervalue.indexOf("pid=") > -1) {
            return true;
        } else {
            return false;
        }
    },
    istyzpc: function (value) {
        if (value.indexOf("book.fxeye.com") > -1) {
            var tyzpc = /^http[s]*:\/\/book.fxeye.com\/[a-zA-Z]+_[a-zA-Z]+[-]*[a-zA-Z]+\/[0-9a-zA-Z_]+.html$/;
            return tyzpc.test(value);
        } else {
            return false;
        }
    }, istyzpcwiki: function (value) {
        //https://book.wikifx.com/cn_zh-cn/S_JO0000111200103BMOF.html 
        if (value.indexOf("book.wikifx.com") > -1) {
            var tyzpc = /^http[s]*:\/\/book.wikifx.com\/[a-zA-Z]+_[a-zA-Z]+[-]*[a-zA-Z]+\/[0-9a-zA-Z_]+.html$/;
            return tyzpc.test(value);
        } else {
            return false;
        }
    },
    GetUrl: function GetUrl(jumpType, tCode) {
        var url = "";
        if (jumpType === 3)
            url = "http://www.fxeye.com/" + tCode + ".html";
        if (jumpType === 4)
            url = "http://survey.fxeye.com/cn_zh-cn/" + tCode + ".html";
        return url;
    }
};

/********************常用表单*****************/
var formhelper = {
    GetRadioValue: function (name) {
        var item = $("input:radio[name=" + name + "]:checked");
        return item ? item.val() : "";
    }
    , Gettextarea: function (str) {
        var reg = new RegExp("<br>", "g"); //创建正则RegExp对象
        var newstr = str.replace(reg, "\n");
        return newstr;
    }
    , Trim: function (str) {
        if (str) {
            return str.replace(/(^\s*)|(\s*$)/g, "");
        } else {
            return "";
        }
    }
};