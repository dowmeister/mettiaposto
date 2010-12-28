var currentSignalID = 0;

$(document).ready(function ()
{
    $('#address').focus();
    $('#tabs').tabs({
        show: function (event, ui)
        {
            if ($(ui.panel).attr('map'))
            {
                var mapOpts = {
                    zoom: 6,
                    scaleControl: false, mapTypeControl: false, disableDefaultUI: true, disableDoubleClickZoom: true,
                    scrollwheel: false
                };

                var mapDiv = $(ui.panel).attr('mapDiv');
                var map = getMap(mapDiv);

                if (!map)
                {
                    initializeMap(mapDiv, 42.53, 13.66, mapOpts);

                    if (mapDiv == 'mapNearby')
                    {
                        getSignalsNeraby($(ui.panel).attr('zip'));
                    }
                }
            }
        }
    });

    $('.serviceLink').click(function ()
    {
        $('.serviceBox').hide();

        var link = $(this);
        var box = $(link.attr('href'));

        if (link.attr('href') == '#commentsBox')
            getComments(0);

        box.slideDown();

        return false;
    });
});

function saveSignal()
{
    if (!getMap('map_canvas'))
    {
        alert('Mappa non inizializzata');
        return false;
    }

    if (!getMarker('geoLocatedMarker0'))
    {
        alert('Inserire un indirizzo o impostare correttamente il marker sulla mappa');
        return false;
    }

    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '#validationError', showAs: 'div', headerMessage: 'Alcuni campi non sono compilati correttamente'
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

    if (validation.validationResult())
    {
        $('#submitForm').hide();

        writeAjax('#messages');

        addSignal();
    }
    else
        validation.showErrorMessage();
}

function addSignal()
{

    var proxy = new JSONService();

    var s = new Object();
    s.subject = $('#txtSubject').val();
    s.description = $('#txtDescription').val();
    s.showName = document.getElementById('chkPublicName').checked;
    s.categoryID = $('#ddlCategories').val();
    s.email = $('#txtEmail').val();
    s.latitude = getMarker('geoLocatedMarker0').getPosition().lat();
    s.name = $('#txtName').val();
    s.longitude = getMarker('geoLocatedMarker0').getPosition().lng();
    s.address = $('#txtAddress').val();
    s.zip = getAddressComponent(completeAddress, 'postal_code').long_name;
    s.city = getAddressComponent(completeAddress, 'locality').long_name;
    s.zoom = getMap('map_canvas').obj.getZoom();
    s.attachment = '';

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
                        s.attachment = data.fileName;
                        proxy.addSignal(s, ajaxSessionKey, addSignal_callback);
                    }
                },
                error: function (data, status, e)
                {
                    alert(e);
                }
            });
    }
    else
        proxy.addSignal(s, ajaxSessionKey, addSignal_callback);
}

function addSignal_callback(r)
{
    if (r.error)
    {
        writeError(r.error.message, '#messages');
    }
    else if (r.result)
    {
        var text = 'La segnalazione è stata salvata correttamente.<br/><a href="/' + r.result.city.toLowerCase() + '/' + r.result.signalID + '/segnalazione.aspx">Clicca qui</a> per visualizzare la pagina di dettaglio.';

        writeMessage('Segnalazione salvata correttamente', text, '#messages');
    }
}

function getSignalsNeraby(zipCode)
{
    var proxy = new JSONService();
    proxy.getSignalsNearby({ zip: zipCode, ajaxSessionKey: ajaxSessionKey }, getSignalsNearby_callback);
}

function getSignalsNearby_callback(r)
{
    if (r.error)
    {
        alert(r.error.message);
    }
    else if (r.result)
    {
        if (r.result.length > 0)
        {
            var map = getMap('mapNearby').obj;

            var bounds = new google.maps.LatLngBounds();

            for (var i = 0; i < r.result.length; i++)
            {
                var s = r.result[i];
                var signal = s.signal;

                var myLatLng = new google.maps.LatLng(signal.latitude, signal.longitude);

                bounds.extend(myLatLng);

                var m = createMarker('signalMarker' + signal.signalID, myLatLng, false, map);
                var w = new google.maps.InfoWindow({ content: s.description, maxWidth: 300 });
                google.maps.event.addListener(getMarker('signalMarker' + signal.signalID), 'click', function () { openInfoWindow(w, getMarker('signalMarker' + signal.signalID), map); });
            }

            map.fitBounds(bounds);
            map.setCenter(bounds.getCenter());

            if (map.getZoom() < 15)
                map.setZoom(15);
        }
    }
}

function openInfoWindow(w, marker, map)
{
    w.open(map, marker);
}