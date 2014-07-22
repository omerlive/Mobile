<!-- These are the notifications that are displayed to the user through pop-ups if the above JS files does not exist in the same directory-->
        if ((typeof cordova == 'undefined') && (typeof Cordova == 'undefined')) alert('Cordova variable does not exist. Check that you have included cordova.js correctly');
        if (typeof CDV == 'undefined') alert('CDV variable does not exist. Check that you have included cdv-plugin-fb-connect.js correctly');
        if (typeof FB == 'undefined') alert('FB variable does not exist. Check that you have included the Facebook JS SDK file.');
           
//        FB.Event.subscribe('auth.login', function(response) {
//            alert('auth.login event');
//        });
//            
//        FB.Event.subscribe('auth.logout', function(response) {
//            alert('auth.logout event');
//        });
//            
//        FB.Event.subscribe('auth.sessionChange', function(response) {
//            alert('auth.sessionChange event');
//        });
//            
//        FB.Event.subscribe('auth.statusChange', function(response) {
//            alert('auth.statusChange event');
//        });
        
        // Wait for device API libraries to load
        document.addEventListener("deviceready", onDeviceReady, false);

        // device APIs are available        
        function onDeviceReady() {
            try {
                FB.init({ nativeInterface: CDV.FB, useCachedDialogs: false });
                //document.getElementById('data').innerHTML = "";
            } catch (e) {
                alert(e);
            }

            $('#Login').on('click', function () {
                Login();
            });
        }

        function Login()
	    {
		    FB.login(
                function(response) 
                {
		            if (response.authResponse) 
		            {
		    	        getUserInfo();
  			        }
                    else 
  			        {
  	    	         console.log('User cancelled login or did not fully authorize.');
   			        }
		        },
                {scope: 'email,user_photos,user_videos'}
            );	
	    }

        function getUserInfo() {

            FB.api('/me', function (response) {
                //document.getElementById('status').innerHTML = 'Hi, ' + response.first_name + '!';
                firstName = response.first_name;
                lastName = response.last_name;
                age = 27;
                city = response.hometown_location;
                userName = firstName + " " + lastName;
                email = response.email;
                password = response.id;
                imageUrl = 'Images/EmptyProfile.jpg';
                
//                alert(firstName, lastName, age, city, userName, email, password, imageUrl);
//                AddUser(firstName, lastName, age, city, userName, email, password, imageUrl);
                
                getPhoto();
                    $('#Login').css('display', 'none');
                    $('#btnLogout').css('display', 'block');

                AddUser(firstName, lastName, age, city, userName, email, password, imageUrl);

            });
        }


	    function getPhoto()
	    {
	        FB.api('/me/picture?type=square', function (response) {
                if (response.data.url == undefined) {
                    imageUrl = 'Images/EmptyProfile.jpg';
                    document.getElementById("status").innerHTML+= "<img src='"+imageUrl+"'/>";
                }
                else {
                    imageUrl = response.data.url;
                    var str="<img src='"+response.data.url+"'/>";
	  	            document.getElementById("status").innerHTML+=str;
                }
            });
	    }

	    function Logout()
	    {
                $.ajax({ // ajax call starts
                url: 'http://proj.ruppin.ac.il/bgroup14/prod/tar6/WebService.asmx/LogOut', // server side method
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

		    FB.logout(function(){document.location.reload();});
	    }       

        
        //fires before the PG
        $(document).on("pageinit", "#firstPage", function (event) {
           
        });


        function AddUser(firstName, lastName, age, city, userName, email, Password, imageUrl) {
        alert('AddUser');
        var dataString = '{UserName:"' + userName + '",' + 'Password:"' + Password + '",' + 'FirstName:"' + firstName + '",' + 'LastName:"' + lastName + '",' + 'Age:' + age + ',' + 'City:"' + city + '",' + 'Email:"' + email + '",' + 'imageUrl:"' + imageUrl + '"}';

        $.ajax({ // ajax call starts
            url: 'http://proj.ruppin.ac.il/bgroup14/prod/tar6/WebService.asmx/AdduserMobile', // server side method
            data: dataString,    // the parameters sent to the server
            type: 'POST',
            dataType: 'json', // Choosing a JSON datatype
            contentType: 'application/json; charset = utf-8',
              success: function (data) // Variable data contains the data we get from server side
            {    alert("success");
                user = $.parseJSON(data.d);

                if (user.Fname != undefined) {

                    if (typeof (Storage) !== "undefined") {
                        sessionStorage.Email = user.Email;
                        sessionStorage.Fname = user.Fname;
                        sessionStorage.ImageUrl = user.ImageUrl;
                        sessionStorage.adminId = user.UserId;
                    }
                    window.location.assign("index.html");
                }
                else {
                    document.getElementById("errorLbl").innerText = "Wrong email or password";
                }
                // run on all the POIs and display them

 
            }, // end of success
            error: function (e) {
             alert("failed in getTarget add user :( " + e.responseText);
//                alert("error in jason" + e);
            } // end of error
        }) // end of ajax call


        function LogInJson() {

        email = document.getElementById("emailTb").value;
        password = document.getElementById("passwordTb").value;

        var dataString = '{Email:"' + email + '",' + 'Password:"' + password + '"}';

        $.ajax({ // ajax call starts
            url: 'http://proj.ruppin.ac.il/bgroup14/prod/tar6/WebService.asmx/LoginMobile',   // server side method
            data: dataString,    // parameters passed to the server
            type: 'POST',
            dataType: 'json', // Choosing a JSON datatype
            contentType: 'application/json; charset = utf-8',
            success: function (data) // Variable data contains the data we get from server side
            {
                user = $.parseJSON(data.d);

                if (user.Fname != undefined) {

                    if (typeof (Storage) !== "undefined") {
                        sessionStorage.Email = user.Email;
                        sessionStorage.Fname = user.Fname;
                        sessionStorage.ImageUrl = user.ImageUrl;
                        sessionStorage.adminId = user.UserId;
                    }
                    window.location.assign("index.html");
                }
                else {
                    document.getElementById("errorLbl").innerText = "Wrong email or password";
                }
                // run on all the POIs and display them

            }, // end of success
            error: function (e) {
                alert("failed in getTarget :( " + e.responseText);
            } // end of error
        }) // end of ajax call
    }
}