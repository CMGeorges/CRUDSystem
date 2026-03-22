using System;

namespace CRUDSystem.Application.Services
{
    public static class AgeCalculator
    {
        public static int CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            var age = currentDate.Year - birthDate.Year;
            if (birthDate.Date > currentDate.Date.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
