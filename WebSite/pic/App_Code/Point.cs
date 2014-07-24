using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Point
/// </summary>
public class Point
{
    private double lat;
    private double lng;


    public Point()
    {

    }

    public Point(double lat, double lng)
    {
        this.Lat = lat;
        this.Lng = lng;

    }

    public double Lat
    {
        get { return lat; }
        set { lat = value; }
    }
    public double Lng
    {
        get { return lng; }
        set { lng = value; }
    }
        
}