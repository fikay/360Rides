function initMap() {
    // Initialize the Google Places service
    var autocompleteService = new google.maps.places.AutocompleteService();
    var placesService = new google.maps.places.PlacesService(document.createElement('div'));

    // Get the input field
    var input = document.getElementById('autocomplete');

    const options = {
        componentRestrictions: {country : "ca"}
    }
    // Set up the autocomplete object
    var autocomplete = new google.maps.places.Autocomplete(input, options);

    // Set the types of predictions to be returned (e.g., 'geocode', 'establishment')
    autocomplete.setTypes(['address']);
  /*  autocomplete1.setTypes(['address']);*/

    // Listen for the event when a place is selected
    autocomplete.addListener('place_changed', function () {
        var place = autocomplete.getPlace();

        $('#autocomplete').val(place.name);
        $('#city').val((place.address_components[2].long_name));
        $('#state').val((place.address_components[4].long_name));
        $('#postal').val((place.address_components[6].long_name));



        console.log(place);
        console.log(place.address_components[2].long_name);
        
        /* Handle the selected place (you can access various properties of the place object)*/
        console.log('Place Name:', place.name);
        console.log('Place Address:', place.formatted_address);
        console.log('Place Latitude:', place.geometry.location.lat());
        console.log('Place Longitude:', place.geometry.location.lng());
        long = place.geometry.location.lng();
        lat = place.geometry.location.lat()
    });
    //autocomplete1.addListener('place_changed', function () {
    //    var place1 = autocomplete1.getPlace();
    //    //console.log('Input 1 - Place Name:', place1.name);
    //    //console.log('Input 1 - Place Address:', place1.formatted_address);
    //    //console.log('Input 1 - Place Latitude:', place1.geometry.location.lat());
    //    //console.log('Input 1 - Place Longitude:', place1.geometry.location.lng());
    //    long1 = place1.geometry.location.lng();
    //    lat1 = place1.geometry.location.lat()
    //});

}