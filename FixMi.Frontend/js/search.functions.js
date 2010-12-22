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

function searchSignals()
{
    var proxy = new JSONService();
    var params = new Object();
    params["address"] = $('#txtAddress').val();
    params["zip"] = $('#txtZip').val();
    params["city"] = $('#lblCity').html();
    params["categoryID"] = parseInt($('#ddlCategories').val());
    params["status"] = parseInt($('#ddlStatus').val());
    params["start"] = 0;

    proxy.searchSignals(params, searchSignals_callback);
}

function searchSignals_callback(r)
{
    if (r.error)
    {
        alert(r.error.message);
    }
    else if (r.result)
    {
        removeAllMarkers();

        if (r.result.signals.length > 0)
        {
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
    }
}