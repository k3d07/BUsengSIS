using System;
using System.Collections.Generic;
using System.Text;
using System;
using MySqlConnector;

namespace StudentInformationSystem.Data
{
    public static class DbHelper
    {
        // Keith: replace with real implementation reading from App.config
        public static MySqlConnection GetConnection()
        {
            throw new NotImplementedException("Keith: implement GetConnection");
        }

        public static bool TestConnection(out string error)
        {
            error = "Not implemented yet";
            return false;
        }
    }
}