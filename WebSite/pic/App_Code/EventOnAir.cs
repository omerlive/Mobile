using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for EventOnAir
/// </summary>
public class EventOnAir
{
    public EventOnAir()
    {
        point = new Point();
    }

    private int catedory;
    private int numOfParti;
    private string address;
    private DateTime dateTime;
    private string dateTimeStr;
    private double minAge;
    private double maxAge;
    private int frequency;
    private bool IsPrivate;
    private int adminId;
    private Point point;
    private string comments;
    private string imageUrl;
    private string description;
    private string eventNum;

    public string EventNum
    {
        get { return eventNum; }
        set { eventNum = value; }
    }


    //private double lat;
    //private double lng;


    //prop
    #region
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string ImageUrl
    {
        get { return imageUrl; }
        set { imageUrl = value; }

    }
    public Point Point
    {
        get { return point; }
        set { point = value; }
    }
    public int Catedory
    {
        get { return catedory; }
        set { catedory = value; }
    }
    public int NumOfParti
    {
        get { return numOfParti; }
        set { numOfParti = value; }
    }
    public string Address
    {
        get { return address; }
        set { address = value; }
    }
    public DateTime DateTime
    {
        get { return dateTime; }
        set { dateTime = value; }
    }
    public string DateTimeStr
    {
        get { return dateTimeStr; }
        set { dateTimeStr = value; }
    }
    public double MinAge
    {
        get { return minAge; }
        set { minAge = value; }
    }
    public double MaxAge
    {
        get { return maxAge; }
        set { maxAge = value; }
    }
    public bool IsPrivate1
    {
        get { return IsPrivate; }
        set { IsPrivate = value; }
    }
    public int Frequency
    {
        get { return frequency; }
        set { frequency = value; }
    }
    public string Comments
    {
        get { return comments; }
        set { comments = value; }
    }
    public int AdminID
    {
        get { return adminId; }
        set { adminId = value; }
    }
    //public double Lat
    //{
    //    get { return lat; }
    //    set { lat = value; }
    //}
    //public double Lng
    //{
    //    get { return lng; }
    //    set { lng = value; }
    //}
    #endregion //prop


    public EventOnAir ReadFordt(DataTable dtEvent, int i)
    {

        this.Point = new Point(double.Parse(dtEvent.Rows[i]["Lat"].ToString()), double.Parse(dtEvent.Rows[i]["Lng"].ToString()));
        this.Address = dtEvent.Rows[i]["Address"].ToString();
        this.MaxAge = int.Parse(dtEvent.Rows[i]["MaxAge"].ToString());

        return this;
    }

    //insert envent
    public int insert()
    {
        DBservices dbs = new DBservices();
        int numAffected = dbs.insert(this);
        return numAffected;
    }

    //read the event Table
    public DataTable readTable()
    {
        DBservices dbs = new DBservices();
        dbs = dbs.ReadFromDataBase("bgroup14_test1ConnectionString", "EventsOnAir");

        return dbs.dt;

    }


    //read the UserInEventTable
    public DataTable ReadUserInEvent(string eventNum)
    {
        DBservices dbs = new DBservices();
        dbs = dbs.ReadUserInEvent("bgroup14_test1ConnectionString", eventNum);

        return dbs.dt;
    }



}