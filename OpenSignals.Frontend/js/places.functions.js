
var mapManager;

function initAddPlacePage(city)
{
    mapManager = $.mapManager();

    $('#tabs').tabs({
        show: function (event, ui)
        {
            mapManager.geolocate({ address: city + ', Italia',
                callback: function (response, status)
                {
                    geolocationByAddress_callback(response, status);
                }
            });
        } 
    });
    
}

function geolocationByAddress_callback(r, status, options)
{
    if (mapManager.checkGeolocationResult(status))
    {
        var data = mapManager.getGeolocationData(r, 0);

        mapManager.createMap({
            container: 'map', bounds: data.geometry.bounds, position: data.geometry.location, googleOptions: {
                scaleControl: false, mapTypeControl: false, streetViewControl: false, zoom: 12
            }
        });

        mapManager.addMarker({ id: 'place0', image: MARKERIMAGE_ALERT, mapID: 'map', position: data.geometry.location, center: true, draggable: true,
            dragEnd: function (event) { mapManager.setCenter({ mapID: 'map', position: event.latLng }); } 
        });
    }
}

function addPlace()
{
    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '#messages', showAs: 'div', headerMessage: 'Alcuni campi non sono compilati correttamente'
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
        $('#submitForm').hide();

        writeAjax('#messages');

        var o = new Object();
        o.name = $('#txtCity').val();
        o.email = $('#txtEmail').val();
        o.zoom = mapManager.getZoom('map');
        o.lat = mapManager.getMarker('place0').obj.getPosition().lat();
        o.lng = mapManager.getMarker('place0').obj.getPosition().lng();
        o = addSessionKey(o);
        var proxy = new JSONService();
        proxy.addPlace(o, addPlace_callback);
    }
    else
        validation.showErrorMessage();
}

function addPlace_callback(r)
{
    hideAjax('#messages');

    if (r.error)
    {
        writeError(r.error.message, '#messages');
    }
    else if (r.result)
    {
        var text = 'Appena possibile verrà validata e pubblicata e sarai avvertito con una comunicazione inviata all\'indirizzo email che hai inserito.';

        writeMessage('Grazie per aver aggiunto la tua città a Mettiaposto', text, '#messages');
    }
}