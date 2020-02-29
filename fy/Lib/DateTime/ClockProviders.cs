using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class ClockProviders
    {
        public static UnspecifiedClockProvider Unspecified { get; } = new UnspecifiedClockProvider();

        public static LocalClockProvider Local { get; } = new LocalClockProvider();

        public static UtcClockProvider Utc { get; } = new UtcClockProvider();
    }

    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with UTC times.
    /// </summary>
    public class UtcClockProvider : IClockProvider
    {
        public DateTime Now => DateTime.UtcNow;

        public DateTimeKind Kind => DateTimeKind.Utc;

        public bool SupportsMultipleTimezone => true;

        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return dateTime;
        }

        internal UtcClockProvider()
        {

        }
    }


    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with local times.
    /// </summary>
    public class LocalClockProvider : IClockProvider
    {
        public DateTime Now => DateTime.Now;

        public DateTimeKind Kind => DateTimeKind.Local;

        public bool SupportsMultipleTimezone => false;

        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            }

            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            return dateTime;
        }

        internal LocalClockProvider()
        {

        }
    }

    public class UnspecifiedClockProvider : IClockProvider
    {
        public DateTime Now => DateTime.Now;

        public DateTimeKind Kind => DateTimeKind.Unspecified;

        public bool SupportsMultipleTimezone => false;

        public DateTime Normalize(DateTime dateTime)
        {
            return dateTime;
        }

        internal UnspecifiedClockProvider()
        {

        }
    }

    /// <summary>
    /// Defines interface to perform some common date-time operations.
    /// </summary>
    public interface IClockProvider
    {
        /// <summary>
        /// Gets Now.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Gets kind.
        /// </summary>
        DateTimeKind Kind { get; }

        /// <summary>
        /// Is that provider supports multiple time zone.
        /// </summary>
        bool SupportsMultipleTimezone { get; }

        /// <summary>
        /// Normalizes given <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">DateTime to be normalized.</param>
        /// <returns>Normalized DateTime</returns>
        DateTime Normalize(DateTime dateTime);
    }
}
