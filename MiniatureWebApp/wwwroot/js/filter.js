///REMEMBER TO PRESS SHIFT F5 TO RELOAD MY JavaScript.

/*
function changeListener() {
    var value = this.value;
    console.log(value);

    if (value == "white") {
        document.body.style.background = "white";
    }
    else if (value == "red") {
        document.body.style.background = "red";
    }
    else if (value == "blue") {
        document.body.style.background = "blue";
    }
}
*/


///REMEMBER TO PRESS SHIFT F5 TO RELOAD MY JavaScript.
function PowerStationFilter() {
    //console.log(JSON.stringify(this));
    powerStationNameFilter = this.value;
    //@powerStationIdFilter = this.value;
    
    console.log("powerStationNameFilter: " + powerStationNameFilter);

    let baseUrl = "https://" + window.location.host;
    let filterUrl = "/Inspections?" + "powerStationNameFilter=" + powerStationNameFilter;

    console.log(baseUrl + filterUrl);
    window.location.assign(baseUrl + filterUrl);
}

function InspectorNameFilter() {
    //console.log(JSON.stringify(this));
    inspectorNameFilter = this.value
    console.log("inspectorNameFilter: " + inspectorNameFilter);

    let baseUrl = "https://" + window.location.host;
    let filterUrl = "/Inspections?" + "inspectorNameFilter=" + inspectorNameFilter;

    console.log(baseUrl + filterUrl);
    window.location.assign(baseUrl + filterUrl);
}

function StatusFilter() {
    //console.log(JSON.stringify(this));
    statusFilter = this.value
    console.log("statusFilter: " + statusFilter);

    let baseUrl = "https://" + window.location.host;
    let filterUrl = "/Inspections?" + "statusFilter=" + statusFilter;

    console.log(baseUrl + filterUrl);
    window.location.assign(baseUrl + filterUrl);
}