
var map;
var circle;

function shippingProviderMap() {
    var initialLocation = { lat: 9.934739, lng: -84.087502 };
    map = new google.maps.Map(document.getElementById('mapSelector'),
        {
            zoom: 8,
            center: initialLocation
        });

    circle = new google.maps.Circle({
        strokeColor: '#000000',
        strokeOpacity: 1,
        strokeWeight: 2,
        fillOpacity: 0.2,
        fillColor: '#FF0000',
        map: map,
        center: initialLocation,
        draggable: true,
        geodesic: true,
        editable: true,
        radius: 50 
    });

    google.maps.event.addListener(circle, 'radius_changed', function () {
        $('#txtAreaRadius').val(circle.getRadius());
        $("#btnChooseCoordinates").css('visibility', 'visible');
    });

    google.maps.event.addListener(circle, 'center_changed', function () {
        $('#txtAreaLatitude').val(circle.getCenter().lat);
        $('#txtAreaLongitude').val(circle.getCenter().lng);
        $("#btnChooseCoordinates").css('visibility', 'visible');
    });

}

function checkIfUserIsInDeliveryArea(userCoordinates) {
    return isInArea(circle.getCenter(), circle.getRadius(), userCoordinates);
}

function isInArea(center, radius, coordinates) {
    var inArea;
    radiuskm = radius / 1000;
    var distance = Math.sqrt(Math.pow((coordinates.lat - center.lat()), 2) + Math.pow((coordinates.lng - center.lng()), 2));
    (distance > radiuskm) ?  inArea = false : inArea = true;

    return inArea;
}

