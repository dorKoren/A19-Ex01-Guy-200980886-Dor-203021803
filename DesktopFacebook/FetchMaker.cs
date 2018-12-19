using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    internal class FetchMaker
    {
        internal BirthdayWishUI BirthdayWishUI { get; }
        internal SharedPhotosUI SharedPhotosUI { get; }

    internal FetchMaker(BirthdayWishUI i_BirthdayWishUI, SharedPhotosUI i_SharedPhotosUI)
        {
            BirthdayWishUI = i_BirthdayWishUI;
            SharedPhotosUI = i_SharedPhotosUI;
        }

        public void FetchInitBirthdayWishUI(Form i_Form)
        {
            BirthdayWishUI.FetchInit(i_Form);
        }
        public void FetchInitSharedPhotosUI(Form i_Form)
        {
            SharedPhotosUI.FetchInit(i_Form);
        }

        public void FetchResetBirthdayWishUI(Form i_Form)
        {
            BirthdayWishUI.FetchReset(i_Form);
        }

        public void FetchResetSharedPhotosUI(Form i_Form)
        {
            SharedPhotosUI.FetchReset(i_Form);
        }
    }
}
