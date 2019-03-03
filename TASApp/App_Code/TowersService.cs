using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for TowersService
/// </summary>

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]

public class TowersService : WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        
        return "Hello World";
    }

    [WebMethod]
    public List<Towers> getData()
    {
        List<Towers> TASTowers = new List<Towers>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_Retrivedata", cnn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.id = Convert.ToInt32(dr["Id"]);
                towers.SiteId = dr["SiteId"].ToString();
                towers.District = dr["District"].ToString();
                towers.Cell = dr["Cell"].ToString();
                towers.Ckt = dr["Ckt"].ToString();
                towers.EqptatAEnd = dr["EqptatAEnd"].ToString();
                towers.Port_A = dr["Port_A"].ToString();
                towers.EqptatbEnd = dr["EqptatbEnd"].ToString();
                towers.Port_B = dr["Port_B"].ToString();
                towers.POP = dr["POP"].ToString();
                towers.Connectivity = dr["Connectivity"].ToString();
                towers.EngineerName = dr["EngineerName"].ToString();
                towers.RBL2 = dr["RBL2"].ToString();
                //towers.PagingData = new TowersPagingData(){pagesize=10 };
                TASTowers.Add(towers);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return TASTowers;
    }

    [WebMethod]
    public int updateData(string idvalue, string fieldname, string fieldvalue)
    {
        int id = 0;
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("update TAS_Towers set " + fieldname + " ='" + fieldvalue + "' where Id =" + idvalue + "", cnn);
            cnn.Open();
            id = cmd.ExecuteNonQuery();
            cnn.Close();
        }
        catch (Exception fex)
        {
            throw new Exception(fex.Message);
        }
        return id;
    }

    [WebMethod]
    public int deleteData(string idvalue)
    {
        int id = Convert.ToInt32(idvalue);
        int val = 0;
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_Deletedata", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", id));
            cnn.Open();
            val = cmd.ExecuteNonQuery();
            cnn.Close();
        }
        catch (Exception fex)
        {
            throw new Exception(fex.Message);
        }
        return val;
    }

    [WebMethod]
    public int updateDatabyId(string data)
    {
        string[] dataarray = data.Split(';');
        int retunvalue = 0;

        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_Editdata", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", dataarray[0]));
            cmd.Parameters.Add(new SqlParameter("@SiteId", dataarray[1]));
            cmd.Parameters.Add(new SqlParameter("@District", dataarray[2]));
            cmd.Parameters.Add(new SqlParameter("@Cell", dataarray[3]));
            cmd.Parameters.Add(new SqlParameter("@Ckt", dataarray[4]));
            cmd.Parameters.Add(new SqlParameter("@EqptatAEnd", dataarray[5]));
            cmd.Parameters.Add(new SqlParameter("@Port_A", dataarray[6]));
            cmd.Parameters.Add(new SqlParameter("@EqptatbEnd", dataarray[7]));
            cmd.Parameters.Add(new SqlParameter("@Port_B", dataarray[8]));
            cmd.Parameters.Add(new SqlParameter("@POP", dataarray[9]));
            cmd.Parameters.Add(new SqlParameter("@Connectivity", dataarray[10]));
            cmd.Parameters.Add(new SqlParameter("@EngineerName", dataarray[11]));
            cmd.Parameters.Add(new SqlParameter("@RBL2", dataarray[12]));
            cnn.Open();
            retunvalue = cmd.ExecuteNonQuery();
            cnn.Close();
        }
        catch (Exception fex)
        {
            throw new Exception(fex.Message);
        }
        return retunvalue;
    }

    [WebMethod]
    public List<Towers> getDatabyId(string id)
    {
        List<Towers> TASTowers = new List<Towers>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_Retrivedatabyid", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", Convert.ToInt32(id)));
            cnn.Open();
            cmd.ExecuteNonQuery();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.id = Convert.ToInt32(dr["Id"]);
                towers.SiteId = dr["SiteId"].ToString();
                towers.District = dr["District"].ToString();
                towers.Cell = dr["Cell"].ToString();
                towers.Ckt = dr["Ckt"].ToString();
                towers.EqptatAEnd = dr["EqptatAEnd"].ToString();
                towers.Port_A = dr["Port_A"].ToString();
                towers.EqptatbEnd = dr["EqptatbEnd"].ToString();
                towers.Port_B = dr["Port_B"].ToString();
                towers.POP = dr["POP"].ToString();
                towers.Connectivity = dr["Connectivity"].ToString();
                towers.EngineerName = dr["EngineerName"].ToString();
                towers.RBL2 = dr["RBL2"].ToString();
                //towers.PagingData = new TowersPagingData(){pagesize=10 };
                TASTowers.Add(towers);
            }
            cnn.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return TASTowers;
    }

    [WebMethod]
    public List<Towers> getDatabySiteId(string SiteId) 
    {
        List<Towers> TASTowers = new List<Towers>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_RetrivedatabySiteid", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
            cnn.Open();
            cmd.ExecuteNonQuery();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.id = Convert.ToInt32(dr["Id"]);
                towers.SiteId = dr["SiteId"].ToString();
                towers.District = dr["District"].ToString();
                towers.Cell = dr["Cell"].ToString();
                towers.Ckt = dr["Ckt"].ToString();
                towers.EqptatAEnd = dr["EqptatAEnd"].ToString();
                towers.Port_A = dr["Port_A"].ToString();
                towers.EqptatbEnd = dr["EqptatbEnd"].ToString();
                towers.Port_B = dr["Port_B"].ToString();
                towers.POP = dr["POP"].ToString();
                towers.Connectivity = dr["Connectivity"].ToString();
                towers.EngineerName = dr["EngineerName"].ToString();
                towers.RBL2 = dr["RBL2"].ToString();
                TASTowers.Add(towers);
            }
            cnn.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return TASTowers;
    }

    [WebMethod]
    public List<Towers> getDatabyPOP(string POP)
    {
        List<Towers> TASTowers = new List<Towers>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_RetrivedatabyPOP", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@POP", POP));
            cnn.Open();
            cmd.ExecuteNonQuery();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.id = Convert.ToInt32(dr["Id"]);
                towers.SiteId = dr["SiteId"].ToString();
                towers.District = dr["District"].ToString();
                towers.Cell = dr["Cell"].ToString();
                towers.Ckt = dr["Ckt"].ToString();
                towers.EqptatAEnd = dr["EqptatAEnd"].ToString();
                towers.Port_A = dr["Port_A"].ToString();
                towers.EqptatbEnd = dr["EqptatbEnd"].ToString();
                towers.Port_B = dr["Port_B"].ToString();
                towers.POP = dr["POP"].ToString();
                towers.Connectivity = dr["Connectivity"].ToString();
                towers.EngineerName = dr["EngineerName"].ToString();
                towers.RBL2 = dr["RBL2"].ToString();
                TASTowers.Add(towers);
            }
            cnn.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return TASTowers;
    }

    [WebMethod]
    public List<Towers> getDatabydistrict(string district)
    {
        List<Towers> TASTowers = new List<Towers>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_Retrivedatabydistrict", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@district", district));
            cnn.Open();
            cmd.ExecuteNonQuery();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.id = Convert.ToInt32(dr["Id"]);
                towers.SiteId = dr["SiteId"].ToString();
                towers.District = dr["District"].ToString();
                towers.Cell = dr["Cell"].ToString();
                towers.Ckt = dr["Ckt"].ToString();
                towers.EqptatAEnd = dr["EqptatAEnd"].ToString();
                towers.Port_A = dr["Port_A"].ToString();
                towers.EqptatbEnd = dr["EqptatbEnd"].ToString();
                towers.Port_B = dr["Port_B"].ToString();
                towers.POP = dr["POP"].ToString();
                towers.Connectivity = dr["Connectivity"].ToString();
                towers.EngineerName = dr["EngineerName"].ToString();
                towers.RBL2 = dr["RBL2"].ToString();
                TASTowers.Add(towers);
            }
            cnn.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return TASTowers;
    }

    [WebMethod]
    public List<Towers> getDatabyengname(string engname)
    {
        List<Towers> TASTowers = new List<Towers>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_Retrivedatabyengname", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@engname", engname));
            cnn.Open();
            cmd.ExecuteNonQuery();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.id = Convert.ToInt32(dr["Id"]);
                towers.SiteId = dr["SiteId"].ToString();
                towers.District = dr["District"].ToString();
                towers.Cell = dr["Cell"].ToString();
                towers.Ckt = dr["Ckt"].ToString();
                towers.EqptatAEnd = dr["EqptatAEnd"].ToString();
                towers.Port_A = dr["Port_A"].ToString();
                towers.EqptatbEnd = dr["EqptatbEnd"].ToString();
                towers.Port_B = dr["Port_B"].ToString();
                towers.POP = dr["POP"].ToString();
                towers.Connectivity = dr["Connectivity"].ToString();
                towers.EngineerName = dr["EngineerName"].ToString();
                towers.RBL2 = dr["RBL2"].ToString();
                TASTowers.Add(towers);
            }
            cnn.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return TASTowers;
    }

    [WebMethod]
    public List<string> getSiteId()
    {
        List<string> siteid = new List<string>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_getSiteId", cnn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.SiteId = dr["SiteId"].ToString();
                siteid.Add(towers.SiteId);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return siteid;
    }

    [WebMethod]
    public List<string> getengineername()
    {
        List<string> engineername = new List<string>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_getengineername", cnn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.EngineerName = dr["EngineerName"].ToString();
                engineername.Add(towers.EngineerName);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return engineername;
    }

    [WebMethod]
    public List<string> getpop()
    {
        List<string> pop = new List<string>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_getpop", cnn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.POP = dr["POP"].ToString();
                pop.Add(towers.POP);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return pop;
    }

    [WebMethod]
    public List<string> getdistrict()
    {
        List<string> district = new List<string>();
        try
        {
            SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
            SqlCommand cmd = new SqlCommand("TAS_Towers_getdistrict", cnn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Towers");
            foreach (DataRow dr in ds.Tables["Towers"].Rows)
            {
                Towers towers = new Towers();
                towers.District = dr["District"].ToString();
                district.Add(towers.District);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return district;
    }

}
