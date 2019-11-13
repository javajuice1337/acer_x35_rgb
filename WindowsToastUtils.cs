using System;
using System.Collections.Generic;
using System.Threading;
using Windows.Foundation;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;

namespace acer_rgb
{
    public static class WindowsToastUtils
    {
        public static bool RequestAccess(out bool denied)
        {
            bool ret = false;
            denied = false;

            // Get the listener
            UserNotificationListener listener = UserNotificationListener.Current;

            // And request access to the user's notifications (must be called from UI thread)
            var t = listener.RequestAccessAsync();

            UserNotificationListenerAccessStatus accessStatus;
            try
            {
                while (t.Status != AsyncStatus.Completed) Thread.Sleep(10);
                accessStatus = t.GetResults();
            }
            finally
            {
                t.Close();
            }

            switch (accessStatus)
            {
                // This means the user has granted access.
                case UserNotificationListenerAccessStatus.Allowed:

                    // Yay! Proceed as normal
                    ret = true;
                    break;

                // This means the user has denied access.
                // Any further calls to RequestAccessAsync will instantly
                // return Denied. The user must go to the Windows settings
                // and manually allow access.
                case UserNotificationListenerAccessStatus.Denied:

                    // Show UI explaining that listener features will not
                    // work until user allows access.
                    denied = true;
                    break;

                // This means the user closed the prompt without
                // selecting either allow or deny. Further calls to
                // RequestAccessAsync will show the dialog again.
                case UserNotificationListenerAccessStatus.Unspecified:

                    // Show UI that allows the user to bring up the prompt again
                    break;
            }

            return ret;
        }

        public static int GetToastCount()
        {
            int ret = 0;

            try
            {
                // Get the listener
                UserNotificationListener listener = UserNotificationListener.Current;

                var n = listener.GetNotificationsAsync(NotificationKinds.Toast);
                IReadOnlyList<UserNotification> l;

                try
                {
                    while (n.Status != AsyncStatus.Completed) Thread.Sleep(10);
                    l = n.GetResults();

                    ret = l.Count;
                }
                finally
                {
                    n.Close();
                }
            }
            catch { }

            return ret;
        }
    }
}
