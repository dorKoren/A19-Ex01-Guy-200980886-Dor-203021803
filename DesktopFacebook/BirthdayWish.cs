using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;


namespace DesktopFacebook
{
    public class BirthdayWish
    {
        private static readonly int sr_NumOfDays = 365;
        public BirthdayNode[] BirthdayFriends { get; set; }

        public BirthdayWish()
        {
            BirthdayFriends = new BirthdayNode[sr_NumOfDays];
            initBirthdays();
        }

        private void initBirthdays()
        {
            DateTime today = new DateTime(2018, 1, 1); // DOR Need to check for a more ellegant way

            for(int i = 0; i < sr_NumOfDays; i++)
            {
                BirthdayFriends[i] = new BirthdayNode(today);
                today.AddDays(1);
            }

            //foreach(BirthdayNode currentDay in BirthdayFriends)
            //{
            //    currentDay.Date = today;
            //    today.AddDays(1);
            //}
        }

        public void FillBirthdays(User i_LoggedInUser)
        {
            foreach (User friend in i_LoggedInUser.Friends)
            {
                DateTime birthday = DateTime.Parse(friend.Birthday);
                BirthdayFriends[birthday.DayOfYear].BirthdayFriends.Add(friend);
            }
        }        
    }
}
