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
        //console.log("latitude: " + latitude);
        //console.log("longitude: " + longitude);

        let latLong = { lat: latitude, lng: longitude };

        console.log("latLong = " + latLong);

        if (count == 0) {
            count = count + 1;

            console.log("Setting Map Center");
            //The map, centered at location
            const mapCenter = { lat: 43.869972, lng: -78.724945 };
            const map = new google.maps.Map(document.getElementById("map"), {
                zoom: 4,
                center: mapCenter,
            });
        }

        // The marker, positioned at location
        let marker = new google.maps.Marker({
            position: latLong,
            map: map
        });        
        //marker.setMap(map);

    }
}

window.initMap = initMap;