var mapLat = 0.0;
var mapLng = 0.0;
var coordinates;

function reloadMarker() {
    initialLocation = { lat: tempData.WarehouseLatitude, lng: tempData.WarehouseLongitude };

    // Initial map configuration 
    var options = {
        zoom: 8,
        center: initialLocation
    }

    // Creates new map
    var newMap = new google.maps.Map(document.getElementById('mapSelector'), options);

    // Marker on map
    var marker = new google.maps.Marker({
        position: initialLocation,
        map: newMap,
        draggable: false,
        animation: google.maps.Animation.DROP
    });

    // Event listener that grabs latitude and longitude coordinates
    google.maps.event.addListener(marker, 'dragend', function () {
        mapLat = marker.getPosition().lat();
        mapLng = marker.getPosition().lng();
        coordinates = "Latitud: " + mapLat + "  |  Longitud: " + mapLng;
        document.querySelector('#txtWarehouseLatitude').value = mapLat;
        document.querySelector('#txtWarehouseLongitude').value = mapLng;
        document.querySelector('#txtWarehouseCoordinates').value = coordinates;
        document.querySelector('#lblChosenCoordinates').innerHTML = coordinates;
        document.getElementById('btnChooseCoordinates').style.visibility = "visible";
    });
}

function initMap() {

    // Initial position
    initialLocation = { lat: 9.934739, lng: -84.087502 };

    // Initial map configuration 
    var options = {
        zoom: 8,
        center: initialLocation
    }

    // Creates new map
    var newMap = new google.maps.Map(document.getElementById('mapSelector'), options);

    // Marker on map
    var marker = new google.maps.Marker({
        position: initialLocation,
        map: newMap,
        draggable: false,
        animation: google.maps.Animation.DROP
    });

    // Event listener that grabs latitude and longitude coordinates
    google.maps.event.addListener(marker, 'dragend', function () {
        mapLat = marker.getPosition().lat();
        mapLng = marker.getPosition().lng();
        coordinates = "Latitud: " + mapLat + "  |  Longitud: " + mapLng;
        document.querySelector('#txtWarehouseLatitude').value = mapLat;
        document.querySelector('#txtWarehouseLongitude').value = mapLng;
        document.querySelector('#txtWarehouseCoordinates').value = coordinates;
        document.querySelector('#lblChosenCoordinates').innerHTML = coordinates;
        document.getElementById('btnChooseCoordinates').style.visibility = "visible";
    });

}
