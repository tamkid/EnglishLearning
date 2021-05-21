using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning
{
    public class EnumHelper
    {
        public static string GetName(Type enumType, int value)
        {
            Enums.WordType myEnum = (Enums.WordType)value;
            string a = myEnum.ToString();

            //Enums.WordType myEnum = (Enums.WordType)Enum.Parse(typeof(Enums.WordType), myString);

            var x = Enum.GetValues(enumType).Cast<int>().Select(o => o == value).FirstOrDefault();
            return null;
        }
    }
}
