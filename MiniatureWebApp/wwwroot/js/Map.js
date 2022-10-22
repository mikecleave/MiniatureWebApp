// Initialize and add the map
function initMap() {
    // The location of Uluru
    var latitude = parseFloat($('#map').attr('latitude'));
    var longitude = parseFloat($('#map').attr('longitude'));
    
    console.log("latitude: " + latitude);
    console.log("longitude: " + longitude);

    const uluru = { lat: latitude, lng: longitude };    
    //const uluru = { lat: 43.869972, lng: -78.724945 };

    console.log(uluru);

    // The map, centered at Uluru
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10,
        center: uluru,
    });
    // The marker, positioned at Uluru
    const marker = new google.maps.Marker({
        position: uluru,
        map: map,
    });
}

window.initMap = initMap;