//function initialize() {

//    var input = document.getElementById('searchTextField');
//    var autocomplete = new google.maps.places.Autocomplete(input);
//}

//google.maps.event.addDomListener(window, 'load', initialize);


function initialize() {

    var input = document.getElementById('cityTb');
    var autocomplete = new google.maps.places.Autocomplete(input);

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        infowindow.close();
        marker.setVisible(false);
        input.className = '';
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            // Inform the user that the place was not found and return.
            input.className = 'notfound';
            return;
        }

        var address = '';
        if (place.address_components) {
            address = [
          (place.address_components[0] && place.address_components[0].short_name || ''),
          (place.address_components[1] && place.address_components[1].short_name || ''),
          (place.address_components[2] && place.address_components[2].short_name || '')
        ].join(' ');
        }

        infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);

    });

    setupClickListener('changetype-geocode', ['geocode']);
}
google.maps.event.addDomListener(window, 'load', initialize);
