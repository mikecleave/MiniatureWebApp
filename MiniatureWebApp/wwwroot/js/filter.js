let sortOrder;

///REMEMBER TO PRESS SHIFT F5 TO RELOAD MY JavaScript.
function PowerStationFilter() {
    powerStationNameFilter = this.value;
    window.sessionStorage.setItem('powerStationNameFilter', powerStationNameFilter);
    BuildURL();
}

function InspectorNameFilter() {
    inspectorNameFilter = this.value
    window.sessionStorage.setItem('inspectorNameFilter', inspectorNameFilter);
    BuildURL();
}

function StatusFilter() {
    statusFilter = this.value
    window.sessionStorage.setItem('statusFilter', statusFilter);
    BuildURL();
}

function DateSort() {
    console.log("TESTING");
    
    sortOrder = sessionStorage.getItem('sortOrder', sortOrder);
    console.log(sortOrder);

    if (sortOrder == null) {
        sortOrder = "name_desc";
    }
    else if (sortOrder == "name_desc" || sortOrder == "") {
        sortOrder = "date_desc";
    }
    else if (sortOrder == "date_desc") {
        sortOrder = "Date";
    }
    else if (sortOrder == "Date") {
        sortOrder = "date_desc";
    }
    console.log("TEST2");
    window.sessionStorage.setItem('sortOrder', sortOrder);
    console.log("BEFORE BUILD URL");
    BuildURL();
}

function PowerStationSort() {
    sortOrder = sessionStorage.getItem('sortOrder', sortOrder);
    console.log(sortOrder);

    if (sortOrder == null || sortOrder == "" || sortOrder == "Date" || sortOrder == "date_desc") {
        sortOrder = "name_desc";
    }
    else if (sortOrder == "name_desc") {
        sortOrder = "";
    }
    window.sessionStorage.setItem('sortOrder', sortOrder);
    console.log("BEFORE BUILD URL");
    BuildURL();
}

function BuildURL() {
    console.log("START OF BUILD URL");
    let sortOrder = sessionStorage.getItem('sortOrder');
    let powerStationNameFilter = sessionStorage.getItem('powerStationNameFilter');
    let inspectorNameFilter = sessionStorage.getItem('inspectorNameFilter');
    let statusFilter = sessionStorage.getItem('statusFilter');
    let baseUrl = "https://" + window.location.host + "/Inspections?";
    let fullURL = baseUrl;

    if (powerStationNameFilter != null) {
        let powerStationNameFilterURL = "powerStationNameFilter=" + sessionStorage.getItem('powerStationNameFilter');
        fullURL = fullURL + "&" + powerStationNameFilterURL;
    }
    if (inspectorNameFilter != null) {
        let inspectorNameFilterURL = "inspectorNameFilter=" + sessionStorage.getItem('inspectorNameFilter');
        fullURL = fullURL + "&" + inspectorNameFilterURL;
    }
    if (statusFilter != null) {
        let statusFilterURL = "statusFilter=" + sessionStorage.getItem('statusFilter');
        fullURL = fullURL + "&" + statusFilterURL;
    }
    if (sortOrder != null) {
        let sortOrderURL = "sortOrder=" + sessionStorage.getItem('sortOrder');
        fullURL = fullURL + "&" + sortOrderURL;
    }

    console.log("END OF BUILD URL");
    window.sessionStorage.setItem('fullURL', fullURL);
    window.location.assign(fullURL);
}