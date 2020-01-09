using System;

namespace Assure.Core.Utilities.Interfaces.Services
{
    public interface IDayCalculator
    {
        /// <summary>
        /// Calculate days inclusively, i.e. 2018-01-01 => 2018-01-01 is actually 2018-01-01 00:00:00 => 2018-01-01 23:59:59 therefore will return 1.
        /// Should handle timezone changes.
        /// start date MUST be before end date.
        /// More of an example than anything else. Recommend NodaTime to deal with date/time calculations properly.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        int CalculateDaysInclusive(DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
