using Newtonsoft.Json.Converters;

namespace SSSM.WebAPI
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
