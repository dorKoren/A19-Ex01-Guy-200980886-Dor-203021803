using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopFacebook
{
    internal class FetchMaker
    {
        internal BirthdayWishUI m_BirthdayWishUI;
        internal SharedPhotosUI m_SharedPhotosUI;

        internal FetchMaker(BirthdayWishUI i_BirthdayWishUI, SharedPhotosUI i_SharedPhotosUI)
        {
            m_BirthdayWishUI = i_BirthdayWishUI;
            m_SharedPhotosUI = i_SharedPhotosUI;
        }

        public void FetchBirthdayWishUI(List<Control> i_Controls)
        {
            m_BirthdayWishUI.Fetch(i_Controls);
        }
        public void FetchSharedPhotosUI(List<Control> i_Controls)
        {
            m_SharedPhotosUI.Fetch(i_Controls);
        }
    }
}
