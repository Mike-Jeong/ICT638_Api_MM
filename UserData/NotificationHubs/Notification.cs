using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserData.Configurations;
using Microsoft.Azure.NotificationHubs;

namespace UserData.NotificationHubs
{
    public class Notification { 
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }

        public static async void sendNotification(string t, string c)
        {

            NotificationHubConfiguration _hubConfiguration = new NotificationHubConfiguration();
            _hubConfiguration.ConnectionString = "Endpoint=sb://ict638mindec2020.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=925tJgnq/Zq7WdSsXCXqGF85xffgqs6s/iO5s7M8VOo=";
            _hubConfiguration.HubName = "ICT638Min2020";
            NotificationHubProxy notificationHubProxy = new NotificationHubProxy(_hubConfiguration);
            Notification notification = new Notification();
            notification.Content = c;
            notification.Title = t;
            HubResponse<NotificationOutcome> hubResponse = await notificationHubProxy.SendNotification(notification);

            if (hubResponse.CompletedWithSuccess)
            {
                Console.WriteLine("Push Message successful: " + notification.Title + " " + notification.Content);
            }
        }
    }
}
