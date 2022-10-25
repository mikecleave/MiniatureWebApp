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
    powerStationIdFilter = this.value
    console.log("powerStationIdFilter: " + powerStationIdFilter);
}

function InspectorNameFilter() {
    //console.log(JSON.stringify(this));
    inspectorNameFilter = this.value
    console.log("inspectorNameFilter: " + inspectorNameFilter);
}

function StatusFilter() {
    //console.log(JSON.stringify(this));
    statusFilter = this.value
    console.log("statusFilter: " + statusFilter);
}