jQuery.fn.extend({
  numInput: function(opt){
      var defaultOptions = {
          width: 100,
          wrapperClass: 'num-input-wrap',
          inputClass: 'num-input',
          addClass: 'add',
          subtractClass: 'subtract',
          positive: true,
          negative: true,
          floatCount: 2,
          val: '',
          placeholder: '',
          min: '',
          max:''
      }
    var options = jQuery.extend(defaultOptions, opt);
    this._initNumDom(options);
  },
  _initNumDom: function(opt){
    for (var i = 0,len = this.length; i < len; i++) {
      var wrapper = $('<div class="'+opt.wrapperClass+'"></div>');
      wrapper.css({"position": "relative", "display": "inline-block", "vertical-align": "top", "height": 36, "width": opt.width, "box-sizing": "border-box", "overflow": "hidden"})
      console.log
        $(this[i]).append(wrapper);
        var inputN = $('<input type="text" class="layui-input ' + opt.inputClass + '" autocomplete="off" maxlength="6" name="' + opt.inputClass + '" value="' + opt.val + '" placeholder="' + opt.placeholder + '"/>');
        inputN.css({ "height": 36, "width": "100%", "padding": "0 25px 0 12px", "font-size": "14px", "line-height": "36px", "background": "#fff", "border": "solid 1px #e6e6e6"})
      var addBtn = $('<span class="'+opt.addClass+'"></span>')
      addBtn.css({"position": "absolute", 'right': 0, 'top': 0, 'width': 25, "height": 19, "border-left": "1px solid #ccc", "box-sizing": "border-box", "cursor": "pointer"})
      var subtractBtn = $('<span class="'+opt.subtractClass+'"></span>')
      subtractBtn.css({"position": "absolute", 'right': 0, 'bottom': 0, 'width': 25, "height": 19, "border-left": "1px solid #ccc", "box-sizing": "border-box", "cursor": "pointer"})
      wrapper.append(inputN).append(addBtn).append(subtractBtn);
      this._initNumEvent(inputN, addBtn, subtractBtn, opt)  
    } 
    $('<style type="text/css">.add:hover,.subtract:hover{background: #d8d8d8;}.add.deep,.subtract.deep{background: #b3b3b3;}.add::after{position: absolute;left: 8px;top: 5px;content: "";border-left: 4px solid transparent;border-right: 4px solid transparent;border-bottom: 6px solid #333;}.subtract::after{position: absolute;left: 8px;bottom: 5px;content: "";border-left: 4px solid transparent;border-right: 4px solid transparent;border-top: 6px solid #333;}</style>').appendTo('head');
  },
    _initNumEvent: function (it, ab, sb, opt) {
        var countDown, quickChange;
        ab.on('mousedown', function () {
            $(this).addClass('deep');
            var val = parseFloat($(this).prevAll('input').val())
            val = isNaN(val) ? 0 : val;
            val++;
            if (opt.max!=''&&val > opt.max && opt.positive) {
                val = opt.max;
                WKFXCms.Tips('最大允许输入值为' + opt.max, $(this), 1);
                return false;
            }
            val = Math.round(val * Math.pow(10, opt.floatCount)) / Math.pow(10, opt.floatCount);
            it.val(val);
            countDown = setTimeout(function () {
                quickChange = setInterval(function () {
                    var val = parseFloat(it.val())
                    val++;
                    if (opt.max != '' && val > opt.max && opt.positive) {
                        clearTimeout(countDown);
                        clearInterval(quickChange);
                        val = opt.max;
                    }
                    val = Math.round(val * Math.pow(10, opt.floatCount)) / Math.pow(10, opt.floatCount);
                    it.val(val);
                }, 30)
            }, 500)
        })
        ab.on('mouseup', function () {
            $(this).removeClass('deep');
            if (countDown) {
                clearTimeout(countDown);
            }
            if (quickChange) {
                clearInterval(quickChange);
            }
        })
        sb.on('mousedown', function () {
            $(this).addClass('deep');
            var val = parseFloat($(this).prevAll('input').val())
            val = isNaN(val) ? 0 : val;
            val--;
            if (opt.min!=''&&val < opt.min && !opt.negative) {
                val = opt.min;
                WKFXCms.Tips('最小允许输入值为' + opt.min, $(this), 1);
                return false;
            } else if (opt.max != '' &&val > opt.max && !opt.positive) {
                val = opt.max;
            }
            val = Math.round(val * Math.pow(10, opt.floatCount)) / Math.pow(10, opt.floatCount);
            it.val(val);
            countDown = setTimeout(function () {//长按半秒触发快速加减
                quickChange = setInterval(function () {
                    var val = parseFloat(it.val());
                    val--;
                    if (opt.min != '' && val < opt.min && !opt.negative) {
                        clearTimeout(countDown);
                        clearInterval(quickChange);
                        val = opt.min;
                    }
                    val = Math.round(val * Math.pow(10, opt.floatCount)) / Math.pow(10, opt.floatCount);
                    it.val(val);
                }, 30)
            }, 500)
        })
        sb.on('mouseup', function () {
            $(this).removeClass('deep');
            if (countDown) {
                clearTimeout(countDown);
            }
            if (quickChange) {
                clearInterval(quickChange);
            }
        })
        it.on('keyup', function () {
            var val = $(this).val();
            if ((opt.positive && opt.negative) || (!opt.positive && !opt.negative)) {
                var reg = new RegExp('^\D*(-?(([1-9]\\d*)?|([0]))?(\\.\\d{0,' + opt.floatCount + '})?)?.*$', 'g');
            } else if (!opt.positive && opt.negative) {
                var reg = new RegExp('^(-(([1-9]\\d*)?|([0]))?(\\.\\d{0,' + opt.floatCount + '})?)?.*$', 'g');
            } else if (opt.positive && !opt.negative) {
                var reg = new RegExp('^\D*(-?(([1-5]\\d{0})?|([0]))?(\\.\\d{0,' + opt.floatCount + '})?)?.*$', 'g');
            }
            val = val.replace(reg, '$1');
            if (val.substring(0,2) ==".0" || val.indexOf('0.00') != -1 || ((val.indexOf('.') == -1 || (val.indexOf('.') + 1) < val.length) && val.indexOf('-') == -1 && val.indexOf('0.0') == -1 && val.indexOf('.0') == -1)) {
                val = parseFloat(val);
                val = isNaN(val) ? '' : val;
            }
            $(this).val(val);
        })
    }
})