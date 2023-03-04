using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LopushokSop.Classes
{
    public class LanguagesErrorClass
    {
        public static bool RussianError(char text)
        {
            if (text >= 'а' && text <= 'я')
            {
                return false;
            }
            if (text >= 'А' && text <= 'Я')
            {
                return false;
            }
            return true;
        }
        public static bool NumberError(char text)
        {
            if (text >= '0' && text <= '9')
            {
                return true;
            }
            return false;
            
        }
    }
}
