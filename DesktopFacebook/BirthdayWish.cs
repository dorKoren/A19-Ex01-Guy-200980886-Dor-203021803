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
        private Birthday[] m_BirthdayFriends { get; set; }

        internal BirthdayWish()
        {
            m_BirthdayFriends = new Birthday[sr_NumOfDays];
            initBirthdayFriends();
        }

        private void initBirthdayFriends()
        {
            DateTime today = new DateTime(2018, 1, 1);

            foreach(Birthday currentDay in m_BirthdayFriends)
            {
                currentDay.m_Date = today;
                today.AddDays(1);
            }
        }

        internal class Birthday
        {
            internal DateTime m_Date { get; set; }
            internal List<User> m_BirthdayFriends { get; set; }


            internal Birthday(DateTime i_Date)
            {
                m_Date = i_Date;
                m_BirthdayFriends = new List<User>();
            }        
        }
    }
}
