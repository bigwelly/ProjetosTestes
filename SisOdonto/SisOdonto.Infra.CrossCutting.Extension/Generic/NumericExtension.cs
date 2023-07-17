using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace SisOdonto.Infra.CrossCutting.Extension.Generic
{
    public static class NumericExtension
    {
        public static string ToCurrencyString(this decimal value)
        {
            return String.Format(new CultureInfo("pt-BR"), "{0:N2}", value);
            //A máscara "{0:C}" exibe o "R$");
        }
    }
}
