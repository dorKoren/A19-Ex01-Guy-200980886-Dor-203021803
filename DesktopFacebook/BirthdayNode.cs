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
        internal DateTime Date { get; set; }

        internal List<User> BirthdayFriends { get; set; }


        internal BirthdayNode(DateTime i_Date)
        {
            Date = i_Date;
            BirthdayFriends = new List<User>();
        }
    }
}
