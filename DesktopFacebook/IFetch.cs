using System.Windows.Forms;

namespace DesktopFacebook
{
    internal interface IFetch
    {
        void FetchInit(TabPage i_TabPage);

        void FetchReset(TabPage i_TabPage);
    }
}
