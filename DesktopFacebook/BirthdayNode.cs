using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    public class BirthdayNode
    {
        internal DateTime m_Date { get; set; }

        internal List<User> m_BirthdayFriends { get; set; }


        internal BirthdayNode(DateTime i_Date)
        {
            int year = i_Date.Year;
            int month =  i_Date.Month;
            int day = i_Date.Day;

            

            m_Date = new DateTime(year, month, day);
            m_BirthdayFriends = new List<User>();
        }
    }
}
