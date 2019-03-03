using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for TAS_Helper
/// </summary>
public static class TAS_Helper
{
    public static string getConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["TAS_connectionstring"].ConnectionString;
    }
}