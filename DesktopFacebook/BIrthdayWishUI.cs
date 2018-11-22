using System.Collections.Generic;
using System.Windows.Forms;

namespace DesktopFacebook
{
    public static class BIrthdayWishUI
    {
        internal static List<string> GetFriendsListNamesFromCheckListBox(CheckedListBox i_CheckedListBox)
        {
            List<string> list = new List<string>();

            foreach (CheckBox chechBox in i_CheckedListBox.CheckedItems)
            {
                list.Add(chechBox.Text);
                
            }

            return list;
        }
    }
}
