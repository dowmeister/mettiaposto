var MARKERIMAGE_ALERT = '/images/alert.png';
var MARKERIMAGE_OK = '/images/check.png';

$.mapManager = function () {

    this.maps = new Array();
    this.markers = new Array();

    this.createMap = function (options) {

        var latlng = new google.maps.LatLng(options.lat, options.lng);

        options.googleOptions.mapTypeId = google.maps.MapTypeId.ROADMAP;
        options.googleOptions.center = latlng;

        var m = new google.maps.Map(document.getElementById(options.container), options.googleOptions);

        this.maps.push({ id: options.container, obj: m });

        return this;
    }

    this.createMarker = function (options) {

        this.removeMarkerById(options.id);

        var marker = new google.maps.Marker({
            map: options.map,
            position: options.position,
            draggable: options.draggable,
            icon: new google.maps.MarkerImage(options.image, new google.maps.Size(32, 32))
        });

        this.markers.push({ id: options.id, obj: marker, map: options.map });

        return marker;
    }

    this.addMarker = function (options) {

        options.map = this.getMap(options.mapID).obj;

        var marker = this.createMarker(options);

        if (options.center)
            options.map.setCenter(location);

        if (options.draggable)
            google.maps.event.addListener(marker, 'dragend', function () {
                $.mapManager().geolocate({ position: marker.getPosition(), callback: options.goelocalizationCallback });
            });

        if (options.localize)
            this.geolocate(options);

        return marker;
    }

    this.getMap = function (id) {
        var found;
        $.each(this.maps, function (index, map) {
            if (map.id == id) {
                found = map;
                return false;
            }
        });
        return found;
    }

    this.geolocate = function (options) {
        var m = this.getMap(options.mapID);

        if (m) {
            var geoCoder = new google.maps.Geocoder();
            if (options.address)
                geoCoder.geocode({ addr: options.address }, options.callback);
            else
                geoCoder.geocode({ latLng: options.position }, options.callback);
        }
    }

    this.removeMarkerFromMap = function(marker) {
        marker.setMap(null);
    }

    this.removeMarker = function(index) {
        this.removeMarkerFromMap(markers[index].obj);
        this.markers.splice(index, 1);
    }

    this.removeMarkerById = function(id) {
        for (var i = this.markers.length - 1; i >= 0; i--) {
            if (this.markers[i].id == id) {
                this.removeMarker(i);
                break;
            }
        }
    }

    this.removeAllMarkers = function () {
        for (var i = this.markers.length - 1; i >= 0; i--) {
            this.removeMarker(i);
        }
    }

    this.fitBounds = function (options) {
        var map = this.getMap(options.mapID).obj;
        map.fitBounds(options.bounds);

        if (options.center)
            map.setCenter(options.bounds.getCenter());
    }

    this.normalizeZoom = function (options) {
        var map = this.getMap(options.mapID).obj;

        if (map.getZoom() > options.zoomLimit)
            map.setZoom(options.zoomLimit);
    }

    return this;
}