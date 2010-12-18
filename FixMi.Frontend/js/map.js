var map;
var marker
var completeAddress;

function initializeMap(mapDiv, lat, long) {
    var latlng = new google.maps.LatLng(lat, long);
    var myOptions = {
        zoom: 8,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        scaleControl: false, mapTypeControl: false, disableDefaultUI: true, disableDoubleClickZoom: true,
        scrollwheel: false
        //click: function () { setMarker(); }
    };

    map = new google.maps.Map(document.getElementById(mapDiv), myOptions);
}

function geolocationByAddress(address) {
    if (map) {
        var addr = address + ', Milano';
        var geoCoder = new google.maps.Geocoder();
        geoCoder.geocode({ address: addr }, function (response, status) { geolocation_response(response, status); });
    }
}

function geolocation_response(r, status) {
    if (status == google.maps.GeocoderStatus.OK) {
        var data = r[0];

        map.setCenter(data.geometry.location);

        switch (data.types[0]) {
            case 'street_address':
                map.setZoom(16);
                setMarker(data.geometry.location);
                break;
            case 'postal_code':
                map.setZoom(14);
                break;
            case 'sublocality':
                map.setZoom(13);
                break;
            case 'route':
                map.setZoom(15);
                setMarker(data.geometry.location);
                break;
        }
    }
}

function setMarker(location) {
    if (marker)
        marker.setMap(null);

    marker = new google.maps.Marker({
        position: location,
        map: map,
        draggable: true
    });
    
    geoLocationByLatLng(location);

    google.maps.event.addListener(marker, 'dragend', function () { geoLocationByLatLng(marker.getPosition(), 'completeAddress'); });
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