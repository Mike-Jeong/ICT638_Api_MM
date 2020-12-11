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
            _hubConfiguration.ConnectionString = "Endpoint=sb://ict638assessmentmma.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=TzSoKNYQRlZbxtsARKUg7atfw+EwmDgQ9ohmCx5biq8=";
            _hubConfiguration.HubName = "ICT638GroupMMA";
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
