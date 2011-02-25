/*
* GENERAL PURPOSE FUNCTIONS
*/

$(function () {
    $('#feedback').tabSlideOut({
        tabHandle: '.handle',                     //class of the element that will become your tab
        pathToTabImage: '/images/contact_tab.gif', //path to the image for the tab //Optionally can be set using css
        imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
        imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
        tabLocation: 'left',                      //side of screen where tab lives, top, right, bottom, or left
        speed: 300,                               //speed of animation
        action: 'click',                          //options: 'click' or 'hover', action to trigger animation
        topPos: '200px',                          //position from the top/ use if tabLocation is left or right
        leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
        fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
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

