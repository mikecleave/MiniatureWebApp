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
///REMEMBER TO PRESS SHIFT F5 TO RELOAD MY JavaScript.
function powerStationFilter() {
    console.log(JSON.stringify(this));
    PSId = this.value
    console.log("Power Station Filter: " + PSId);

}