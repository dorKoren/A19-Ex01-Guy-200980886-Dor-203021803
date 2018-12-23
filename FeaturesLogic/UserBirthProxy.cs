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
