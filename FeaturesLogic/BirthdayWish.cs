using System;
using System.Collections.Generic;
using System.Text;

namespace FeaturesLogic
{
    public class BirthdayWish
    {
        #region Class Members
        public BirthdayDictionary BirthdayDictionary { get; set; }
        public int                CurrentDayOfYear   { get;      }
        #endregion Class Members

        #region Constructor
        public BirthdayWish()
        {
            BirthdayDictionary = BirthdayDictionary.Instance;
            CurrentDayOfYear   = DateTime.Now.DayOfYear - 1;
        }
        #endregion Constructor

        #region Public Methods
        public string GenerateCongratulations(List<string> i_CheckedItems, string i_BirthdayWish)
        {
            StringBuilder congrats = new StringBuilder("Congratulations ");

            // Concat all Friends NAMES
            foreach (string friendName in i_CheckedItems)
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
        #endregion Public Methods
    }
}
