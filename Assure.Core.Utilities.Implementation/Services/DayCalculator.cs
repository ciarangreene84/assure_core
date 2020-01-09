using Assure.Core.Utilities.Interfaces.Services;
using System;
using Boot4ServiceCollection.Attributes;

namespace Assure.Core.Utilities.Implementation.Services
{
    [AddSingleton(typeof(IDayCalculator))]
    public class DayCalculator : IDayCalculator
    {
        public int CalculateDaysInclusive(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (startDate >= endDate) throw new ArgumentException("Parameter startDate must be before parameter endDate");
            return Convert.ToInt32((endDate - startDate).TotalDays) + 1;
        }
    }
}
