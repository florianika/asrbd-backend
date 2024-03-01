using System;

namespace Application.Common.Converter
{
    public static class PythonToCSharpConverter
    {
        public static string WhereClouseToLinqString(string queryExpression)
        {
            var linqStringExpression =  queryExpression.Replace("|", "or");
            linqStringExpression = linqStringExpression.Replace("&", "and");
            linqStringExpression = linqStringExpression.Replace(".isnull()", " == null", StringComparison.OrdinalIgnoreCase);
            linqStringExpression = linqStringExpression.Replace(".notnull()", " != null", StringComparison.OrdinalIgnoreCase);
            return linqStringExpression;
        }
    }
}
