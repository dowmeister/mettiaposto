$(document).ready(function ()
{
    $('#address').focus();
    $('#tabs').tabs({
        show: function (event, ui)
        {
            if ($(ui.panel).attr('map'))
            {
                var mapDiv = $(ui.panel).attr('mapDiv');
                var map = getMap(mapDiv);

                if (!map)
                {
                    initializeMap(mapDiv, 45.4636889, 9.1881408);

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
        $($(this).attr('href')).slideDown();
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
    s.showName = $('#chkShowName').val();
    s.categoryID = $('#ddlCategories').val();
    s.email = $('#txtEmail').val();
    s.latitude = getMarker('geoLocatedMarker0').getPosition().lat();
    s.name = $('#txtName').val();
    s.longitude = getMarker('geoLocatedMarker0').getPosition().lng();
    s.address = $('#txtAddress').val();
    s.zip = getAddressComponent(completeAddress, 'postal_code').long_name;
    s.city = getAddressComponent(completeAddress, 'locality').long_name;
    s.zoom = getMap('map_canvas').obj.getZoom();

    proxy.addSignal(s, addSignal_callback);
}

function addSignal_callback(r)
{
    if (r.error)
    {
        writeError(r.error.message, '#messages');
    }
    else if (r.result)
    {
        writeMessage('Segnalazione salvata correttamente',
            'Segnalazione numero:' + r.result, '#messages');
    }
}

function getSignalsNeraby(zipCode)
{
    var proxy = new JSONService();
    proxy.getSignalsNearby(zipCode, getSignalsNearby_callback);
}

function getSignalsNearby_callback(r)
{
    if (r.error)
    {
        alert(r.error.message);
    }
    else if (r.result)
    {
        var map = getMap('mapNearby').obj;

        var bounds = new google.maps.LatLngBounds();
        var sw = new google.maps.LatLng(0, 0);
        var ne = new google.maps.LatLng(0, 0);

        for (var i = 0; i < r.result.length; i++)
        {
            var s = r.result[i];

            var myLatLng = new google.maps.LatLng(s.latitude, s.longitude);

            bounds.extend(myLatLng);

            var m = createMarker('signalMarker' + s.signalID, myLatLng, false, map);
            var w = new google.maps.InfoWindow({ content: 'xxxxxx' });
            google.maps.event.addListener(m, 'click', function () { w.open(map, m); });
        }

        map.fitBounds(bounds);
        map.setCenter(bounds.getCenter());
    }
}

function openInfoWindow(w, marker, map)
{
    w.open(map, marker);
}