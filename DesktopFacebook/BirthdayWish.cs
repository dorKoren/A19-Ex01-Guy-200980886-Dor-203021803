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
using System.Globalization;

namespace DesktopFacebook
{
    public class BirthdayWish
    {


        public class BirthdayNode
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


        private static readonly int sr_NumOfDays = 365;
        internal BirthdayNode[] BirthdayFriends { get; set; }

        internal BirthdayWish()
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
                DateTime tommorow = today.AddDays(1);
                today = tommorow;
            }
        }

        public void FillBirthdays(User i_LoggedInUser)
        {
            foreach (User friend in i_LoggedInUser.Friends)
            {
                DateTime birthday = DateTime.ParseExact(
                    friend.Birthday, "d", new CultureInfo("En-us"));

                BirthdayFriends[birthday.DayOfYear - 1].BirthdayFriends.Add(friend);
            }
        }

        public User FindUserInCurrentDay(string i_UserName, int i_DayOfYear)
        {
            User user = null;


            foreach (User friend in BirthdayFriends[i_DayOfYear].BirthdayFriends)
            {
                if (i_UserName.Equals(user.Name + user.LastName))
                {
                    user = friend;
                    break;
                }            
            }
            return user;
        }
    }
}
