/*
* SIGNALS FUNCTIONS
*/

var currentSignalID = 0;
var mapManager;
var nearbyLoaded = false;

var mapOpts = {
    zoom: 6,
    scaleControl: false, mapTypeControl: false, disableDefaultUI: true, disableDoubleClickZoom: true,
    scrollwheel: false, streetViewControl: false
};

$(document).ready(function () {
    mapManager = $.mapManager();

    $('#address').focus();
    $('#tabs').tabs({
        show: function (event, ui) {
            if ($(ui.panel).attr('map')) {
                if ($(ui.panel).attr('id') == 'map')
                    mapManager.createMap({ container: 'map', lat: 42.53, lng: 13.66, googleOptions: mapOpts });
                else {
                    getSignalsNeraby(currentMarker.zip);
                }
            }
        }
    });

    $('.serviceLink').click(function () {
        $('.serviceBox').hide();

        var link = $(this);
        var box = $(link.attr('href'));

        if (link.attr('href') == '#commentsBox')
            getComments(0);

        box.slideDown();

        return false;
    });
});

function saveSignal() {
    if (!mapManager.getMap('map')) {
        alert('Mappa non inizializzata');
        return false;
    }

    if (!mapManager.getMarker('geoLocatedMarker0')) {
        alert('Inserire un indirizzo o impostare correttamente il marker sulla mappa');
        return false;
    }

    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '#validationError', showAs: 'div', headerMessage: 'Alcuni campi non sono compilati correttamente'
    });

    validation.addRule({
        field: '#txtAddress', validateFunction: 'notEmpty', message: 'Indirizzo'
    });
    validation.addRule({
        field: '#txtSubject', validateFunction: 'notEmpty', message: 'Oggetto'
    });
    validation.addRule({
        field: '#txtDescription', validateFunction: 'notEmpty', message: 'Descrizione'
    });
    validation.addRule({
        field: '#txtName', validateFunction: 'notEmpty', message: 'Nome'
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

    if (validation.validationResult()) {
        $('#submitForm').hide();

        writeAjax('#messages');

        addSignal();
    }
    else
        validation.showErrorMessage();
}

function addSignal() {
    var proxy = new JSONService();

    var s = new Object();
    s.subject = $('#txtSubject').val();
    s.description = $('#txtDescription').val();
    s.showName = document.getElementById('chkPublicName').checked;
    s.categoryID = $('#ddlCategories').val();
    s.email = $('#txtEmail').val();
    s.latitude = mapManager.getMarker('geoLocatedMarker0').obj.getPosition().lat();
    s.name = $('#txtName').val();
    s.longitude = mapManager.getMarker('geoLocatedMarker0').obj.getPosition().lng();
    s.address = $('#txtAddress').val();
    s.zip = mapManager.getAddressComponent(completeAddress, 'postal_code').long_name;
    s.city = mapManager.getAddressComponent(completeAddress, 'locality').long_name;
    s.zoom = mapManager.getZoom('map');
    s.attachment = '';

    if ($('#fuFile').val() != '') {
        $.ajaxFileUpload
        (
            {
                url: '/Ajax/Upload.aspx',
                secureuri: false,
                fileElementId: 'fuFile',
                dataType: 'json',
                success: function (data, status) {
                    if (data.error)
                        alert(data.error);
                    else {
                        s.attachment = data.fileName;
                        proxy.addSignal(s, ajaxSessionKey, addSignal_callback);
                    }
                },
                error: function (data, status, e) {
                    alert(e);
                }
            });
    }
    else
        proxy.addSignal(s, ajaxSessionKey, addSignal_callback);
}

function geolocateByAddress() {
    var options =
    mapManager.geolocate(
        { address: $('#txtAddress').val() + ", " + $('#ltCity').html(),
            mapID: 'map',
            callback: function (response, status) {
                geolocationByAddress_callback(response, status, { map: 'map' });
            }
        });
}

function geolocationByAddress_callback(r, status, options) {
    if (mapManager.checkGeolocationResult(status)) {
        var data = mapManager.getGeolocationData(r, 0);

        mapManager.setCenter({ mapID: 'map', position: data.geometry.location });

        switch (data.types[0]) {
            case 'street_address':
                mapManager.setZoom({ mapID: 'map', zoom: 16 });
                break;
            case 'postal_code':
                mapManager.setZoom({ mapID: 'map', zoom: 14 });
                break;
            case 'sublocality':
                mapManager.setZoom({ mapID: 'map', zoom: 13 });
                break;
            case 'route':
                mapManager.setZoom({ mapID: 'map', zoom: 15 });
                break;
            case 'locality':
                mapManager.setZoom({ mapID: 'map', zoom: 7 });
                break;
        }

        switch (data.types[0]) {
            case 'street_address':
            case 'route':
                mapManager.addMarker({
                    managerInstance: mapManager,
                    id: 'geoLocatedMarker0', position: data.geometry.location, draggable: true, mapID: 'map',
                    image: MARKERIMAGE_ALERT, center: true, localize: true,
                    goelocalizationCallback:
                        function (response, status) { geoLocationByLatLng_callback(response, status); },
                    dragEnd: function (event) {
                        mapManager.geolocate({ mapID: 'map', position: event.latLng, callback:
                            function (response, status) { geoLocationByLatLng_callback(response, status); }
                        });
                    }
                });
                break;
        }
    }

    currentMap = null;
}

function geoLocationByLatLng_callback(response, status) {
    if (mapManager.checkGeolocationResult(status)) {
        var data = mapManager.getGeolocationData(response, 0);

        completeAddress = data.address_components;

        if ($('#ltCity').html() != null) {
            if (mapManager.getAddressComponent(data.address_components, 'locality').long_name == $('#ltCity').html()) {
                var address = mapManager.getAddressComponent(data.address_components, 'route').long_name;
                if (mapManager.getAddressComponent(data.address_components, 'street_number').long_name != '')
                    address += ', ' + mapManager.getAddressComponent(data.address_components, 'street_number').long_name;

                $('#txtAddress').val(address);

                $('#completeAddress').show();
                $('#completeAddress').html('Indirizzo completo: ' + data.formatted_address);
            }
            else
                alert("L'indirizzo che hai inserito non è a " + $('#ltCity').html());
        }
    }
}

function addSignal_callback(r) {
    hideAjax('#messages');

    if (r.error) {
        writeError(r.error.message, '#messages');
    }
    else if (r.result) {
        var text = 'La segnalazione è stata salvata correttamente.<br/><a href="/' + r.result.city.toLowerCase() + '/' + r.result.signalID + '/segnalazione.aspx">Clicca qui</a> per visualizzare la pagina di dettaglio.';

        writeMessage('Segnalazione salvata correttamente', text, '#messages');
    }
}

function getSignalsNeraby(zipCode) {
    if (!nearbyLoaded) {
        var proxy = new JSONService();
        proxy.getSignalsNearby({ zip: zipCode, signalID: currentMarker.id, ajaxSessionKey: ajaxSessionKey }, getSignalsNearby_callback);
    }
}

function getSignalsNearby_callback(r) {
    if (r.error) {
        alert(r.error.message);
    }
    else if (r.result) {
        if (r.result.length > 0) {
            
            mapOpts.disableDoubleClickZoom = false;
            mapOpts.disableDefaultUI = false;
            mapManager.createMap({ container: 'mapNearby', lat: 42.53, lng: 13.66, googleOptions: mapOpts });

            var bounds = new google.maps.LatLngBounds();

            for (var i = 0; i < r.result.length; i++) {
                var s = r.result[i];
                var signal = s.signal;

                var myLatLng = new google.maps.LatLng(signal.latitude, signal.longitude);

                bounds.extend(myLatLng);

                var image = '';
                if (s.signal.status == 2)
                    image = MARKERIMAGE_OK;
                else
                    image = MARKERIMAGE_ALERT;

                var m = mapManager.addMarker({ id: 'signalMarker' + signal.signalID, position: myLatLng, draggable: false, mapID: 'mapNearby', image: image });
                var w = new google.maps.InfoWindow({ content: s.description, maxWidth: 300 });
                google.maps.event.addListener(m, 'click', function () { w.open(mapManager.getMap('mapNearby').obj, this) });
            }

            mapManager.fitBounds({ mapID: 'mapNearby', bounds: bounds });
        }
        nearbyLoaded = true;
    }
}

function initDetailPage() {
    var image = '';
    
    if (currentMarker.status == 2)
        image = MARKERIMAGE_OK;
    else
        image = MARKERIMAGE_ALERT;

    mapManager.addMarker({ id: 'signalMarker' + currentMarker.id, position: new google.maps.LatLng(currentMarker.lat, currentMarker.lng),
        image: image, center: true, zoom: true, zoomValue: currentMarker.zoom, mapID: 'map'
    });
    fbInit();
    getComments(0);
    $('#lnkPhoto').fancybox();
}