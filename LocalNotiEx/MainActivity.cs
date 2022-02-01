using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using System;
using static AndroidX.Core.App.NotificationCompat;

namespace LocalNotiEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        private const int pendingIntentId = 1;

        Button _normalNotification;
        Button _actionNotification;
        Button pendingIntentotification;
        Button bigTextNotification;
        Button bigPictureNotification;
        Button inboxNotification;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            UIReferences();
            UIClickEvents();
            CreateNotificationChannel();

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            { 
                return; 
            
            }

            string channelName = Resources.GetString(Resource.String.channel_name);
            string channelDescription = Resources.GetString(Resource.String.channel_description);
            NotificationChannel notificationChannel = new NotificationChannel(Resources.GetString(Resource.String.channel_id), channelName, NotificationImportance.Default);
            notificationChannel.Description = channelDescription;
            NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(notificationChannel);
        }

        private void UIClickEvents()
        {
            _normalNotification.Click += _normalNotification_Click;
            _actionNotification.Click += _actionNotification_Click;
            pendingIntentotification.Click += PendingIntentotification_Click;
            bigTextNotification.Click += BigTextNotification_Click;
            bigPictureNotification.Click += BigPictureNotification_Click;
            inboxNotification.Click += InboxNotification_Click;
        }

        private void InboxNotification_Click(object sender, EventArgs e)
        {
            InboxStyle inboxStyle = new InboxStyle();

            inboxStyle.AddLine(cs: "Ross: Care");
            inboxStyle.AddLine(cs: "Monica: Cook");
            inboxStyle.AddLine(cs: "Rachel: Waiteress");
            inboxStyle.AddLine(cs: "Chandler: Miss Chandelour Bong");
            inboxStyle.AddLine(cs: "Joey: Loves Pizza");
            inboxStyle.AddLine(cs: "Pheobe: Loves Singing");
            inboxStyle.SetSummaryText(cs: "+2 more");

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, Resources.GetString(Resource.String.channel_id))
                .SetContentTitle("5 new messages")
                .SetContentText("friends@xamarin.com")
                .SetSmallIcon(Resource.Drawable.ic_launcher_notifi_foreground)
                .SetDefaults((int)NotificationDefaults.Vibrate)
                .SetStyle(inboxStyle);

            Notification notification = builder.Build();

            NotificationManager notificationManager = GetSystemService(NotificationService) as NotificationManager;

            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);



        }

        private void BigPictureNotification_Click(object sender, EventArgs e)
        {
            BigPictureStyle pictureStyle = new BigPictureStyle();

            pictureStyle.BigPicture(BitmapFactory.DecodeResource(Resources, Resource.Drawable.monica));

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, Resources.GetString(Resource.String.channel_id))
                .SetContentTitle("Big Picture Style Notification")
                .SetContentText("This is a big picture style notification")
                .SetSmallIcon(Resource.Drawable.ic_launcher_notifi_foreground)
                .SetDefaults((int)NotificationDefaults.Vibrate)
                .SetStyle(pictureStyle);

            Notification notification = builder.Build();

            NotificationManager notificationManager = GetSystemService(NotificationService) as NotificationManager;

            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);

        }

        private void BigTextNotification_Click(object sender, EventArgs e)
        {
            BigTextStyle textStyle = new BigTextStyle();

            string longTextMessage = "Monica E. Geller is a fictional character, one of the six main characters who appears on the American sitcom Friends (1994–2004). " + 
                "Created by David Crane and Marta Kauffman, and portrayed by actress Courteney Cox, Monica appears in all of the show's 236 episodes, from its premiere in 1994, to its finale in 2004"+
                "A chef known for her cleanliness, competitiveness and obsessive-compulsive nature, Monica is the younger sister of Ross Geller and best friend of Rachel Green, the latter of whom she invites to live with her after Rachel forsakes her own wedding"+
                "The two characters spend several years living together as roommates until Monica begins a romantic relationship with long-time neighbor and friend Chandler Bing, whom she marries. Unable to conceive children on their own, Chandler and Monica eventually adopt twins Erica and Jack and move out of their apartment into a larger house in the suburbs."+
                "The creators' first choice for the role of Monica was comedienne Janeane Garofalo."+
                " Cox had originally been offered the role of Rachel but declined in favor of playing the character's best friend Monica because she was drawn to her strong personality."+
                " Meanwhile, the role of Rachel went to actress Jennifer Aniston.";

            textStyle.BigText(longTextMessage);
            textStyle.SetSummaryText(cs: "This is a summary");

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, Resources.GetString(Resource.String.channel_id))
                .SetContentTitle("Monica E. Geller is a fictional character")
                .SetContentText("This is a big text style notification")
                .SetSmallIcon(Resource.Drawable.ic_launcher_notifi_foreground)
                .SetDefaults((int)NotificationDefaults.Vibrate)
                .SetStyle(textStyle);

            Notification notification = builder.Build();

            NotificationManager notificationManager = GetSystemService(NotificationService) as NotificationManager;

            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }

        private void PendingIntentotification_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(SecondActivity));
            i.PutExtra(name:"key", value:"Hello World");
            PendingIntent pendingintent = PendingIntent.GetActivity(this, pendingIntentId, i, PendingIntentFlags.OneShot);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, Resources.GetString(Resource.String.channel_id))
                .SetContentTitle("Action Notification")
                .SetContentText("This is an action notification")
                .SetSmallIcon(Resource.Drawable.ic_launcher_notifi_foreground)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources,Resource.Drawable.chandler))
                .SetContentIntent(pendingintent)
                .SetAutoCancel(true)
                .SetDefaults((int)NotificationDefaults.All);

            Notification notification = builder.Build();

            NotificationManager notificationManager = GetSystemService(NotificationService) as NotificationManager;

            const int notificationId = 1;
            notificationManager.Notify(notificationId, notification);
        }

        private void _actionNotification_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(SecondActivity));
            PendingIntent pendingintent = PendingIntent.GetActivity(this, pendingIntentId, i, PendingIntentFlags.OneShot);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, Resources.GetString(Resource.String.channel_id))
                .SetContentTitle("Action Notification")
                .SetContentText("This is an action notification")
                .SetSmallIcon(Resource.Drawable.ic_launcher_notifi_foreground)
                .SetContentIntent(pendingintent)
                .SetDefaults((int)NotificationDefaults.All);

            Notification notification = builder.Build();

            NotificationManager notificationManager = GetSystemService(NotificationService) as NotificationManager;

            const int notificationId = 1;
            notificationManager.Notify(notificationId, notification);


        }

        private void _normalNotification_Click(object sender, EventArgs e)
        {
           
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, Resources.GetString(Resource.String.channel_id));
            builder.SetContentTitle("Sample Notification");
            builder.SetContentText("This is normal Notification");
            builder.SetSmallIcon(Resource.Drawable.ic_launcher_notifi_foreground);
            builder.SetDefaults((int)NotificationDefaults.Vibrate);

            Notification notification = builder.Build();

            NotificationManager notificationManager = GetSystemService(NotificationService) as NotificationManager;

            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }

        private void UIReferences()
        {
            _normalNotification = FindViewById<Button>(Resource.Id.button1);
            _actionNotification = FindViewById<Button>(Resource.Id.button2);
            pendingIntentotification = FindViewById<Button>(Resource.Id.button3);
            bigTextNotification = FindViewById<Button>(Resource.Id.button4);
            bigPictureNotification = FindViewById<Button>(Resource.Id.button5);
            inboxNotification = FindViewById<Button>(Resource.Id.button6);
        }
    }
}