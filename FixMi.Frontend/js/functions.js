function writeMessage(title, message, container)
{
    hideMessage(container);

    var c = $('<div class="message success"></div>');

    if (title != '')
        c.append($('<h2></h2>').html(title));

    if (message != '')
        c.append($('<p></p>').html(message));

    c.appendTo(container);
    c.show();
}

function hideMessage(container)
{
    $(container + ' > .success').remove();
}

function writeError(error, container)
{
    hideError(container);
    $('<div class="message error"></div>').html('<h2>Ops! Errore!</h2>').html(error).appendTo(container).show();
}

function clearMessages(container)
{
    $(container + ' > .message').remove();
}

function hideError(container)
{
    $(container + ' > .error').remove();
}

function hideAjax(container)
{
    $(container + ' > .ajax').remove();
}

function writeAjax(container)
{
    hideAjax(container);
    var c = $('<div class="message ajax"></div>');
    c.append('<img src="/images/ajax-loader.gif" />');
    c.appendTo(container);
    c.show();
}

function goTo(ref)
{
    window.location.href = ref;
}

function getCode(e)
{
    return (e.keyCode ? e.keyCode : e.which);
}

function addSessionKey(params)
{
    params["ajaxSessionKey"] = ajaxSessionKey;
    return params;
}

