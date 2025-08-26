using System.ComponentModel;
using System.Reflection;

namespace TestWebService_RealIT.Common
{
    /// <summary>
    /// класс перечислений проекта
    /// </summary>
    public class DefaultEnums
    {
        /// <summary>
        /// методы платежа
        /// </summary>
        public enum PaymentMethod
        {
            [Description("toCard")]
            toCard,
            [Description("sbp")]
            sbp,
            [Description("toAccount")]
            toAccount,
            [Description("transgran")]
            transgran,
            [Description("transgranSBP")]
            transgranSBP,
            [Description("nspk")]
            nspk
        }

        /// <summary>
        /// метод получения описания значения Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
