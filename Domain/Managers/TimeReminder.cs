using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Managers
{
    public class TimeReminder
    {
        static Timer timer;
        long interval = 60000;
        static object synclock = new object();
        public bool notif { get; set; }
        public TimeReminder()
        {
            notif = true;

        }
        public void Init(DateTime data1)
        {
            timer = new Timer(new TimerCallback(satae => ToastNotificationSend(satae, data1)), null, 0, interval);
        }
        public void TurnOn()
        {
            timer.Change(0, interval);
            notif = true;
        }
        public void TurnOff()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            notif = false;
        }

        private void ToastNotificationSend(object obj, DateTime date1)
        {
            DateTime dd = DateTime.Now;
            if (dd.Hour == date1.Hour-1 && dd.Minute == date1.Minute)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("There is an hour left before the appointment with the doctor!");
                Console.ForegroundColor = ConsoleColor.White;
                notif = true;
            }

        }

    }
}