using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Context.User.IsInRole("Admin"))
        {
            Menu1.Items[1].Text = ""; Menu1.Items[2].Text = "Admin Control Panel"; Menu1.Items[3].Text = "Manage Towers";
        }
        else if (this.Context.User.IsInRole("User"))
        {
            Menu1.Items[1].Text = "TowersInfo";
        }        
        else
        {
            Menu1.Items[1].Text = ""; Menu1.Items[2].Text = ""; Menu1.Items[3].Text = "";
        }
    }
}
