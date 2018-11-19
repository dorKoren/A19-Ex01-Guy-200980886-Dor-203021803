using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DesktopFacebook.BirthdayDictionary;
using static System.Windows.Forms.CheckedListBox;

namespace DesktopFacebook
{
    public class BirthdayWishSettings
    {
        internal static readonly int sr_CurrentDayOfYear = DateTime.Now.DayOfYear - 1;

        internal BirthdayDictionary BirthdayDictionary { get; set; }

        internal BirthdayWishSettings()
        {
            BirthdayDictionary = new BirthdayDictionary();
        }

        internal int CurrentDayOfYear
        {
            get { return sr_CurrentDayOfYear; }
        }

        public string GenerateCongratulations(CheckedItemCollection checkedItems, string i_BirthdayWish)
        {
            StringBuilder congrats = new StringBuilder("Congratulations ");

            // Concat all Friends NAMES
            foreach (string friendName in checkedItems)
            {
                congrats.Append(friendName + ", ");
            }

            congrats.Remove(congrats.Length - 2, 2);
            congrats.Append("!");

            // Concat text box
            congrats.AppendLine();
            congrats.AppendLine(i_BirthdayWish);
            return congrats.ToString();
        }
    }
}
