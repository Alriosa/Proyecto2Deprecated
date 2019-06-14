
function reloadMarker() {
    initialLocation = { lat: tempData.Data.Latitude, lng: tempData.Data.Longitude };

    // Initial map configuration 
    var options = {
        zoom: 8,
        center: initialLocation
    };

    // Creates new map
    var newMap = new google.maps.Map(document.getElementById('mapSelector'), options);

    // Marker on map
    var marker = new google.maps.Marker({
        position: initialLocation,
        map: newMap,
        animation: google.maps.Animation.DROP
    });

    // Event listener that grabs latitude and longitude coordinates
    google.maps.event.addListener(marker, 'dragend', function () {
        mapLat = marker.getPosition().lat();
        mapLng = marker.getPosition().lng();
        coordinates = "Latitud: " + mapLat + "  |  Longitud: " + mapLng;
        document.querySelector('#txtLatitude').value = mapLat;
        document.querySelector('#txtLongitude').value = mapLng;
        //document.querySelector('#txtWarehouseCoordinates').value = coordinates;
        document.querySelector('#lblChosenCoordinates').innerHTML = coordinates;
        //document.getElementById('btnChooseCoordinates').style.visibility = "visible";
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
        animation: google.maps.Animation.DROP
    });

    // Event listener that grabs latitude and longitude coordinates
    google.maps.event.addListener(marker, 'dragend', function () {
        mapLat = marker.getPosition().lat();
        mapLng = marker.getPosition().lng();
        coordinates = "Latitud: " + mapLat + "  |  Longitud: " + mapLng;
        document.querySelector('#txtLatitude').value = mapLat;
        document.querySelector('#txLongitude').value = mapLng;
    });

}
