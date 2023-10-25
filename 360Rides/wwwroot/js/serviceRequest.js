var long;
var long1;
var lat;
var lat1;
var previousValue;
var flag = false;


document.addEventListener('DOMContentLoaded', function () {
    var now = new Date();

    // Format the date in "yyyy-MM-dd" format
    var formattedDate = now.toISOString().slice(0, 10);

    // Format the time in "hh:mm" format
    var formattedTime = now.toTimeString().slice(0, 5);

    // Combine date and time in the required format "yyyy-MM-ddThh:mm"
    var formattedDatetime = formattedDate + 'T' + formattedTime;

    // Set the formatted datetime as the value of the datetime-local input
    // document.getElementById('datetimePicker').value = formattedDatetime;
    var inputs = document.querySelectorAll('input[type="datetime-local"]');
    inputs.forEach(function (input) {
        input.value = formattedDate;
    })

});


//$("#ChildrenNumber").on('focusin', function () {
//    previousValue = $(this).val()
//    flag =true
//})


//    $("#ChildrenNumber").on('focusout', function (event) {
//        if (flag == true) {
//            var currentValue = $(this).val();
//            console.log(currentValue)
//            console.log(previousValue)
//            if (currentValue != 0 && previousValue == 0) {
//                console.log("here2")
//                for (let i = 0; i < currentValue; i++) {
//                    var label = $('<label>').text('Child Name');

//                    // Create input element
//                    var newInput = $('<input>').attr('type', 'text').addClass('form-control new-input');

//                    // Append label and input element to the div container
//                    $('#names').append(label);
//                    $('#names').append(newInput);
//                }
//            }
//            else if (currentValue != 0 && previousValue != 0 && currentValue > previousValue) {
//                console.log("here1")
//                var num = currentValue - previousValue
//                for (let i = 0; i < num; i++) {
//                    var label = $('<label>').text(' Child Name');

//                    // Create input element
//                    var newInput = $('<input>').attr('type', 'text').addClass('form-control new-input');

//                    // Append label and input element to the div container
//                    $('#names').append(label);
//                    $('#names').append(newInput);
//                }

//            }
//            else {
//                console.log("here")
//                if (currentValue < previousValue) {
//                    var num = previousValue - currentValue
//                    for (let i = 0; i < num; i++) {
//                        $('.new-input:last').prev('label').remove();
//                        $('.new-input:last').remove();
//                    }

//                }
//            }

//            event.stopPropagation();
//            flag = false;
//        }
       

//    })


function initMap() {
    // Initialize the Google Places service
    var autocompleteService = new google.maps.places.AutocompleteService();
    var placesService = new google.maps.places.PlacesService(document.createElement('div'));

    // Get the input field
    var input = document.getElementById('autocomplete');
    var input1 = document.getElementById('autocomplete1');

    // Set up the autocomplete object
    var autocomplete = new google.maps.places.Autocomplete(input);
    var autocomplete1 = new google.maps.places.Autocomplete(input1);

    // Set the types of predictions to be returned (e.g., 'geocode', 'establishment')
    autocomplete.setTypes(['address']);
    autocomplete1.setTypes(['address']);

    // Listen for the event when a place is selected
    autocomplete.addListener('place_changed', function () {
        var place = autocomplete.getPlace();

        // Handle the selected place (you can access various properties of the place object)
        //console.log('Place Name:', place.name);
        //console.log('Place Address:', place.formatted_address);
        //console.log('Place Latitude:', place.geometry.location.lat());
        //console.log('Place Longitude:', place.geometry.location.lng());
        long = place.geometry.location.lng();
        lat = place.geometry.location.lat()
    });
    autocomplete1.addListener('place_changed', function () {
        var place1 = autocomplete1.getPlace();
        //console.log('Input 1 - Place Name:', place1.name);
        //console.log('Input 1 - Place Address:', place1.formatted_address);
        //console.log('Input 1 - Place Latitude:', place1.geometry.location.lat());
        //console.log('Input 1 - Place Longitude:', place1.geometry.location.lng());
        long1 = place1.geometry.location.lng();
        lat1 = place1.geometry.location.lat()
    });

}

function calculateDistance() {
    
    

    var origin = new google.maps.LatLng(lat, long); // Latitude and longitude of the first place
    var destination = new google.maps.LatLng(lat1,long1); // Latitude and longitude of the second place

    var service = new google.maps.DistanceMatrixService();
    service.getDistanceMatrix({
        origins: [origin],
        destinations: [destination],
        travelMode: 'DRIVING',
        unitSystem: google.maps.UnitSystem.METRIC, // Use METRIC for kilometers, IMPERIAL for miles
        avoidHighways: false,
        avoidTolls: false
    }, function (response, status) {
       
        if (status === 'OK') {
            var distance = response.rows[0].elements[0].distance.value;
            priceCalc(distance)
           /* priceCalc(distance);*/
           /* document.getElementById('price').value =  distance;*/
        } else {
            console.error('Error calculating distance:', status);
        }
    });
}


function priceCalc(distance) {
  
    $.ajax({
        url: `/customer/home/getPrice?distance=${distance}`,
        type: 'GET',
        success: function (data) {
           /* $('#price').prop("disabled", false);*/
            $('#price').val(data.price);
            /*$('#price').prop("disabled", true);*/
        }
    })
}
// Call the function to notify user



