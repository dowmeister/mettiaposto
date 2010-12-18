function writeMessage(title, message, container) {
    $(container).empty()
    var c = $('<div class="message success"></div>');
    c.append($('<h2></h2>').html(title));
    c.append($('<p></p>').html(message));
    c.show().appendTo(container);
}

function hideMessage(container) {
    $(container + ' > .success').remove();
}

function writeError(error, container) {
    $(container).empty();
    $('<div class="message error"></div>').html('<h2>Ops! Errore!</h2>').html(error).show().appendTo(container);
}

function hideError(container) {
    $(container + ' > .error').remove();
}

function hideAjax(container) {
    $(container + ' > .ajax').remove();
}

function writeAjax(container) {
    $(container).empty();
    var c = $('<div class="message ajax"></div>').show();
    c.append('<img src="/images/ajax-loader.gif" />');
    c.appendTo(container);
}

function goTo(ref) {
    window.location.href = ref;
}

function getCode(e) {
    return (e.keyCode ? e.keyCode : e.which);
}

