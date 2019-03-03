using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;

public partial class Admin_AdminPanel : System.Web.UI.Page
{
    string connString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRoles();
            BindUsers();
        }
    }
    
    protected void btncreate_Click(object sender, EventArgs e)
    {
        createRoles();
        BindRoles();
    }

    public void createRoles()
    {
        try
        {
            if (!Roles.RoleExists(txtrolename.Text))
            {
                Roles.CreateRole(txtrolename.Text);
                BindUsers();
                BindRoles();
                Label1.Text = "Role(s) Created Successfully";
            }
            else
            {
                Label1.Text = "Role(s) Already Exists";
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    public void BindRoles()
    {
        SqlDataAdapter da = new SqlDataAdapter("select RoleName from aspnet_Roles", TAS_Helper.getConnectionString());
        DataSet ds = new DataSet();
        da.Fill(ds, "Roles");
        lstRoles.DataSource = ds;
        lstRoles.DataTextField = "RoleName";
        lstRoles.DataValueField = "RoleName";
        lstRoles.DataBind();
    }

    public void BindUsers()
    {
        SqlDataAdapter da = new SqlDataAdapter("select UserName from aspnet_users", TAS_Helper.getConnectionString());
        DataSet ds = new DataSet();
        da.Fill(ds, "Roles");
        lstusers.DataSource = ds;
        lstusers.DataTextField = "UserName";
        lstRoles.DataValueField = "UserName";
        lstusers.DataBind();
    }

    private void AssignRoles()
    {
        try
        {
            if (!Roles.IsUserInRole(lstRoles.SelectedItem.Text))
            {
                Roles.AddUserToRole(lstusers.SelectedItem.Text,
                lstRoles.SelectedItem.Text);
                BindUsers();
                BindRoles();
                Label1.Text = "User Assigned To Role Successfully";
            }
            else
            {
                Label1.Text = "Role(s) Already Assigned To User";
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    private void RemoveuserFromRole()
    {
        try
        {
            if (Roles.IsUserInRole(lstRoles.SelectedItem.Text))
            {
                Roles.RemoveUserFromRole(lstusers.SelectedItem.Text, lstRoles.SelectedItem.Text);
                BindUsers();
                BindRoles();
                Label1.Text = "User Is Removed From The Role Successfully";
            }
            else
            {
                Label1.Text = "User is not in the specified Role. ";
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    public void RemoveRole()
    {
        try
        {
            Roles.DeleteRole(lstRoles.SelectedItem.Text);
            BindUsers();
            BindRoles();
            Label1.Text = "Role(s) Removed Successfully";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
   
    protected void btnassign_Click(object sender, EventArgs e)
    {
        AssignRoles();
    }
    
    protected void btnremove_Click(object sender, EventArgs e)
    {
        RemoveuserFromRole();
    }
    
    protected void btndelete_Click(object sender, EventArgs e)
    {
        RemoveRole();
        BindRoles();
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        FileUpload1.SaveAs(Server.MapPath(@"..\Uploads\" + FileUpload1.FileName));
        ImportData();
    }
     
    private void ImportData()
    {
        switch (Path.GetExtension(FileUpload1.FileName))
        {
            case ".xls":
                connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0", Server.MapPath(@"..\Uploads\Towers.xls"));
                break;
            case ".xlsx":
                connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", Server.MapPath(@"..\Uploads\Towers.xlsx"));
                break;   
        }
                OleDbConnection oledbConn = new OleDbConnection(connString);
                SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
                try
                {
                    DeleteData();
                    oledbConn.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Towers$]", oledbConn);
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.AcceptChangesDuringFill = false;
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SqlCommand sqlcmd = new SqlCommand("TAS_Towers_Insertdata", cnn);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@SiteId", dt.Rows[i]["SiteId"]);
                        sqlcmd.Parameters.AddWithValue("@District", dt.Rows[i]["District"]);
                        sqlcmd.Parameters.AddWithValue("@Cell", dt.Rows[i]["Cell"]);
                        sqlcmd.Parameters.AddWithValue("@Ckt", dt.Rows[i]["Ckt"]);
                        sqlcmd.Parameters.AddWithValue("@EqptatAEnd", dt.Rows[i]["EqptatAEnd"]);
                        sqlcmd.Parameters.AddWithValue("@Port_A", dt.Rows[i]["Port_A"]);
                        sqlcmd.Parameters.AddWithValue("@EqptatbEnd", dt.Rows[i]["EqptatbEnd"]);
                        sqlcmd.Parameters.AddWithValue("@Port_B", dt.Rows[i]["Port_B"]);
                        sqlcmd.Parameters.AddWithValue("@POP", dt.Rows[i]["POP"]);
                        sqlcmd.Parameters.AddWithValue("@Connectivity", dt.Rows[i]["Connectivity"]);
                        sqlcmd.Parameters.AddWithValue("@EngineerName", dt.Rows[i]["EngineerName"]);
                        sqlcmd.Parameters.AddWithValue("@RBL2", dt.Rows[i]["RBL2"]);
                        cnn.Open();
                        sqlcmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                    Label1.Text = "DataBase Updated Succesfully";
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
    }

    private void DeleteData()
    {
        SqlConnection cnn = new SqlConnection(TAS_Helper.getConnectionString());
        cnn.Open();
        SqlCommand cmd = new SqlCommand("truncatedata", cnn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cnn.Close();
    }
    
    protected void btndeleteuser_Click(object sender, EventArgs e)
    {
        try
        {
            Membership.DeleteUser(lstusers.SelectedItem.Text);
            BindUsers();
            Label1.Text = "User Removed Sucessfully";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
}