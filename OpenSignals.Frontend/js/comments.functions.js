/*
* COMMENT FUNCTIONS
*/

function getComments(offset)
{
    writeAjax('#commentsMessages');
    $('#comments').empty();
    var proxy = new JSONService();
    var params = new Object();
    params["signalID"] = currentSignalID;
    params["offset"] = offset;

    params = addSessionKey(params);

    proxy.getComments(params, getComments_callback);
}

function getComments_callback(r)
{
    hideAjax('#commentsMessages');

    if (r.error)
    {
        writeError(r.error.message, '#commentsMessages');
    }
    else
    {
        $('#comments').html(r.result);
        $('.photo > a').fancybox();
    }
}

function addComment(signalID)
{
    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '#submitCommentMessage', showAs: 'div', headerMessage: 'Alcuni campi non sono compilati correttamente'
    });

    validation.addRule({
        field: '#txtDescription', validateFunction: 'notEmpty', message: 'Descrizione'
    });
    validation.addRule({
        field: '#txtName', validateFunction: 'notEmpty', message: 'Nome'
    });
    validation.addRule({
        field: '#txtEmail', validateFunction: 'notEmpty', message: 'E-Mail vuota'
    });
    validation.addRule({
        field: '#txtEmail', validateFunction: 'validEmail', message: 'E-Mail non valida'
    });

    validation.validate();

    if (validation.validationResult())
    {
        $('#commentForm').hide();

        writeAjax('#submitCommentMessage');

        _addComment();
    }
    else
        validation.showErrorMessage();
}

function _addComment()
{
    var proxy = new JSONService();

    var c = new Object();
    c.signalID = currentSignalID;
    c.text = $('#txtDescription').val();
    c.authorName = $('#txtName').val();
    c.authorEmail = $('#txtEmail').val();
    c.showAuthorName = document.getElementById('chkPublicName').checked;
    c.attachment = '';

    if ($('#ddlStatus').val() == '2')
        c.setSignalResolved = true;

    if ($('#fuFile').val() != '')
    {
        $.ajaxFileUpload
        (
            {
                url: '/Ajax/Upload.aspx',
                secureuri: false,
                fileElementId: 'fuFile',
                dataType: 'json',
                success: function (data, status)
                {
                    if (data.error)
                        alert(data.error);
                    else
                    {
                        c.attachment = data.fileName;
                        proxy.addComment(c, ajaxSessionKey, addComment_callback);
                    }
                },
                error: function (data, status, e)
                {
                    alert(e);
                }
            });
    }
    else
        proxy.addComment(c, ajaxSessionKey, addComment_callback);
}

function addComment_callback(r)
{
    hideAjax('#submitCommentMessage');

    if (r.error)
        writeError(r.error.message, '#submitCommentMessage');
    else
    {
        if (r.result != 0)
            writeMessage('Grazie per il tuo commento!', '#submitCommentMessage');
    }

    $('#commentForm').show();
    $('#commentForm > input, textarea').val('');

    getComments(0);
}

function subscribeSignal()
{
    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '#subscribeSignalMessages', showAs: 'div', headerMessage: 'Alcuni campi non sono compilati correttamente'
    });

    validation.addRule({
        field: '#txtSubscribeEmail', validateFunction: 'notEmpty', message: 'E-Mail vuota'
    });
    validation.addRule({
        field: '#txtSubscribeEmail', validateFunction: 'validEmail', message: 'E-Mail non valida'
    });

    validation.validate();

    if (validation.validationResult())
    {
        writeAjax('#subscribeSignalMessages');

        var proxy = new JSONService();
        var params = new Object();

        params["signalID"] = currentSignalID;
        params["email"] = $('#txtSubscribeEmail').val();
        params = addSessionKey(params);

        proxy.subscribeSignal(params, subscribeSignal_callback);
    }
    else
        validation.showErrorMessage();
}

function subscribeSignal_callback(r)
{
    hideAjax('#subscribeSignalMessages');
    clearMessages('#subscribeSignalMessages');

    if (r.error)
        writeError(r.error.message, '#subscribeSignalMessages');
    else
    {
        writeMessage('Ti sei iscritto a questa segnalazione', "In questo modo potrai rimanere aggiornato sullo stato e i commenti della segnalazione", '#subscribeSignalMessages');
    }
}