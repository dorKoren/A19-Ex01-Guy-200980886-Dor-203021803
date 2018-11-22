using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;
using System.Globalization;

namespace DesktopFacebook
{
    public class BirthdayDictionary
    {
        internal class BirthdayNode
        {
            internal DateTime   Date            { get; set; }
            internal List<User> BirthdayFriends { get; set; }

            internal BirthdayNode(DateTime i_Date)
            {
                int year  = i_Date.Year;
                int month = i_Date.Month;
                int day   = i_Date.Day;

                Date = new DateTime(year, month, day);
                BirthdayFriends = new List<User>();
            }
        }

        internal void FillBirthdays(User i_LoggedInUser)
        {
            foreach (User friend in i_LoggedInUser.Friends)
            {
                DateTime birthday = DateTime.ParseExact(
                    friend.Birthday, "d", new CultureInfo("En-us"));

                BirthdayFriends[birthday.DayOfYear - 1].BirthdayFriends.Add(friend);
            }
        }

        private static readonly int sr_NumOfDays = 365;
        internal BirthdayNode[] BirthdayFriends { get; set; }

        public BirthdayDictionary()
        {
            BirthdayFriends = new BirthdayNode[sr_NumOfDays];
            initBirthdays();
        }

        private void initBirthdays()
        {
            DateTime today = new DateTime(2018, 1, 1); 

            for (int i = 0; i < sr_NumOfDays; i++)
            {
                BirthdayFriends[i] = new BirthdayNode(today);

                DateTime tomorrow = today.AddDays(1);
                today = tomorrow;
            }
        }    
    }
}
