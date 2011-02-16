/*
* MOBILE FUNCTIONS
*/

var initialLocation = new google.maps.LatLng(45.46265906981793, 9.187488555908203);
var currentLocation = null;
var completeAddress = null;
var marker = null;
var map = null;

$(document).ready(function ()
{
    if (navigator.geolocation)
    {
        navigator.geolocation.getCurrentPosition(function (position)
        {
            initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            if (initialLocation)
            {
                currentLocation = initialLocation;
                var geoCoder = new google.maps.Geocoder();
                geoCoder.geocode({ latLng: initialLocation }, function (response, status) { geoLocation_callback(response, status); });
            }
        }, function ()
        {

        },
        { enableHighAccuracy: true }
        );
    }
});

function geoLocation_callback(response, status)
{
    if (checkGeolocationResult(status))
    {
        completeAddress = getGeolocationData(response, 0).address_components;
        currentLocation = getGeolocationData(response, 0).geometry.location;
        $('#txtAddress').val(getGeolocationData(response, 0).formatted_address);
        initMap();
        addMarker();
    }
}

function initMap()
{
    if (!currentLocation)
    {
        alert('Digita nell\'indirizzo almeno la città in cui ti trovi');
        return;
    }

    if (!map)
    {
        $('#map').show();

        var mapOpts = {
            zoom: 17,
            scaleControl: false, mapTypeControl: false, disableDoubleClickZoom: true,
            scrollwheel: false, streetViewControl: false
        };

        map = initializeMap('map', currentLocation.lat(), currentLocation.lng(), mapOpts);

        addMarker();
    }
}

function addMarker()
{
    if (marker)
        marker.setMap(null);

    marker = new google.maps.Marker({
        map: map,
        position: currentLocation,
        draggable: true,
        icon: new google.maps.MarkerImage(MARKERIMAGE_ALERT, new google.maps.Size(32, 32))
    });

    map.setCenter(currentLocation);

    google.maps.event.addListener(marker, 'dragend', function () { geolocationOnMove(marker.getPosition()); });
}

function geolocationOnMove(position)
{
    var geoCoder = new google.maps.Geocoder();
    currentLocation = position;
    $('#txtAddress').val('Localizzazione in corso...');
    geoCoder.geocode({ latLng: position }, function (response, status) { geoLocation_callback(response, status); });
}

function geolocalizeByAddress()
{
    var geoCoder = new google.maps.Geocoder();
    geoCoder.geocode({ address: $('#txtAddress').val() }, function (response, status) { geoLocation_callback(response, status); });
    $('#txtAddress').val('Localizzazione in corso...');
}

function sendSignal()
{
    if (!currentLocation)
    {
        alert('Inserire un indirizzo o impostare correttamente il marker sulla mappa');
        return false;
    }

    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '#messages', showAs: 'alert', headerMessage: 'Alcuni campi non sono compilati correttamente'
    });

    validation.addRule({
        field: '#txtSubject', validateFunction: 'notEmpty', message: 'Oggetto'
    });
    validation.addRule({
        field: '#txtDescription', validateFunction: 'notEmpty', message: 'Descrizione'
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
        $('.submit').hide();

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
    s.showName = false;
    s.categoryID = $('#ddlCategories').val();
    s.email = $('#txtEmail').val();
    s.latitude = currentLocation.lat();
    s.name = 'Anonimo';
    s.longitude = currentLocation.lng();

    s.address = getAddressComponent(completeAddress, 'route').long_name;

    if (getAddressComponent(completeAddress, 'street_number').long_name != '')
        s.address += ', ' + getAddressComponent(completeAddress, 'street_number').long_name;

    s.zip = getAddressComponent(completeAddress, 'postal_code').long_name;
    s.city = getAddressComponent(completeAddress, 'locality').long_name;

    if (map)
        s.zoom = map.getZoom();
    else
        s.zoom = 16;

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
    hideAjax('#messages');

    if (r.error)
    {
        writeError(r.error.message, '#messages');
    }
    else if (r.result)
    {
        var text = 'La segnalazione è stata salvata correttamente.<br/><a href="/' + r.result.city.toLowerCase() + '/' + r.result.signalID + '/segnalazione.aspx">Clicca qui</a> per visualizzare la pagina di dettaglio.';

        writeMessage('Segnalazione inviata', text, '#messages');
    }
}