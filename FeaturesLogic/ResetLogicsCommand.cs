using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeaturesLogic
{
    public class ResetLogicsCommand : ICommand
    {
        private BirthdayWishLogic BirthdayLogic;
        private SharedPhotosLogic PhotosLogic;

        public ResetLogicsCommand(BirthdayWishLogic i_BirthdayWishLogic, SharedPhotosLogic i_SharedPhotosLogic)
        {
            BirthdayLogic = i_BirthdayWishLogic;
            PhotosLogic = i_SharedPhotosLogic;
        }

        public void Execute()
        {
            BirthdayLogic = null;
            PhotosLogic = null;
        }
    }
}
