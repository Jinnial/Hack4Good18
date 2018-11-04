using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using Amazon.Lambda.Core;

using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AngelService
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns the Notification Token of Avalible Angel</returns>
        public string FindAnAngel()
        {
            return "";
        }

        public void UpdateNotifToken(string uID, string token)
        {

        }
    }

    public class NotificationSystem
    {
        public static String authToken = "key=AAAAH_0zj6A:APA91bH4Q2Wf5E6cT8qCeQjYQTurpYQ3Zhqb8ms32nURX7rdyrND_fNQBnNzE5osXUKScXxQgC8YRBwjHtZeO7S06wOYWRHmyv2rtpBsfXbYa59AQ45EYFie3lAh-baCbD3IMS1Qo4V9";

        public static String UpdateChatFeed(string token)
        {
            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, authToken);
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Notification notif = new Notification
                {
                    device = token,
                    data = new NotificationType
                    {
                        type = "new_chat_avalible"
                    }
                };
                string notifStr = JsonConvert.SerializeObject(notif);
                streamWriter.Write(notifStr);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public static void AlertAngel(string clientToken, string chatID)
        {
            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, authToken);
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                NotificationAngel notif = new NotificationAngel
                {
                    device = clientToken,
                    data = new ClientData
                    {
                        client = clientToken,
                        chat = chatID
                    }
                };
                string notifStr = JsonConvert.SerializeObject(notif);
                streamWriter.Write(notifStr);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
        }
    }

    #region Notification Classes
    [DataContract]
    public class Notification
    {
        [DataMember(Name = "to")]
        public string device { get; set; }

        [DataMember(Name = "data")]
        public NotificationType data { get; set; }
    }

    [DataContract]
    public class NotificationType
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
    }

    [DataContract]
    public class NotificationAngel
    {
        [DataMember(Name = "to")]
        public string device { get; set; }

        [DataMember(Name = "data")]
        public ClientData data { get; set; }
    }

    [DataContract]
    public class ClientData
    {
        [DataMember(Name = "client")]
        public string client { get; set; }

        [DataMember(Name = "chat")]
        public string chat { get; set; }
    }

    [DataContract]
    public class AngelRequest
    {
        [DataMember(Name = "clientToken")]
        public string clientToken { get; set; }

        [DataMember(Name = "data")]
        public string chatID { get; set; }
    }
    #endregion
}
