/*
* MOBILE FUNCTIONS
*/

var initialLocation = new google.maps.LatLng(45.46265906981793, 9.187488555908203);
var currentLocation = null;
var completeAddress = null;
var mapManager = null;
var inputs;

$(document).bind("mobileinit", function ()
{
    $.extend($.mobile, {
        loadingMessage: 'Caricamento...'
    });
});

$(document).ready(function ()
{
    mapManager = $.mapManager();

    inputs = $('input,select,textarea');
    if ($.browser.mozilla)
    {
        inputs.keypress(checkEnter);
    } else
    {
        inputs.keydown(checkEnter);
    }

    if (mapManager.hasGeolocation())
    {
        navigator.geolocation.getCurrentPosition(function (position)
        {
            initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            if (initialLocation)
            {
                currentLocation = initialLocation;
                mapManager.geolocate(
                {
                    position: initialLocation, callback:
                    function (response, status) { geoLocation_callback(response, status); }
                });
            }
        }, function ()
        {
            $('#txtAddress').val('');
        },
        { enableHighAccuracy: true }
        );
    }
    else
        $('#txtAddress').val('');
});

function checkEnter(event)
{
    if (event.keyCode == 13)
    {
        var nextIndex = parseInt($(event.currentTarget).attr('tabindex'))+1;

        if ($('[tabindex="' + nextIndex + '"]').length > 0)
        {
            $('[tabindex="' + (nextIndex-1) + '"]').blur();
            $('[tabindex="' + nextIndex + '"]').focus().select();
            event.preventDefault();
        }
        else
        {
            event.eventPreventDefault();
            $(event.currentTarget).blur();
        }

        return false;
    }
}

function geoLocation_callback(response, status)
{
    if (mapManager.checkGeolocationResult(status))
    {
        completeAddress = mapManager.getGeolocationData(response, 0).address_components;
        currentLocation = mapManager.getGeolocationData(response, 0).geometry.location;
        $('#txtAddress').val(mapManager.getGeolocationData(response, 0).formatted_address);
        initMap();
        addMarker();
        $('#form').show();
    }
    else
    {
        $('#txtAddress').val('');
    }
}

function initMap()
{
    if (!currentLocation)
    {
        alert('Digita nell\'indirizzo almeno la città in cui ti trovi');
        return;
    }

    if (!mapManager.getMap('map'))
    {

        mapManager.createMap({
            lat: currentLocation.lat(), lng: currentLocation.lng, container: 'map', googleOptions:
            {
                zoom: 17,
                scaleControl: false, mapTypeControl: false, disableDoubleClickZoom: true,
                scrollwheel: false, streetViewControl: false, draggable: false
            }
        });

        $('#map').show();

        addMarker();
    }
}

function addMarker()
{
    mapManager.removeAllMarkers();

    mapManager.addMarker({
        id: 'signal0', position: currentLocation, draggable: true, image: MARKERIMAGE_ALERT,
        mapID: 'map', center: true,
        dragEnd: function () { geolocationOnMove(mapManager.getMarker('signal0').obj.getPosition()); }
    });
}

function geolocationOnMove(position)
{
    currentLocation = position;
    mapManager.geolocate({ position: position, mapID: 'map',
        callback: function (response, status) { geoLocation_callback(response, status); }
    });
}

function geolocalizeByAddress()
{
    if ($('#txtAddress').val() != '')
    {
        mapManager.geolocate({ address: $('#txtAddress').val(), mapID: 'map',
            callback: function (response, status) { geoLocation_callback(response, status); }
        });
    }
    else
        alert('Inserisci un indirizzo');
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
//        $('.submit').hide();

        /*writeAjax('#messages');*/

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

    s.address = mapManager.getAddressComponent(completeAddress, 'route').long_name;

    if (mapManager.getAddressComponent(completeAddress, 'street_number').long_name != '')
        s.address += ', ' + mapManager.getAddressComponent(completeAddress, 'street_number').long_name;

    s.zip = mapManager.getAddressComponent(completeAddress, 'postal_code').long_name;
    s.city = mapManager.getAddressComponent(completeAddress, 'locality').long_name;

    if (mapManager.getMap('map'))
        s.zoom = mapManager.getZoom('map')
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
    /*hideAjax('#messages');*/

    if (r.error)
    {
        /*writeError(r.error.message, '#messages');*/
    }
    else if (r.result)
    {
        $.mobile.changePage($('#submitResult'));

        var text = 'La segnalazione è stata salvata correttamente.<br/><a rel="external" href="/' + r.result.city.toLowerCase() + '/' + r.result.signalID + '/segnalazione.aspx">Clicca qui</a> per visualizzare la pagina di dettaglio.';

        writeMessage('Segnalazione inviata', text, '#messages');
    }
}