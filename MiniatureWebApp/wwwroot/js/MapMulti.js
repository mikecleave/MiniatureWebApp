//https://developers.google.com/maps/documentation/javascript/overview#Inline

let map;

function initMap() {
    //alert("running fuction"); 
    
    // *** THIS ONLY WORKS IF THERE ARE NO WHITE SPACES IN ANY OF MY FIELDS ***

    let stringJSONPowerStations = $('#map').attr('jsonString');
    let powerStations = JSON.parse(stringJSONPowerStations);
    console.log(powerStations.length); // length = 10816 That makes no sense...???
    console.log(powerStations);

    let count = 0;
    for (let p in powerStations) {
        console.log(p);
        let latitude = parseFloat(powerStations[p].Latitude);
        let longitude = parseFloat(powerStations[p].Longitude);
        let locationName = String(powerStations[p].Name);
        //console.log("latitude: " + latitude);
        //console.log("longitude: " + longitude);
        console.log("locationName = " + locationName);

        let latLong = { lat: latitude, lng: longitude };

        console.log("latLong = " + JSON.stringify(latLong));

        if (count == 0) {
            count = count + 1;

            console.log("Setting Map Center");
            //The map, centered at location
            const mapCenter = { lat: 47.67883443924906, lng: - 81.35067753295823 };

            map = new google.maps.Map(document.getElementById("map"), {zoom: 6, center: mapCenter});
        }

        //https://developers.google.com/maps/documentation/javascript/markers
        // The marker, positioned at location
        let marker = new google.maps.Marker({ position: latLong, map: map, title: locationName });        
        //marker.setMap(map);
    }
}

window.initMap = initMap;