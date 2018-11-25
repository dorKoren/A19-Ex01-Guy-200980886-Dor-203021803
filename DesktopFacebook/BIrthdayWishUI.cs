using System.Collections.Generic;
using System.Windows.Forms;

namespace DesktopFacebook
{
    public static class BirthdayWishUI
    {
        #region Internal Static Methods
        internal static List<string> GetFriendsListNamesFromCheckListBox(CheckedListBox i_CheckedListBox)
        {
            List<string> list = new List<string>();

            foreach (CheckBox checkBox in i_CheckedListBox.CheckedItems)
            {
                list.Add(checkBox.Text);           
            }

            return list;
        }
        #endregion Internal Static Methods
    }
}
