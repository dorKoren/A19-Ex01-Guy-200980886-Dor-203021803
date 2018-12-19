using System.Collections.Generic;

using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    internal interface IFetch
    {
        void FetchInit(TabPage i_TabPage);

        void FetchReset(TabPage i_TabPage);
    }
}
