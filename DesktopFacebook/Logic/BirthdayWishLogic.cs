﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopFacebook
{
    internal class BirthdayWishLogic
    {

        internal int CurrentDayOfYear { get; }

        internal BirthdayDictionary BirthdayDictionary { get; set; }

        internal BirthdayWishLogic()
        {
            BirthdayDictionary = new BirthdayDictionary();
            CurrentDayOfYear = DateTime.Now.DayOfYear - 1;
        }

        internal string GenerateCongratulations(List<string> i_CheckedItems, string i_BirthdayWish)
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
