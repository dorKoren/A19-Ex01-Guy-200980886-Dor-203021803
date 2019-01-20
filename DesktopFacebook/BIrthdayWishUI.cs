using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FeaturesLogic;
using static FeaturesLogic.BirthdayDictionary;

namespace DesktopFacebook
{
    internal class BirthdayWishUI : IFetch
    {
        internal BirthdayWishLogic BirthdayWishLogic { get; set; }

        internal BirthdayWishUI()
        {
            BirthdayWishLogic = new BirthdayWishLogic();         
        }

        internal static List<string> GetFriendsListNamesFromCheckListBox(CheckedListBox i_CheckedListBox)
        {
            List<string> list = new List<string>();

            foreach (CheckBox checkBox in i_CheckedListBox.CheckedItems)
            {
                list.Add(checkBox.Text);           
            }

            return list;
        }

        public void FetchInit(TabPage i_TabPageBirthday)
        {
            foreach (Control control in i_TabPageBirthday.Controls)
            {
                control.Visible = true;
            }
        }

        public void FetchReset(TabPage i_TabPageBirthday)
        {
            
            foreach (Control control in i_TabPageBirthday.Controls)
            {
                if (control is CheckedListBox)
                {
                    (control as CheckedListBox).Items.Clear();
                }
                else
                {
                    control.Visible = false;
                }
            }
            // BirthdayWishLogic = null;
        }
        
        internal void UpdateCheckedListBoxWishes(
            CheckedListBox i_CheckedListBox, TextBox i_TextBox, Button i_Button)
        {
            int currentDay = BirthdayWishLogic.CurrentDayOfYear;      
            bool enabled = true;

            BirthdayNode curNode = BirthdayWishLogic.BirthdayDictionary.BirthdayFriends[currentDay];

            foreach (User friend in curNode.BirthdayFriends)
            {
                i_CheckedListBox.Items.Add(new UserBirthProxy { User = friend });
            }

            if (i_CheckedListBox.Items.Count != 0)
            {
                i_Button.Enabled  = enabled;
                i_TextBox.Enabled = enabled;
            }
        }

        internal void PostWishToFriends(CheckedListBox i_CheckedListBoxWishes, TextBox i_TextBoxWish, User i_User)
        {
            List<string> friends = GetFriendsListNamesFromCheckListBox(i_CheckedListBoxWishes);

            string congrats = BirthdayWishLogic.GenerateCongratulations(friends, i_TextBoxWish.Text);

            i_User.PostStatus(congrats);
        }
    }
}
