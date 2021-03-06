﻿using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;
using System.Globalization;

namespace FeaturesLogic
{
    public sealed class BirthdayDictionary
    {
        #region Nested Class
        public class BirthdayNode
        {
            #region Nested Class Members
            internal DateTime Date { get; set; }
            public List<User> BirthdayFriends { get; set; }
            #endregion Nested Class Members

            #region Nestes Class Constructor
            public BirthdayNode(DateTime i_Date)
            {
                int year = i_Date.Year;
                int month = i_Date.Month;
                int day = i_Date.Day;

                Date = new DateTime(year, month, day);
                BirthdayFriends = new List<User>();
            }
            #endregion Nestes Class Constructor
        }
        #endregion Nested Class

        #region Class Members
        private static BirthdayDictionary s_Instance = null;
        private static object s_LockObj = new Object();
        private static readonly int sr_NumOfDays = 365;
        public BirthdayNode[] BirthdayFriends { get; set; }

        #endregion Class Members

        #region Constructor
        private BirthdayDictionary()
        {
            BirthdayFriends = new BirthdayNode[sr_NumOfDays];
            initBirthdays();
        }

        #endregion Constructor

        #region Properties
        public static BirthdayDictionary Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (s_LockObj)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new BirthdayDictionary();
                        }
                    }
                }

                return s_Instance;
            }
        }
        #endregion Properties

        #region Public Methods
        public void FillBirthdays(User i_LoggedInUser)
        {
            foreach (User friend in i_LoggedInUser.Friends)
            {
                DateTime birthday = DateTime.ParseExact(
                    friend.Birthday, "d", new CultureInfo("En-us"));

                BirthdayFriends[birthday.DayOfYear - 1].BirthdayFriends.Add(friend);
            }
        }
        #endregion Public Methods

        #region  Private Methods
        private void initBirthdays()
        {
            DateTime today = new DateTime(2018, 1, 1);

            for (int i = 0; i < sr_NumOfDays; i++)
            {
                BirthdayFriends[i] = new BirthdayNode(today);

                DateTime tomorrow = today.AddDays(1);
                today = tomorrow;
            }
        }
        #endregion Private Methods
    }
}
