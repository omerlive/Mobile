using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Text;


/// <summary>
/// Summary description for DBservices
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;
    public string conectionStr = "bgroup14_test1ConnectionString";

    public DBservices()
    {
    }
    public SqlConnection connect(String conString)
    {
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //insert event to DB
    public int insert(EventOnAir p)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect(conectionStr);
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        String cStr = BuildInsertCommand(p);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //build insert command for event
    private String BuildInsertCommand(EventOnAir p)
    {
        String command;
        int isprivate = 0;
        string dateStr = " ";
        StringBuilder sb = new StringBuilder();
        if (p.IsPrivate1)
            isprivate = 1;

        dateStr += p.DateTime.Month.ToString() + "/" + p.DateTime.Day.ToString() + "/" + p.DateTime.Year.ToString() + " " + p.DateTime.Hour.ToString() + ":" + p.DateTime.Minute.ToString() + "0:00";


        sb.AppendFormat("Values({0}, {1} ,{2}, {3},'{4}',{5},{6},'{7}',{8},'{9}','{10}','{11}')", p.NumOfParti, p.Catedory, p.Frequency, isprivate, dateStr, p.MinAge, p.MaxAge, p.Comments, p.AdminID, p.Address, p.Point.Lat, p.Point.Lng);
        String prefix = "INSERT INTO EventsOnAir " + "( NumOfParticipants, CategoryId, FrequencyId, [Private],[Time],MinAge,MaxAge,Comments,AdminId,Address,Lat,Lng)";
        command = prefix + sb.ToString();

        return command;
    }


    //insert user to useres Table
    public int insert(User u)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect(conectionStr);
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        String cStr = BuildInsertCommand(u);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //bulid insert comand for NewUser to Users table
    private String BuildInsertCommand(User u)//build insert command for User
    {
        String command;
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}','{4}',{5},'{6}','{7}',{8})", u.UserName, u.Fname, u.Lname, u.Email, u.UserPassword, u.Age, u.City, u.ImageUrl, u.Rating);
        String prefix = "INSERT INTO Users " + "(UserName, Fname, Lname, Email, UserPassword,[Age],[City],[Picture],[Rating])";

        command = prefix + sb.ToString();

        return command;
    }


    //insert user to Event
    public int InsertToEvent(User u, string eventNum)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect(conectionStr);
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        String cStr = BuildInsertCommand(u, eventNum);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //bulid insert commad for user to event
    private String BuildInsertCommand(User u, string eventNum)//build insert command for User
    {
        String command;
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Values({0},'{1}')", eventNum, u.Email);
        String prefix = "INSERT INTO UsersInEvent " + "(EventNumber,Email )";

        command = prefix + sb.ToString();

        return command;
    }

    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = CommandSTR;
        cmd.CommandTimeout = 10;
        cmd.CommandType = System.Data.CommandType.Text;
        return cmd;
    }

    //check password
    public DataTable CheckPassword(User u)
    {
        SqlConnection con;
        con = connect("bgroup14_test1ConnectionString");
        DataSet tblpassword = new DataSet();
        SqlDataAdapter adpt1;

        SqlCommand MySPCommand = new SqlCommand("GetUserPassword", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
        parEmail.Value = (u.Email);
        parEmail.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEmail);

        adpt1 = new SqlDataAdapter(MySPCommand);

        adpt1.Fill(tblpassword, "T1");
        con.Close();
        return tblpassword.Tables["T1"];

    }
    //check admin name
    public DataTable CheckUserName(User u)
    {
        SqlConnection con;
        con = connect(conectionStr);
        DataSet tblGetAdminName = new DataSet();
        SqlDataAdapter adpt1;

        SqlCommand MySPCommand = new SqlCommand("GetAdminName", con);
        MySPCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter parEmail = new SqlParameter("@AdminId", SqlDbType.Int);
        parEmail.Value = (u.UserId);
        parEmail.Direction = ParameterDirection.Input;
        MySPCommand.Parameters.Add(parEmail);

        adpt1 = new SqlDataAdapter(MySPCommand);

        adpt1.Fill(tblGetAdminName, "T2");
        con.Close();
        return tblGetAdminName.Tables["T2"];

    }

    // Read from the DB into a table (event)
    public DBservices ReadFromDataBase(string conString, string tableName)
    {

        DBservices dbS = new DBservices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT  [imageUrl], [Description], [NumOfParticipants], [Time], [Frequncy],[Address],[MinAge], [MaxAge],[EventNumber], [Comments],[Private],[AdminId] ,[Lat] , [Lng]FROM [View_EventsOnAir]"; // create the select that will be used by the adapter to select data from the DB

            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command
            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;


            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    // Read from the DB into a table UserInEVent
    public DBservices ReadUserInEvent(string conString, string eventNm)
    {

        DBservices dbS = new DBservices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT  [UserName] FROM [View_UserInEvent] WHERE EventNumber =" + eventNm; // create the select that will be used by the adapter to select data from the DB

            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command
            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;


            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



}
