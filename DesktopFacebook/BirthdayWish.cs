using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;


namespace DesktopFacebook
{
    internal class BirthdayWish
    {
        private static readonly int sr_NumOfDays = 365;
        private BirthdayNode[] BirthdayFriends { get; set; }

        internal BirthdayWish()
        {
            BirthdayFriends = new BirthdayNode[sr_NumOfDays];
            initBirthdays();
        }

        private void initBirthdays()
        {
            DateTime today = new DateTime(2018, 1, 1);                  // DOR Need to check for a more ellegant way
            
            foreach(BirthdayNode currentDay in BirthdayFriends)
            {
                currentDay.Date = today;
                today.AddDays(1);
            }
        }

        private void FillBirthdays(User i_LoggedInUser)
        {
            foreach (User friend in i_LoggedInUser.Friends)
            {
                DateTime birthday = DateTime.Parse(friend.Birthday);
                BirthdayFriends[birthday.DayOfYear].BirthdayFriends.Add(friend);
            }
        }        
    }
}
