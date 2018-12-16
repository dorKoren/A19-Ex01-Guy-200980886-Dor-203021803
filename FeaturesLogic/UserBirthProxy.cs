using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class UserBirthProxy
    {
        public User User { get; set; }

        public override string ToString()
        {
            return string.Format("{0} was born on {1}", User.Name, User.Birthday);
        }
    }
}
