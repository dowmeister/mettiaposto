/*
* GENERAL PURPOSE FUNCTIONS
*/

$(document).ready(function ()
{
    if ($('#feedback').tabSlideOut)
        $('#feedback').tabSlideOut();

    if ($.superfish)
        $('.sf-menu').superfish();

    $('#search').mouseover(function ()
    {
        $(this).css('opacity', 1.0);
    });

    $('#search').mouseout(function ()
    {
        $(this).css('opacity', 0.6);
    });

    $("#searchCity").click(function () { $(this).val(''); });
    $("#searchCity").autocomplete({
        source: places, 
        focus: function (event, ui)
        {
            $("#searchCity").val(ui.item.label);
            return false;
        },
        select: function (event, ui) { window.location.href = ui.item.link + 'index.aspx'; }, 
        minLenght: 0, delay: 0
    });
});

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
    clearMessages();
    $(container + ' > .ajax').remove();
}

function writeAjax(container)
{
    hideAjax(container);
    var c = $('<div class="message ajax"></div>');
    c.append('<img alt="Caricamento in corso.." src="/images/ajax-loader.gif" />');
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

function sendFeedback(footerControl)
{
    $('#feedbackSubmit').hide();
    writeAjax('#feedbackMessage');

    var o = new Object();
    o.name = $('#' + footerControl + '_txtFeedbackName').val();
    o.email = $('#' + footerControl + '_txtFeedbackEmail').val();
    o.message = $('#' + footerControl + '_txtFeedbackComment').val();
    o = addSessionKey(o);
    var proxy = new JSONService();
    proxy.sendFeedback(o, sendFeedback_callback);
}

function sendFeedback_callback(r)
{
    if (checkAjaxError(r, '#feedbackMessage'))
    {
        hideAjax('#feedbackMessage');
        writeMessage('Grazie!', 'per aver inviato il tuo commento', '#feedbackMessage');
    }
}

function checkAjaxError(r, container)
{
    if (r.error)
    {
        writeError(r.error.message, container);
        return false;
    }
    return true;
}

function showNotExistingCityDialog(cityToAdd)
{
    $('#notExistingCityLabel').html(cityToAdd);
    $('#notExistingCity').dialog({
        width: 500, modal: true, resizable: false, draggable: false, title: 'Ops!', show: 'slide',
        buttons: {
            'Aggiungi la tua città': function () { goTo('/' + cityToAdd + '/crea.aspx'); },
            'Leggi le FAQ': function () { goTo('/pages/info.aspx#addCity'); },
            "Dai un'occhiata": function () { $(this).dialog('close'); }
        }
    });
}

function searchCity() {
    window.location.href = '/' + $('#searchCity').val() + '/index.aspx';
}