﻿using System.Windows.Forms;
using FeaturesLogic;

namespace DesktopFacebook
{
    internal class FetchMaker
    {
        internal BirthdayWishUI BirthdayWishUI { get; }
        internal SharedPhotosUI SharedPhotosUI { get; }

        internal ICommand ResetLogicsCommand;

        internal FetchMaker(BirthdayWishUI i_BirthdayWishUI, SharedPhotosUI i_SharedPhotosUI)
        {
            BirthdayWishUI = i_BirthdayWishUI;
            SharedPhotosUI = i_SharedPhotosUI;
            ResetLogicsCommand = new ResetLogicsCommand(BirthdayWishUI.BirthdayWishLogic , i_SharedPhotosUI.SharedPhotosLogic);
        }

        public void FetchInitBirthdayWishUI(TabPage i_TabPage)
        {
            BirthdayWishUI.FetchInit(i_TabPage);
        }
        public void FetchInitSharedPhotosUI(TabPage i_TabPage)
        {
            SharedPhotosUI.FetchInit(i_TabPage);
        }

        public void FetchResetBirthdayWishUI(TabPage i_TabPage)
        {
            BirthdayWishUI.FetchReset(i_TabPage);
        }

        public void FetchResetSharedPhotosUI(TabPage i_TabPage)
        {
            SharedPhotosUI.FetchReset(i_TabPage);
        }

        public void ResetLogics()
        {
            ResetLogicsCommand.Execute();
        }

    }
}
