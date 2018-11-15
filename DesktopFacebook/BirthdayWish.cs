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
        private static readonly int sr_NumOfDays = 365;
        public BirthdayNode[] BirthdayFriends { get; set; }

        public BirthdayWish()
        {
            BirthdayFriends = new BirthdayNode[sr_NumOfDays];
            initBirthdays();
        }

        private void initBirthdays()
        {
            DateTime today = new DateTime(2018, 1, 1); 

            for(int i = 0; i < sr_NumOfDays; i++)
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
                DateTime birthday = DateTime.ParseExact(friend.Birthday, "d", new CultureInfo("En-us"));
                BirthdayFriends[birthday.DayOfYear - 1].m_BirthdayFriends.Add(friend);
            }
        }        
    }
}
