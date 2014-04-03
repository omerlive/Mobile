using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.IO;
using System.Data.SqlClient;
using System.Data;



/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]


public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getEvents()
    {
        EventOnAir ev = new EventOnAir();
        List<EventOnAir> eventsList = new List<EventOnAir>();
        DataTable dt = ev.readTable();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            EventOnAir evTemp = new EventOnAir();
            evTemp.Point = new Point(double.Parse(dt.Rows[i]["Lat"].ToString()), double.Parse(dt.Rows[i]["Lng"].ToString()));
            evTemp.Address = dt.Rows[i]["Address"].ToString();
            evTemp.MaxAge = int.Parse(dt.Rows[i]["MaxAge"].ToString());
            evTemp.MinAge = int.Parse(dt.Rows[i]["MinAge"].ToString());
            evTemp.NumOfParti = int.Parse(dt.Rows[i]["NumOfParticipants"].ToString());
            evTemp.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
            evTemp.AdminID = int.Parse(dt.Rows[0]["AdminId"].ToString());
            evTemp.IsPrivate1 = bool.Parse(dt.Rows[0]["Private"].ToString());
            evTemp.DateTime = DateTime.Parse(dt.Rows[i]["Time"].ToString());
            evTemp.DateTimeStr = (dt.Rows[i]["Time"].ToString());
            evTemp.Description = dt.Rows[i]["Description"].ToString();
            evTemp.Comments = dt.Rows[i]["Comments"].ToString();
            evTemp.EventNum = dt.Rows[i]["EventNumber"].ToString();


            //add the  event to the list
            eventsList.Add(evTemp);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(eventsList);
        return jsonString;
    }


    //mobile
    //add event
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public string setPOI(double lat, double lng, int nop, int category, string type, int frequecy, int minAge, int maxAge, string address, string time, string comments)
    {
        EventOnAir ev = new EventOnAir();
        ev.Point = new Point(lat, lng);
        ev.Address = address;
        ev.MaxAge = maxAge;
        ev.MinAge = minAge;
        ev.NumOfParti = nop;
        ev.Catedory = category;
        ev.IsPrivate1 = bool.Parse(type.ToString());
        ev.DateTimeStr = time;
        ev.Comments = comments;
        ev.Frequency = frequecy;
        //ev.AdminID = int.Parse(dt.Rows[0]["AdminId"].ToString());

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize("ok");
        try
        {
            ev.insert();
            jsonString = js.Serialize("ok");
        }
        catch (Exception ex)
        {
            jsonString = js.Serialize("error in treasure.setPOI --- " + ex.Message);
        }

        return jsonString;
    }

    //add user
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public int Adduser(string UserName, string Password, string FirstName, string LastName, int Age, string City, string Email, string imageUrl)
    {
        User U1 = new User();
        U1.UserName = UserName;
        U1.UserPassword = Password;
        U1.Fname = FirstName;
        U1.Lname = LastName;
        U1.Age = Age;
        U1.City = City;
        U1.Email = Email;
        U1.ImageUrl = "Images\\" + imageUrl;
      
        int numEfect = U1.InsertNewUser();
        return numEfect;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public string Login(string Email, string Password)
    {
        User u = new User();
        u.UserPassword = Password;
        u.Email = Email;
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize("Worng Email or Password ");
        try
        {
            DataTable dt = u.CheckPass();
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["UserPassword"].ToString() == u.UserPassword)
                { jsonString = js.Serialize("ok"); }   
            }
           
          
        }
        catch (Exception ex)
        {
            jsonString = js.Serialize("error in treasure.Login --- " + ex.Message);
        }
        return jsonString;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public string UserToEvent(string Email, string EventNum)
    {  
        
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(" faild");
        try
        {
            User U1 = new User();
            U1.Email = Email;
            int num = U1.InsertToEvent(EventNum);
            if (num>0)
            {
                 jsonString = js.Serialize("Success");
            }
            else
            {
                jsonString = js.Serialize("Event is full!!!");
            }
        }
        catch (Exception ex)
        {
            jsonString = js.Serialize("error in treasure.Login --- " + ex.Message);
        }

        return jsonString;
 
    }













}
