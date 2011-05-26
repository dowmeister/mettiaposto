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

    $("#searchCity").click(function () { $(this).val(''); });
    $("#searchCity").keypress(function (event)
    {
        if (event.which == 13 || event.which == 9)
        {
            checkPlace();
            return false;
        }
    });

    if ($('#feedback').tabSlideOut)
    {
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
    }
});

function checkPlace() {

    if ($("#searchCity").val() == '') {
        alert('Inserire il nome di un Comune Italiano per proseguire');
        return;
    }

    var proxy = new JSONService();
    var place = proxy.checkPlace($("#searchCity").val(), ajaxSessionKey);

    if (place)
    {
        $("#searchCity").val(place.name);

        if (place.status == 1)
            window.location.href = '/' + place.name.toLowerCase() + '/index.aspx';
        else
        {
            $('#alreadyRequestedCityLabel').html(placeName);
            $('#alreadyRequestedCity').dialog({
                title: 'Città richiesta ma non attiva', draggable: false, resizable: false,
                buttons: {
                    'Chiudi': function () { $(this).dialog('destroy'); }
                }
            });
        }
    }
    else
        alert("La città richiesta non è in Italia o non esiste");
}

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
            'Aggiungila': function () { $(this).dialog('close'); },
            'Leggi le FAQ': function () { goTo('/pages/info.aspx#addCity'); },
            "Esci": function () { $(this).dialog('close'); }
        }
    });
}

function searchCity()
{
    window.location.href = '/' + $('#searchCity').val() + '/index.aspx';
}

function sharePopup(a)
{
    window.open(a.href, "Condividi", "status=0,toolbar=0,menubar=0,resizable=0,scrollbars=0,height=400,width=600");
    return false;
}

function checkResponse(r)
{
    if (r)
    {
        if (r.error)
        {
            alert(r.error.message);
            return false;
        }
        else
            return true;
    }
    else
        return false;
}

window.alert = function (msg)
{
    $('#alertMessage').html(msg);
    $('#alertDialog').dialog({
        width: 400, modal: true, draggable: false, resizable: false, title: 'Ops! Un messaggio per te...',
        buttons:
        {
            'Chiudi': function () { $(this).dialog('destroy'); }
        }
    });
    return false;
}