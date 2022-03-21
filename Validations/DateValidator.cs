namespace Validations
{
    public static class DateValidator
    {
        public static string? ValidateNotInPast(DateTime? date)
        {
            if (date == null)
            {
                return "Invalid";
            }

            if (date?.Date < DateTime.UtcNow.Date)
            {
                return "Past date";
            }

            return null;
        }

        public static string? ValidateIsOfAge(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return "Invalid";
            }

            if (dateOfBirth?.AddYears(18) > DateTime.Now)
            {
                return "User must be +18";
            }

            return null;
        }
    }
}