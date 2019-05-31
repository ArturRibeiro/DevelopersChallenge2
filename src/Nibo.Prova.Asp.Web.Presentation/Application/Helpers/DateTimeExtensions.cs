using System;
using System.Globalization;

namespace Nibo.Prova.Asp.Web.Presentation.Application.Helpers
{
    /// <summary>
    /// It facilitates the conversion of a date in string format "yyyyMMdd" to a DateTime structure
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a string to the data format in a structure datetime
        /// </summary>
        /// <param name="s">Value</param>
        /// <param name="format">A format specifier that defines the required format</param>
        /// <param name="cultureString">The name of a culture. name is not case-sensitive.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, string format = "yyyyMMdd", string cultureString = "en-US")
        {
            try
            {
                var r = DateTime.ParseExact(
                    s: s,
                    format: format,
                    provider: CultureInfo.GetCultureInfo(cultureString));
                return r;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (CultureNotFoundException)
            {
                throw; 
            }
        }
    }
}
