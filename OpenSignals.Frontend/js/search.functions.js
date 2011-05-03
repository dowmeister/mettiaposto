/*
* SEARCH FUNCTIONS
*/

var mapManager;

$(document).ready(function ()
{
    mapManager = $.mapManager();

    $('#tabs').tabs({
        show: function (event, ui)
        {
            searchSignals(0);
        }
    });
});

function showForm()
{
    $('#searchList').empty();
    clearMessages('#searchMessages');
    $('#submitForm').show();
}

function searchSignals(start)
{
    $('#submitForm').hide();
    clearMessages('#searchMessages');
    writeAjax('#searchMessages');
    $('#searchList').empty();

    var proxy = new JSONService();
    var params = new Object();
    params["address"] = $('#txtAddress').val();
    params["zip"] = $('#txtZip').val();
    params["city"] = $('#lblCity').html();
    params["categoryID"] = parseInt($('#ddlCategories').val());
    params["status"] = parseInt($('#ddlStatus').val());
    params["start"] = start;

    mapManager.createMap({ container: 'map', lat: currentCity.lat, lng: currentCity.lng, googleOptions: {
        zoom: currentCity.zoom, streetViewControl: false,
        mapTypeControl: false, scrollwheel: false
    }
    });

    params = addSessionKey(params);

    proxy.searchSignals(params, searchSignals_callback);
}

function searchSignals_callback(r)
{
    hideAjax('#searchMessages');

    if (r.error)
    {
        writeError(r.error.message, '#searchMessages');
        $('#submitForm').show();
    }
    else if (r.result)
    {
        mapManager.removeAllMarkers();

        if (r.result.signals.length > 0)
        {
            writeMessage(r.result.signals.length + ' segnalazioni trovate', '<a href="#" onclick="showForm();">Effettua una nuova ricerca</a>', '#searchMessages');

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

                var m = mapManager.addMarker({ id: 'signalMarker' + signal.signalID, position: myLatLng, draggable: false, mapID: 'map', image: image });
                var w = new google.maps.InfoWindow({ content: s.description, maxWidth: 300 });
                google.maps.event.addListener(m, 'click', function () { w.open(mapManager.getMap('map').obj, this) });
            }

            $('#searchList').html(r.result.html);

            mapManager.fitBounds({ mapID: 'map', bounds: bounds });

            mapManager.setZoom({ mapID: 'map', zoom: 15 });
        }
        else
            writeError('Nessuna segnalazione trovata con i parametri di ricerca specificati<p><a href="#" onclick="showForm();">Effettua una nuova ricerca</a> o <a href="' + currentCity.link + 'invia.aspx">inserisci la prima segnalazione</a> per questa città.</a></p>', '#searchMessages');
    }
}