var FancyForm = function () {
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
    FancyForm.setup();
});

var G_tocard_maxTips = 30;

$(function () {
    (
        function () {
            var a = $(".plus-tag");
            $("a em", a).live("click", function () {
                var c = $(this).parents("a"), b = c.attr("title"), d = c.attr("value"), id = c.attr("tid");
                WKFXCms.Confirm('您确定删除该数据吗？', function () {
                    delTips(b, d);
                    //取消列表中关键词的选中状态
                    $(".ulword li").each(function () {
                        var vid = $(this).find("input[type='checkbox']").val();
                        if (vid == id) {
                            $("#" + vid).removeAttr("checked");
                            vkeyIds.remove(vid);
                            return false;
                        }
                    });
                    WKFXCms.RenderForm();
                    WKFXCms.CloseAll();
                })
            });

            hasTips = function (b) {
                var d = $("a", a), c = false;
                d.each(function () {
                    if ($(this).attr("title") == b) {
                        c = true;
                        return false
                    }
                });
                return c
            };

            isMaxTips = function () {
                return
                $("a", a).length >= G_tocard_maxTips
            };

            setTips = function (c, d, id) {
                if (hasTips(c)) {
                    return false
                } if (isMaxTips()) {
                    alert("最多添加" + G_tocard_maxTips + "个标签！");
                    return false
                }
                var b = d ? 'value="' + d + '"' : "";
                a.append($("<a " + b + ' title="' + c + '" href="javascript:void(0);" tid="' + id + '"><span>' + c + "</span><em></em></a>"));
                return true
            };
            //删除标签
            delTips = function (b, c) {
                if (!hasTips(b)) {
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

            getTips = function () {
                var b = [];
                $("a", a).each(function () {
                    b.push($(this).attr("title"))
                });
                return b
            };

            getTipsId = function () {
                var b = [];
                $("a", a).each(function () {
                    b.push($(this).attr("value"))
                });
                return b
            };

            getTipsIdAndTag = function () {
                var b = [];
                $("a", a).each(function () {
                    b.push($(this).attr("value") + "##" + $(this).attr("title"))
                });
                return b
            }
        }

    )()
});