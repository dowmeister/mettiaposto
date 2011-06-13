/*
* COMMENT FUNCTIONS
*/

function getComments(offset)
{
    writeAjax('#commentsMessages');
    $('#comments').empty();
    var proxy = new JSONService();
    var params = new Object();
    params["signalID"] = currentMarker.id;
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
        /*if (r.result.count == 0)
            $('#comments').html('<div class="item"><div class="comment">Nessun commento, puoi essere il primo!</div></div>');
        else*/
        if (r.result.count > 0)
            $('#comments').html(r.result.html);

        $('.photo > a').fancybox();
    }
}

function addComment(signalID)
{
    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '#commentsMessages', showAs: 'div', headerMessage: 'Alcuni campi non sono compilati correttamente'
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

        writeAjax('#commentsMessages');

        _addComment();
    }
    else
        validation.showErrorMessage();
}

function _addComment()
{
    var proxy = new JSONService();

    var c = new Object();
    c.signalID = currentMarker.id;
    c.text = $('#txtDescription').val();
    c.authorName = $('#txtName').val();
    c.authorEmail = $('#txtEmail').val();
    c.showAuthorName = document.getElementById('chkPublicName').checked;
    c.attachment = '';

    if (socialUser)
    {
        c.authorReferenceKey = socialUser.id;
        c.authorReferenceType = 1;
    }
    else
    {
        c.authorReferenceKey = '';
        c.authorReferenceType = -1;
    }

    if ($('#fuFile').val() != '')
    {
        $.ajaxFileUpload
        (
            {
                url: composeAjaxUploadUrl(),
                secureuri: false,
                fileElementId: 'fuFile',
                dataType: 'json',
                success: function (data, status)
                {
                    if (data.error)
                    {
                        if (data.error == 'ERROR')
                            alert(data.errorMessage);
                        else if (data.error = 'WRONG_EXT')
                            alert('Il file caricato non è di tipo immagine');
                        else if (data.error == 'NO_FILE')
                            alert('Il file selezionato non è stato inviato correttamente');
                        else if (data.error == 'TOO_MUCH')
                            alert('Il file selezionato è troppo grande: dimensione massima 10MB');

                        hideAjax('#commentsMessages');
                        $('#commentForm').show();
                    }
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
    hideAjax('#commentsMessages');

    if (r.error)
        writeError(r.error.message, '#commentsMessages');
    else
    {
        if (r.result != 0)
        {
            writeMessage('Grazie per il tuo commento!', '#commentsMessages');

            if (fbLogged)
                fbPostToFeed({ title: document.title, message: 'Ho commentato una segnalazione su Mettiaposto', caption: $('#ltCategory').html(), description: $('#divDescription').html(), link: document.location.href, picture: $('#imgPhoto').attr('src') });
        }
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

        params["signalID"] = currentMarker.id;
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

function openReportAbuseDialog()
{
    $('#reportAbuseDialog').dialog({
        width: 470, title: 'Segnala un contenuto improprio o errato', resizable: false, draggable: false,
        modal: true, buttons: { 'Segnala': function () { reportAbuse(); } }
    });
}

function reportAbuse()
{
    $('#reportAbuseForm').hide();
    writeAjax('#reportAbuseMessages');
    var proxy = new JSONService();
    proxy.reportAbuse($('#txtReportAbuseMesasage').val(), ajaxSessionKey, reportAbuse_callback);
}

function reportAbuse_callback(r)
{
    hideAjax('#reportAbuseMessages');

    if (checkResponse(r)) {
        writeMessage('Grazie!', 'La tua segnalazione contribuisce a rendere migliore e più affidabile questo servizio', '#reportAbuseMessages');
    }
    else {
        writeError(r.error.message, '#reportAbuseMessages');
    }
    
}