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
            FB.api('/me', function(response) {
	            var str="<b>Name</b> : "+response.name+"<br>";
	  	        str +="<b>Link: </b>"+response.link+"<br>";
	  	        str +="<b>Username:</b> "+response.username+"<br>";
	  	        str +="<b>round(id*random): </b>"+ Math.round(parseInt( response.id) * Math.random()) +"<br>";
	  	        //str +="<b>Email:</b> "+response.email+"<br>";
                str +="<b>Email:</b> "+"stam@zzz.com"+"<br>";
	  	        //document.getElementById("status").innerHTML=str;
            });
            //$('#btnPhoto').css('display', 'block');
            getPhoto();
            $('#Login').css('display', 'none');
            $('#btnLogout').css('display', 'block');
        }

	    function getPhoto()
	    {
	        FB.api('/me/picture?type=square', function(response) {
		        var str="<img src='"+response.data.url+"'/>";
	  	        document.getElementById("status").innerHTML+=str;	  	  	    
            });	
	    }

	    function Logout()
	    {
		    FB.logout(function(){document.location.reload();});
	    }       

        
        //fires before the PG
        $(document).on("pageinit", "#firstPage", function (event) {
           
        });