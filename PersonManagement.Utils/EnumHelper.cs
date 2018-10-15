using PersonManagement.Constants;

namespace PersonManagement.Utils
{
    public class EnumHelper
    {
        public string GetPersonGenderName(object gender)
        {
            if (gender == null) return string.Empty;

            switch ((EGender)gender)
            {
                case EGender.Male:
                    return EGender.Male.ToString();
                case EGender.Female:
                        return EGender.Female.ToString();
            }

            return string.Empty;
        }
    }
}