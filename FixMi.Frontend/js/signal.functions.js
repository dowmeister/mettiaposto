$(document).ready(function () {
    $('#address').focus();
    $('#tabs').tabs({
        show: function (event, ui) {
            if ($(ui.panel).hasClass('map')) {
                if (!map)
                    initializeMap('map_canvas', 45.4636889, 9.1881408);
            }
        }
    });
    $('.serviceLink').click(function () {
        $('.serviceBox').hide();
        $($(this).attr('href')).show();
    });
});

function saveSignal() {
    if (!map) {
        alert('Mappa non inizializzata');
        return false;
    }

    if (!marker) {
        alert('Inserire un indirizzo o impostare correttamente il marker sulla mappa');
        return false;
    }

    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '.error', showAs: 'div', headerMessage: 'Alcuni campi non sono compilati correttamente'
    });

    validation.addRule({
        field: '#txtAddress', validateFunction: 'notEmpty', message: 'Indirizzo'
    });
    validation.addRule({
        field: '#txtSubject', validateFunction: 'notEmpty', message: 'Oggetto'
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
    validation.addRule({
        field: '#ddlCategories', validateFunction: 'checkSelected', nullValue: '-1', message: 'Categoria'
    });

    validation.validate();

    if (validation.validationResult()) {

        $('#content').localScroll({ target: 'h1' });

        $('#submitForm').hide();

        writeAjax('#messages');

        addSignal();
    }
    else
        validation.showErrorMessage();
}

function addSignal() {

    var proxy = new JSONService();

    var s = new Object();
    s.subject = $('#txtSubject').val();
    s.description = $('#txtDescription').val();
    s.showName = $('#chkShowName').val();
    s.categoryID = $('#ddlCategories').val();
    s.email = $('#txtEmail').val();
    s.latitude = marker.getPosition().lat();
    s.name = $('#txtName').val();
    s.longitude = marker.getPosition().lng();
    s.address = $('#txtAddress').val();
    s.zip = getAddressComponent(completeAddress, 'postal_code').long_name;
    s.city = getAddressComponent(completeAddress, 'locality').long_name;

    proxy.addSignal(s, addSignal_callback);
}

function addSignal_callback(r) {
    if (r.error) {
        writeError(r.error.message, '#messages');
    }
    else if (r.result) {
        writeMessage('Segnalazione salvata correttamente',
            'Segnalazione numero:' + r.result, '#messages');
    }
}
