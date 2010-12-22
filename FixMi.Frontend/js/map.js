var maps = new Array();
var markers = new Array();
var completeAddress;
var currentMap;

function getMap(id) {
    for (var i = 0; i < maps.length; i++) {
        if (maps[i].id == id)
            return maps[i];
    }
    return false;
}

function getMarker(id) {
    for (var i = 0; i < markers.length; i++) {
        if (markers[i].id = id)
            return markers[i].obj;
    }
    return false;
}

function initializeMap(mapDiv, lat, long, myOptions) {
    var latlng = new google.maps.LatLng(lat, long);

    myOptions.mapTypeId = google.maps.MapTypeId.ROADMAP;
    myOptions.center = latlng;

    map = new google.maps.Map(document.getElementById(mapDiv), myOptions);
    
    maps.push({ id: mapDiv, obj: map });
}

function geolocationByAddress(address, mapId) {

    var map = getMap(mapId);

    if (map) {
        currentMap = map.id;
        var addr = address + ', Milano';
        var geoCoder = new google.maps.Geocoder();
        geoCoder.geocode({ address: addr }, function (response, status) { geolocation_response(response, status); });
        /*$.ajax({
            url: 'http://maps.googleapis.com/maps/api/geocode/json',
            success: function (data, textStatus) { geolocation_response(response, status); },
            data: { address: addr, region: 'it', sensor: true }
        });*/
    }
}

function geolocation_response(r, status) {

    if (checkGeolocationResult(status)) {
        var data = r[0];

        var map = getMap(currentMap);

        map.obj.setCenter(data.geometry.location);

        switch (data.types[0]) {
            case 'street_address':
                map.obj.setZoom(16);
                setMarker('geoLocatedMarker0', data.geometry.location, true, map.id, true, true);
                break;
            case 'postal_code':
                map.obj.setZoom(14);
                break;
            case 'sublocality':
                map.obj.setZoom(13);
                break;
            case 'route':
                map.obj.setZoom(15);
                setMarker('geoLocatedMarker0', data.geometry.location, true, map.id, true, true);
                break;
        }
    }
    currentMap = null;
}

function removeMarker(id)
{
    var marker = getMarker(id);
    marker.setMap(null);
}

function removeAllMarkers()
{
    for (var i = 0; i < markers.length; i++)
    {
        removeMarker(markers[i].id);
    }
}

function createMarker(id, location, draggable, map) {
     
    var marker = new google.maps.Marker({
        map: map,
        position: location,
        draggable: draggable,
        icon: new google.maps.MarkerImage('/images/alert.png', new google.maps.Size(32, 32))
    });

    markers.push({ id: id, obj: marker, map: map });
    
    return marker;
}

function setMarker(id, location, draggable, mapId, localize, center) {

    var map = getMap(mapId);

    var marker = createMarker(id, location, draggable, map.obj);

    //getMap(mapId).markerManager.addMarker(marker);
    //getMap(mapId).markerManager.refresh();

    if (center)
        map.obj.setCenter(location);

    if (draggable)
        google.maps.event.addListener(marker, 'dragend', function () { geoLocationByLatLng(marker.getPosition(), 'completeAddress'); });

    if (localize)
        geoLocationByLatLng(location);

    return marker;
}

function geoLocationByLatLng(position) {    
    var geoCoder = new google.maps.Geocoder();
    geoCoder.geocode({ latLng: position }, function (response, status) { geoLocationByLatLng_response(response, status); });
}

function geoLocationByLatLng_response(response, status) {

    if (checkGeolocationResult(status)) {
        var data = getGeolocationData(response, 0);
        
        completeAddress = data.address_components;
        
        $('#txtAddress').val(getAddressComponent(data.address_components, 'route').long_name + ', ' +
            getAddressComponent(data.address_components, 'street_number').long_name);

        $('#completeAddress').show();
        $('#completeAddress').html('Indirizzo completo: ' + data.formatted_address);
    }
}

function checkGeolocationResult(status) {
    if (status == google.maps.GeocoderStatus.OK)
        return true;
    else
        return false;
}

function getGeolocationData(r, index) {
    return data = r[index];
}

function getAddressComponent(components, type) {
    for (var i = 0; i < components.length; i++) {
        for (var j = 0; j < components[i].types.length; j++)
        {
            if (components[i].types[j] == type)
                return components[i];
        }
    }
    return null;    
}