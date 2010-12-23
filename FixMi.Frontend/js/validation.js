$.validateUtils = function (options)
{

    var rules = new Array();
    var errors = new Array();

    this.validationResult = function ()
    {
        return errors.length == 0;
    }

    this.addRule = function (rule)
    {

        rules.push(rule);
    };

    this.addRules = function (rs)
    {
        $.merge(rules, rs);
    }

    this.validate = function ()
    {
        $(options.errorDiv).hide();

        for (var i = 0; i < rules.length; i++)
        {

            var res = false;
            var currentRule = rules[i];

            switch (currentRule.validateFunction)
            {
                case 'notEmpty':
                    res = this.notEmpty($(currentRule.field));
                    break;
                case 'checkSelected':
                    res = this.checkSelected($(currentRule.field), currentRule.nullValue);
                    break;
                case 'validEmail':
                    res = this.validEmail($(currentRule.field));
                    break;
            }
            if (!res)
                errors.push(currentRule);
            else
                $(currentRule.field).attr('style', '');
        }
    };

    this.showErrorMessage = function ()
    {
        if (options.showAs == 'div')
        {
            $(options.errorDiv).show();

            $(options.errorDiv).empty();
            $(options.errorDiv).html(options.headerMessage);

            var errorContainer = $('<ul></ul>');

            for (var i = 0; i < errors.length; i++)
            {
                errorContainer.append($('<li></li>').html(errors[i].message));

                if (options.errorStyle)
                {
                    $(errors[i].field).attr('style', options.errorStyle);
                }
            }

            $(options.errorDiv).append(errorContainer);
        }

        if (options.errorClass)
        {
            for (var i = 0; i < rules.length; i++)
            {
                $(rules[i].field).addClass(options.errorClass);
            }
        }
    };

    /* rules */
    this.notEmpty = function (field) { return !($(field).val() == ''); };
    this.checkSelected = function (field, nullValue) { return !($(field).val() == nullValue); };
    this.validRegex = function (field, regex)
    {
        if (this.notEmpty(field))
        {
            var r = new RegExp(regex);
            return r.test($(field).val());
        }
        return true;
    };

    this.validEmail = function (field) { return this.validRegex(field, "^([\\w-\\.\\&\\#\\!\\$\\%\\'\\*\\+\\-\\/\\=\\?\\^\\_\\`\\{\\|\\}\\~]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$"); };

    return this;
}