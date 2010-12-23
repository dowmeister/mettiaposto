$(document).ready(function ()
{
    $('#tabs').tabs({
        show: function (event, ui)
        {
            if ($(ui.panel).attr('map'))
            {
                var mapOpts = {
                    zoom: 8, streetViewControl: false,
                    mapTypeControl: false, scrollwheel: false
                };

                var mapDiv = $(ui.panel).attr('mapDiv');
                var map = getMap(mapDiv);

                if (!map)
                {
                    initializeMap(mapDiv, 45.4636889, 9.1881408, mapOpts);
                }
            }
        }
    });
});

function showForm()
{
    $('#list').empty();
    hideMessage('#searchMessages');
    $('.submitForm').show();    
}

function searchSignals(start)
{
    $('.submitForm').hide();
    writeAjax('#searchMessages');
    $('#list').empty();

    var proxy = new JSONService();
    var params = new Object();
    params["address"] = $('#txtAddress').val();
    params["zip"] = $('#txtZip').val();
    params["city"] = $('#lblCity').html();
    params["categoryID"] = parseInt($('#ddlCategories').val());
    params["status"] = parseInt($('#ddlStatus').val());
    params["start"] = start;

    proxy.searchSignals(params, searchSignals_callback);
}

function searchSignals_callback(r)
{
    hideAjax('#searchMessages');

    if (r.error)
    {
        writeError(r.error.message, '#searchMessages');
    }
    else if (r.result)
    {
        removeAllMarkers();

        if (r.result.signals.length > 0)
        {
            writeMessage('Trovati ' + r.result.signals.length + ' risultati', '<a href="#" onclick="showForm();">Effettua una nuova ricerca</a>','#searchMessages');

            var map = getMap('map_canvas').obj;
            var bounds = new google.maps.LatLngBounds();

            for (var i = 0; i < r.result.signals.length; i++)
            {
                var s = r.result.signals[i];
                var signal = s.signal;

                var myLatLng = new google.maps.LatLng(signal.latitude, signal.longitude);

                bounds.extend(myLatLng);

                var m = createMarker('signalMarker' + signal.signalID, myLatLng, false, map);
                var w = new google.maps.InfoWindow({ content: s.description, maxWidth: 300 });
                google.maps.event.addListener(getMarker('signalMarker' + signal.signalID), 'click', function () { w.open(map, getMarker('signalMarker' + signal.signalID)); });
            }

            $('#list').html(r.result.html);

            map.fitBounds(bounds);
            map.setCenter(bounds.getCenter());
        }
        else
            writeError('Nessuna segnalazione trovata con i parametri di ricerca specificati', '#searchMessages');
    }
}