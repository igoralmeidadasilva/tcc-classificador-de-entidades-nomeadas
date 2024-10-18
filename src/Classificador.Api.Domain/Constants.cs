namespace Classificador.Api.Domain;

public static class Constants
{
    public static class Constraints
    {
        public static class User
        {
            public const int EMAIL_MAX_LENGHT = 45;
            public const int NAME_MAX_LENGHT = 45;
            public const int PASSWORD_MIN_LENGHT = 8;
            public const int PASSWORD_MAX_LENGHT = 64;
            public const string PASSWORD_FORMAT = @"^(?=.*[!@#$%^&*()_+\[\]{}|\\:;'"",.<>?/~]).*$";
            public const int CONTACT_MAX_LENGHT = 15;
            public const string CONTACT_FORMAT = @"^\(\d{2}\) \d{5}-\d{4}$";
        }

        public static class PrescribingInformation
        {
            public const int NAME_MAX_LENGHT = 45;
        }
        public static class NamedEntity
        {
            public const int NAME_MAX_LENGHT = 45;
        }
        public static class Category
        {
            public const int NAME_MAX_LENGHT = 45;
        }
        public static class Specialty
        {
            public const int NAME_MAX_LENGHT = 45;
        }
    }

}
