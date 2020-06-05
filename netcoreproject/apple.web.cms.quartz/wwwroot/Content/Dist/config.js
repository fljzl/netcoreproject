layui.define(['laytpl', 'layer', 'element', 'util'], function (exports) {
    exports('setter', {
        container: 'LAY_app' //容器ID
        , base: layui.cache.base //记录layuiAdmin文件夹所在路径
        , views:  '' //视图所在目录
        , entry: 'Main' //默认视图文件名
        , engine: '' //视图文件后缀名
        , pageTabs: false //是否开启页面选项卡功能。单页版不推荐开启

        , name: 'layuiAdmin Pro'
        , tableName: 'layuiAdmin' //本地存储表名
        , MOD_NAME: 'admin' //模块事件名

        , debug: false //是否开启调试模式。如开启，接口异常时会抛出异常 URL 等信息

        , interceptor: false //是否开启未登入拦截

        //自定义请求字段
        , request: {
            tokenName: 'access_token' //自动携带 token 的字段名。可设置 false 不携带。
        }

        //自定义响应字段
        , response: {
            statusName: 'code' //数据状态的字段名称
            , statusCode: {
                ok: 0 //数据状态一切正常的状态码
                , logout: 1001 //登录状态失效的状态码
            }
            , msgName: 'msg' //状态信息的字段名称
            , dataName: 'data' //数据详情的字段名称
        }
    });
});
