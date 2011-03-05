var MARKERIMAGE_ALERT = '/images/alert.png';
var MARKERIMAGE_OK = '/images/check.png';

$.mapManager = function ()
{

    this.maps = new Array();
    this.markers = new Array();

    this.createMap = function (options)
    {
        if (!this.getMap(options.container))
        {
            var latlng = new google.maps.LatLng(options.lat, options.lng);

            options.googleOptions.mapTypeId = google.maps.MapTypeId.ROADMAP;
            options.googleOptions.center = latlng;

            var m = new google.maps.Map(document.getElementById(options.container), options.googleOptions);

            this.maps.push({ id: options.container, obj: m });
        }
    }

    this.createMarker = function (options)
    {
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

    this.addMarker = function (options)
    {
        options.map = this.getMap(options.mapID).obj;

        var marker = this.createMarker(options);

        if (options.center)
            options.map.setCenter(options.position);

        if (options.draggable)
            google.maps.event.addListener(marker, 'dragend', options.dragEnd);

        if (options.localize)
            this.geolocate({ mapID: options.mapID, position: options.position, callback: options.goelocalizationCallback });

        if (options.zoom)
            this.setZoom({ mapID: options.mapID, zoom: options.zoomValue });

        return marker;
    }

    this.getMap = function (id)
    {
        var found;
        $.each(this.maps, function (index, map)
        {
            if (map.id == id)
            {
                found = map;
                return false;
            }
        });
        return found;
    }

    this.geolocate = function (options)
    {
        var checkMap = false;
        var m;

        if (options.mapID)
        {
            m = this.getMap(options.mapID);
        }
        else
            checkMap = false;

        if (checkMap)
        {
            if (!m)
                return false;
        }

        var geoCoder = new google.maps.Geocoder();
        if (options.address)
            geoCoder.geocode({ address: options.address, language: 'it', region:'it' }, options.callback);
        else
            geoCoder.geocode({ latLng: options.position, language: 'it', region: 'it' }, options.callback);
    }

    this.removeMarkerFromMap = function (marker)
    {
        marker.setMap(null);
    }

    this.removeMarker = function (index)
    {
        this.removeMarkerFromMap(this.markers[index].obj);
        this.markers.splice(index, 1);
    }

    this.removeMarkerById = function (id)
    {
        for (var i = this.markers.length - 1; i >= 0; i--)
        {
            if (this.markers[i].id == id)
            {
                this.removeMarker(i);
                break;
            }
        }
    }

    this.removeAllMarkers = function ()
    {
        for (var i = this.markers.length - 1; i >= 0; i--)
        {
            this.removeMarker(i);
        }
    }

    this.fitBounds = function (options)
    {
        var map = this.getMap(options.mapID).obj;
        map.fitBounds(options.bounds);

        if (options.center)
            map.setCenter(options.bounds.getCenter());
    }

    this.normalizeZoom = function (options)
    {
        var map = this.getMap(options.mapID).obj;

        if (map.getZoom() > options.zoomLimit)
            map.setZoom(options.zoomLimit);
    }

    this.getMarker = function (id)
    {
        var found;
        $.each(this.markers, function (index, marker)
        {
            if (marker.id == id)
            {
                found = marker;
                return false;
            }
        });
        return found;
    }

    this.getZoom = function (id)
    {
        return this.getMap(id).obj.getZoom();
    }

    this.setZoom = function (options)
    {
        this.getMap(options.mapID).obj.setZoom(options.zoom);
    }

    this.setCenter = function (options)
    {
        this.getMap(options.mapID).obj.setCenter(options.position);
    }

    this.checkGeolocationResult = function (status)
    {
        if (status == google.maps.GeocoderStatus.OK)
            return true;
        else
            return false;
    }

    this.getGeolocationData = function (r, index)
    {
        return data = r[index];
    }

    this.getAddressComponent = function (components, type)
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

    this.hasGeolocation = function ()
    {
        return navigator.geolocation;
    }

    return this;
}