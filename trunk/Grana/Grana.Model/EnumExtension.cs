using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.Model
{
    public class EnumExtension
    {
        public static TEnumType? Parse<TEnumType>(string value) where TEnumType : struct
        {
            if (!string.IsNullOrEmpty(value))
                return (TEnumType)Enum.Parse(typeof(TEnumType), value);

            return null;
        }

        public static TEnumType? Parse<TEnumType>(int value) where TEnumType : struct
        {
            return Parse<TEnumType>(value.ToString());
        }

        public static TEnumType? Parse<TEnumType>(byte value) where TEnumType : struct
        {
            return Parse<TEnumType>(value.ToString());
        }
    }
}
