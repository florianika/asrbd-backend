using System;

namespace Application.Common.Converter
{
    public static class PythonToCSharpConverter
    {
        public static string WhereClouseToLinqString(string queryExpression)
        {
            var linqStringExpression =  queryExpression.Replace("|", "or");
            linqStringExpression = linqStringExpression.Replace("&", "and");
            return linqStringExpression;
        }
    }
}
