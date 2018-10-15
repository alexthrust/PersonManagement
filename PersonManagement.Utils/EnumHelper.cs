using PersonManagement.Constants;

namespace PersonManagement.Utils
{
    public class EnumHelper
    {
        public static string GetPersonGenderName(object gender)
        {
            if (gender == null) return string.Empty;

            var genderName = (EGender)gender == EGender.Male ? EGender.Male.ToString() : EGender.Female.ToString();
            return genderName;
        }
    }
}