using System;
using System.Collections.Generic;
using System.Text;

namespace FeaturesLogic
{
    public class BirthdayWish
    {
        public   BirthdayDictionary   BirthdayDictionary { get; set; }
        public   int                  CurrentDayOfYear   { get;      }

        public BirthdayWish()
        {
            BirthdayDictionary = new BirthdayDictionary();
            CurrentDayOfYear   = DateTime.Now.DayOfYear - 1;
        }

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
    }
}
