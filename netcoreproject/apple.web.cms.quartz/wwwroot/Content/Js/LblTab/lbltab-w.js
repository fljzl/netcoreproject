var wFancyForm = function () {
    return {
        inputs: ".FancyForm input, .FancyForm textarea",
        setup: function () {
            var a = this;
            this.inputs = $(this.inputs);
            a.inputs.each(function () {
                var c = $(this);
                a.checkVal(c)
            });
            a.inputs.live("keyup blur", function () {
                var c = $(this);
                a.checkVal(c);
            });
        }, checkVal: function (a) {
            a.val().length > 0 ? a.parent("li").addClass("val") : a.parent("li").removeClass("val")
        }
    }
}();

$(document).ready(function () {
    wFancyForm.setup();
});

var wG_tocard_maxTips = 30;

$(function () {
    (
        function () {
            var a = $("#xgTrader");
            $("a em", a).live("click", function () {
                var c = $(this).parents("a"), b = c.attr("title"), d = c.attr("value"), id = c.attr("tid");
                ArtCms.Confirm('您确定删除该交易商吗？', function () {
                    wdelTips(b, d);
                    vxgkeyIds = vxgkeyIds.replace(id + ',', "");
                    $(".xg_chk").each(function () {
                        var vid = $(this).find("input[type='checkbox']").attr("code");
                        if (vid == id) {
                            $(this).find("input[type='checkbox']").removeAttr("checked");
                            return false;
                        }
                    });
                    ArtCms.RenderForm();
                    ArtCms.CloseAll();
                })
            });

            whasTips = function (b) {
                var d = $("a", a), c = false;
                d.each(function () {
                    if ($(this).attr("title") == b) {
                        c = true;
                        return false
                    }
                });
                return c
            };

            wisMaxTips = function () {
                return
                $("a", a).length >= wG_tocard_maxTips
            };

            wsetTips = function (c, d, id) {
                if (whasTips(c)) {
                    return false
                } if (wisMaxTips()) {
                    alert("最多添加" + wG_tocard_maxTips + "个标签！");
                    return false
                }
                var b = d ? 'value="' + d + '"' : "";
                a.append($("<a " + b + ' title="' + c + '" href="javascript:void(0);" tid="' + id + '"><span>' + c + "</span><em></em></a>"));
                return true
            };
            //删除标签
            wdelTips = function (b, c) {
                if (!whasTips(b)) {
                    return false
                }
                $("a", a).each(function () {
                    var d = $(this);
                    if (d.attr("title") == b) {
                        d.remove();
                        return false
                    }
                });
                return true
            };

            //wgetTips = function () {
            //    var b = [];
            //    $("a", a).each(function () {
            //        b.push($(this).attr("title"))
            //    });
            //    return b
            //};

            //wgetTipsId = function () {
            //    var b = [];
            //    $("a", a).each(function () {
            //        b.push($(this).attr("value"))
            //    });
            //    return b
            //};

            //getTipsIdAndTag = function () {
            //    var b = [];
            //    $("a", a).each(function () {
            //        b.push($(this).attr("value") + "##" + $(this).attr("title"))
            //    });
            //    return b
            //}
        }

    )()
});