using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace DesktopFacebook.UI
{
    public static class BirthdayWishUI
    {
        public static List<string> GetNames(CheckedItemCollection i_CheckedItems)
        {
            List<string> names = new List<string>();

            foreach (string friendName in i_CheckedItems)
            {
                names.Add(friendName);
            }

            return names;
        }
    }
}
