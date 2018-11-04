using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace BorrowMyAngel
{
    public class Temp
    {
        public Temp()
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
                    data = new NotifBody
                    {
                        body = "new_chat_avalible"
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
            request.title = header;
            request.body = content;
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
    }

    public class AngelRequest
{
    [DataMember(Name = "clientToken")]
    public string clientToken { get; set; }

    [DataMember(Name = "data")]
    public string chatID { get; set; }
}
}
