using System.Collections.Generic;

using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    internal interface IFetch
    {
        void FetchInit(Form i_Form);

        void FetchReset(Form i_Form);
    }
}
