
var mapManager;

function initAddPlacePage(city)
{
    mapManager = $.mapManager();

    $('#tabs').tabs();
    mapManager.geolocate({ address: city + ', Italia',
        callback: function (response, status)
        {
            geolocationByAddress_callback(response, status);
        }
    });
}

function geolocationByAddress_callback(r, status, options)
{
    if (mapManager.checkGeolocationResult(status))
    {
        var data = mapManager.getGeolocationData(r, 0);

        mapManager.createMap({
            container: 'map', position: data.geometry.location, googleOptions: {
                scaleControl: false, mapTypeControl: false, streetViewControl: false
            }
        });

        mapManager.addMarker({ id: 'place0', image: MARKERIMAGE_ALERT, mapID: 'map', position: data.geometry.location, center: true, draggable: true });
    }
}