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
    }
}