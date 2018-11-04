using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CheckAngelStatus
{
    public class Function
    {
        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public int FunctionHandler(JObject input, ILambdaContext context)
        {
            context.Logger.LogLine($"Beginning to insert record...");

            string id = input["id"].ToString();
            int status = 0;

            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "borrowmyangel.ckwia8qkgyyj.us-east-1.rds.amazonaws.com";
            conn_string.UserID = "b0rrowMyAng3l";
            conn_string.Password = "U8QZW9jU0be1";
            conn_string.Database = "borrowMyAngel";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT angelStatus FROM angels WHERE usrID = ?id";
                    cmd.Parameters.AddWithValue("?id", id);
                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        status = Convert.ToInt32(reader["angelStatus"]);
                    }

                    context.Logger.LogLine($"execution success");

                }

            }
            catch (Exception ex)
            {
                context.Logger.LogLine(ex.Message);

            }
            return status;
        }
    }
}
