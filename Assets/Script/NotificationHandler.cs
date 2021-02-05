using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
    public void Awake()
    {
        BuildNotifChannel();
        BuildSimpleNotifChannel();
    }

    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        sendRepeatingNotif();
    }
    public void BuildNotifChannel()
    {
        string channel_id = "notifChannel_id_repeat";

        string title = "Repeating";

        Importance importance = Importance.Default;

        string description = "Reapeating notif";

        var channel = new AndroidNotificationChannel(channel_id, title,description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void BuildSimpleNotifChannel()
    {
        string channel_id = "notifChannel_id_simple";

        string title = "Default";

        Importance importance = Importance.Default;

        string description = "Simple notif";

        var channel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void sendRepeatingNotif()
    {
        string title = "Repeating Notif";
        string text = "the galaxy is in danger, we need help captain";
        DateTime fireTime = DateTime.Now.AddSeconds(10);
        TimeSpan interval = new TimeSpan(12,0,0);

        var notif = new AndroidNotification(title, text, fireTime, interval);
        AndroidNotificationCenter.SendNotification(notif, "notifChannel_id_repeat");
    }

    public void sendSimpleNotif()
    {
        string title = "Simple Notif";
        string text = "the galaxy is in danger, we need help captain";
        DateTime fireTime = DateTime.Now.AddSeconds(10);

        var notif = new AndroidNotification(title, text, fireTime);
        AndroidNotificationCenter.SendNotification(notif, "notifChannel_id_simple");
    }
}
