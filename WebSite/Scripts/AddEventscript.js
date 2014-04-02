
function AddEvent() {
    alert("AddEvent");
    nop = document.getElementById("nopTb").value;
    category = document.getElementById("categoryDdl").value;
    type = document.getElementById("typeFlip").value;
    frequecy = $('input[name="Frequecy"]:checked').val();
    ageRange = $('#rangeval').text();
    arrtemp = ageRange.split("-");
    minAge = arrtemp[0];
    maxAge = arrtemp[1];
    comments = document.getElementById("commentsTb").value;
    time = document.getElementById("dateTb").value + " " + document.getElementById("timeTb").value;


    if (sessionStorage.AddressSe) {
        address = sessionStorage.AddressSe;
        alert(address);
    }
    else {
        address = "";
    }
    if (sessionStorage.FinalPositonS) {
        postemp = sessionStorage.FinalPositonS;

        postemp = postemp.replace("(", "")
        postemp = postemp.replace(")", "")
        postemp = postemp.split(",");
        lat = postemp[0];
        lng = postemp[1];
        alert(lat);
        alert(lng);
    }
    else {
        lat = 0;
        lng = 0;

    }



    // admin id!?!?!?!!?!?!



    setPOI(lat, lng, nop, category, type, frequecy, minAge, maxAge, address, time, comments);
}
//-----------------------------------------------------------------------
// Send the target point to the server
//-----------------------------------------------------------------------


function setPOI(lat, lng, nop, category, type, frequecy, minAge, maxAge, address, time, comments) {
    var dataString = '{lat:' + lat + ',' + 'lng:' + lng + ',' + 'nop:' + nop + ',' + 'category:' + category + ',' + 'type:"' + type + '",' + 'frequecy:' + frequecy + ',' + 'minAge:' + minAge + ',' + 'maxAge:' + maxAge + ',' + 'address:"' + address + '",' + 'time:"' + time + '",' + 'comments:"' + comments + '"}';

    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/bgroup14/prod/WebService.asmx/setPOI',   // server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            alert("POI set at lat:" + lat + " , lng: " + lng);
        }, // end of success
        error: function (e) {
            alert("failed in setPOI :( " + e.responseText);
        } // end of error
    }) // end of ajax call
}





function Itsworek() {
    alert("work");
}