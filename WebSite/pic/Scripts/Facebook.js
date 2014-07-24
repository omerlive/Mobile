
// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);
    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
        //testAPI(); 

        getUserInfo();
        //Login();
    } else if (response.status === 'not_authorized') {
        // The person is logged into Facebook, but not your app.
        document.getElementById('status').innerHTML = 'Please log ' +
        'into this app.';

    } else {
        // The person is not logged into Facebook, so we're not sure if
        // they are logged into this app or not.
        document.getElementById('status').innerHTML = 'Please log ' +
        'into Facebook.';
        //alert(3333);
        LogOut();

    }
    // Login();

}

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}



window.fbAsyncInit = function () {
    FB.init({
        appId: '295645690612011',
        cookie: true,  // enable cookies to allow the server to access 
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.0' // use version 2.0
    });

    // Now that we've initialized the JavaScript SDK, we call 
    // FB.getLoginStatus().  This function gets the state of the
    // person visiting this page and can return one of three states to
    // the callback you provide.  They can be:
    //
    // 1. Logged into your app ('connected')
    // 2. Logged into Facebook, but not your app ('not_authorized')
    // 3. Not logged into Facebook and can't tell if they are logged into
    //    your app or not.
    //
    // These three cases are handled in the callback function.

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
} (document, 'script', 'facebook-jssdk'));

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function testAPI() {
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me', function (response) {
        console.log('Successful login for: ' + response.name);
        document.getElementById('status').innerHTML =
        'Thanks for logging in, ' + response.name + '!';
    });
}

// Login Button
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&appId=295645690612011&version=v2.0";
    fjs.parentNode.insertBefore(js, fjs);
} (document, 'script', 'facebook-jssdk'));

/*--------------NIR-----------------*/

function Login() {

    FB.login(function (response) {
        if (response.authResponse) {
            getUserInfo();
        }
        else {
            console.log('User cancelled login or did not fully authorize.');

        }
    },
        { scope: 'email,user_photos,user_videos' });
}

function getUserInfo() {

    FB.api('/me', function (response) {
        document.getElementById('status').innerHTML = 'Hi, ' + response.first_name + '!';
        firstName = response.first_name;
        lastName = response.last_name;
        age = 27;
        city = response.hometown_location;
        userName = firstName + " " + lastName;
        email = response.email;
        password = response.id;

        FB.api('/me/picture?type=normal', function (response) {
            if (response.data.url == undefined) {
                imageUrl = 'pic/EmptyProfile.jpg';

            }
            else {
                imageUrl = response.data.url;
            }

            //alert(imageUrl);

            AddUser(firstName, lastName, age, city, userName, email, password, imageUrl);
        });

    });
}

function AddUser(firstName, lastName, age, city, userName, email, Password, imageUrl) {

    var dataString = '{UserName:"' + userName + '",' + 'Password:"' + Password + '",' + 'FirstName:"' + firstName + '",' + 'LastName:"' + lastName + '",' + 'Age:' + age + ',' + 'City:"' + city + '",' + 'Email:"' + email + '",' + 'imageUrl:"' + imageUrl + '"}';

    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/bgroup14/prod/WebService.asmx/Adduser', // server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            ans = $.parseJSON(data.d);
            if (ans == 1) {

            }
            else {
                //alert("Register faild"); ;
            }

        }, // end of success
        error: function (e) {
            alert("error in jason");
        } // end of error
    }) // end of ajax call
}

function LogOut() {

    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/bgroup14/prod/WebService.asmx/LogOut', // server side method
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from serverside
        {
            //     window.location.assign("Default.aspx");

        }, // end of success
        error: function (e) {
            alert("error in jason");
        } // end of error
    }) // end of ajax call
}