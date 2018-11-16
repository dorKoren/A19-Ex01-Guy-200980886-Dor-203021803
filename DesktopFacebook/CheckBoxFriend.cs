using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    public class CheckBoxFriend : CheckBox  // DOR ask balmas!
    {
        internal string   Name     { get; set; }
        internal User     Friend   { get; }
        internal  CheckBox CheckBox { get; }
       
        

        public CheckBoxFriend(User i_Friend)
        {
            Friend = i_Friend;
            CheckBox = new CheckBox();
            CheckBox.Text = i_Friend.Name + i_Friend.LastName;
            Name = i_Friend.Name + i_Friend.LastName;
            CheckBox.Checked = true;
        }
    }
}
