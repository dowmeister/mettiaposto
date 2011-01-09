var maps = new Array();
var markers = new Array();
var completeAddress;
var currentMap;
var currentCity = '';

function getMap(id)
{
    for (var i = 0; i < maps.length; i++)
    {
        if (maps[i].id == id)
            return maps[i];
    }

    return false;
}

function getMarkerObject(id)
{
    for (var i = 0; i < markers.length; i++)
    {
        if (markers[i].id == id)
            return markers[i].obj;
    }

    return false;
}

function getMarker(id)
{
    for (var i = 0; i < markers.length; i++)
    {
        if (markers[i].id = id)
            return markers[i];
    }

    return false;
}


function initializeMap(mapDiv, lat, long, myOptions)
{
    var latlng = new google.maps.LatLng(lat, long);

    myOptions.mapTypeId = google.maps.MapTypeId.ROADMAP;
    myOptions.center = latlng;

    var m = new google.maps.Map(document.getElementById(mapDiv), myOptions);

    maps.push({ id: mapDiv, obj: m });
}

function geolocationByAddress(address, mapId)
{
    var m = getMap(mapId);

    if (m)
    {
        currentMap = m.id;
        var addr = address + ', ' + $('#ltCity').html();
        var geoCoder = new google.maps.Geocoder();
        geoCoder.geocode({ address: addr }, function (response, status) { geolocationByAddress_callback(response, status); });
    }
}

function geolocationByAddress_callback(r, status)
{
    if (checkGeolocationResult(status))
    {
        var data = r[0];

        var m = getMap(currentMap);

        if (m)
        {
            m.obj.setCenter(data.geometry.location);

            switch (data.types[0])
            {
                case 'street_address':
                    m.obj.setZoom(16);
                    setMarker('geoLocatedMarker0', data.geometry.location, true, m.id, true, true);
                    break;
                case 'postal_code':
                    m.obj.setZoom(14);
                    break;
                case 'sublocality':
                    m.obj.setZoom(13);
                    break;
                case 'route':
                    m.obj.setZoom(15);
                    setMarker('geoLocatedMarker0', data.geometry.location, true, m.id, true, true);
                    break;
                case 'locality':
                    m.obj.setZoom(7);
                    break;
            }
        }
    }

    currentMap = null;
}

function removeMarkerFromMap(marker)
{
    marker.setMap(null);
}

function removeMarker(index)
{
    removeMarkerFromMap(markers[index].obj);
    markers.splice(index, 1);
}

function removeMarkerById(id)
{
    for (var i = markers.length - 1; i >= 0; i--)
    {
        if (markers[i].id == id)
        {
            removeMarker(i);
            break;
        }
    }    
}

function removeAllMarkers()
{
    for (var i = markers.length - 1; i >= 0; i--)
    {
        removeMarker(i);
    }
}

function createMarker(id, location, draggable, m)
{
    removeMarkerById(id);

    var marker = new google.maps.Marker({
        map: m,
        position: location,
        draggable: draggable,
        icon: new google.maps.MarkerImage('/images/alert.png', new google.maps.Size(32, 32))
    });

    markers.push({ id: id, obj: marker, map: m });

    return marker;
}

function setMarker(id, location, draggable, mapId, localize, center)
{
    var m = getMap(mapId);

    var marker = createMarker(id, location, draggable, m.obj);

    if (center)
        m.obj.setCenter(location);

    if (draggable)
        google.maps.event.addListener(marker, 'dragend', function () { geoLocationByLatLng(marker.getPosition(), 'completeAddress'); });

    if (localize)
        geoLocationByLatLng(location);

    return marker;
}

function geoLocationByLatLng(position)
{
    var geoCoder = new google.maps.Geocoder();
    geoCoder.geocode({ latLng: position }, function (response, status) { geoLocationByLatLng_callback(response, status); });
}

function geoLocationByLatLng_callback(response, status)
{
    if (checkGeolocationResult(status))
    {
        var data = getGeolocationData(response, 0);

        completeAddress = data.address_components;

        var address = getAddressComponent(data.address_components, 'route').long_name;
        if (getAddressComponent(data.address_components, 'street_number') != '')
            address += ', ' + getAddressComponent(data.address_components, 'street_number').long_name;

        $('#txtAddress').val(address);

        $('#completeAddress').show();
        $('#completeAddress').html('Indirizzo completo: ' + data.formatted_address);
    }
}

function checkGeolocationResult(status)
{
    if (status == google.maps.GeocoderStatus.OK)
        return true;
    else
        return false;
}

function getGeolocationData(r, index)
{
    return data = r[index];
}

function getAddressComponent(components, type)
{
    for (var i = 0; i < components.length; i++)
    {
        for (var j = 0; j < components[i].types.length; j++)
        {
            if (components[i].types[j] == type)
                return components[i];
        }
    }
    return { long_name: '' };
}