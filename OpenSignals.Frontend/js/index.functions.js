/*
* HOME PAGE FUNCTIONS
*/

var initial = 'CAP, indirizzo, quartiere...';
var mapManager;

$(document).ready(function ()
{
    $('#txtSearch').val(initial);
    $('#txtSearch').click(function () { $('#txtSearch').val(''); });
    $('#txtSearch').focus();
    $(document).keypress(function (event)
    {
        if (event.which == 13)
        {
            search();
            return false;
        }
    });

    searchSignals();
});

function search()
{
    if ($('#txtSearch').val() == '' || $('#txtSearch').val() == initial)
        alert('Inserire un indirizzo valido');
    else
        window.location.href = currentCity.link + encodeURI($('#txtSearch').val()) + '/invia.aspx';
}

function searchSignals()
{
    var proxy = new JSONService();
    var params = new Object();
    params["address"] = '';
    params["zip"] = '';
    params["city"] = currentCity.name;
    params["categoryID"] = -1;
    params["status"] = 1;
    params["start"] = 0;

    params = addSessionKey(params);

    var options = {
        googleOptions: {
            zoom: currentCity.zoom,
            scaleControl: false, mapTypeControl: false, streetViewControl: false
        }, container: 'map', lat: currentCity.lat, lng: currentCity.lng
    }

    mapManager = $.mapManager();
    mapManager.createMap(options);

    proxy.searchSignals(params, searchSignals_callback);
}

function searchSignals_callback(r)
{
    if (r.result)
    {
        if (r.result.signals.length > 0)
        {
            {
                var bounds = new google.maps.LatLngBounds();

                for (var i = 0; i < r.result.signals.length; i++)
                {
                    var s = r.result.signals[i];
                    var signal = s.signal;

                    var myLatLng = new google.maps.LatLng(signal.latitude, signal.longitude);

                    bounds.extend(myLatLng);

                    var image = '';

                    if (s.signal.status == 2)
                        image = MARKERIMAGE_OK;
                    else
                        image = MARKERIMAGE_ALERT;

                    var m = mapManager.addMarker({ mapID: 'map', id: 'signalMarker' + signal.signalID, position: myLatLng, draggable: false, image: image });
                    var w = new google.maps.InfoWindow({ content: s.description, maxWidth: 300 });
                    google.maps.event.addListener(m, 'click', function () { w.open(mapManager.getMap('map').obj, this) });
                }

                mapManager.fitBounds({ mapID: 'map', bounds: bounds, center: true });
            }
        }
    }
}