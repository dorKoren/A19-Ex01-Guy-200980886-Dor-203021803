using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    public class CheckBoxFriend : CheckBox 
    {
        internal string   Name     { get; set; }
        internal User     Friend   { get;      }
        //internal CheckBox CheckBox { get;      }
          
        public CheckBoxFriend(User i_Friend)
        {
            Friend = i_Friend;
            //CheckBox = new CheckBox();
            this.Text = i_Friend.Name + i_Friend.LastName;
            Name = i_Friend.Name + i_Friend.LastName;
            this.Checked = true;
        }
    }
}
