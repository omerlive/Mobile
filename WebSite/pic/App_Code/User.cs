using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private int userId;
    private string email;
    private string lname;
    private string imageUrl;
    private string city;
    private int age;
    private int rating=100;
    private string userName;
    private string fname;
    private string userPassword;

    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }
   

    public int Age
    {
        get { return age; }
        set { age = value; }
    }
    

    public int Rating
    {
        get { return rating; }
        set { rating = value; }
    }

    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }
   

    public string Fname
    {
        get { return fname; }
        set { fname = value; }
    }
  
    public string Lname
    {
        get { return lname; }
        set { lname = value; }
    }
   
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    

    public string UserPassword
    {
        get { return userPassword; }
        set { userPassword = value; }
    }
  

    public string City
    {
        get { return city; }
        set { city = value; }
    }
    
    public string ImageUrl
    {
        get { return imageUrl; }
        set { imageUrl = value; }
    }



    public int InsertNewUser()
    {
        DBservices dbs = new DBservices();
        int numAffected = dbs.insert(this);
        return numAffected;
    }

    public DataTable  CheckPass()
    {
        DBservices dbs = new DBservices();
        return dbs.CheckPassword(this);
    }

    public DataTable CheckUserName()
    {
        DBservices dbs = new DBservices();
        return dbs.CheckUserName(this);
    }

    public int InsertToEvent(string eventnum)
    {
        DBservices dbs = new DBservices();
        return dbs.InsertToEvent(this,eventnum);
    }
    
}