using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Api
{
    public class ValueExpression
    {
        internal static string GetValue(string expression, IDictionary<string, object> inputArguments, object outputResult)
        {
            const string Input = "IN:";
            const string Output = "OUT:";
            object obj = null;
            if (expression.StartsWith(Input))
            {
                expression = expression.Replace(Input, "");

                if (expression.IndexOf(".") >= 0)
                {
                    obj = inputArguments[expression.Split('.')[0]];
                }
                else
                {
                    obj = inputArguments[expression];
                }
            }
            else
            {
                obj = outputResult;
                expression = expression.Replace(Output, "");
            }

            string value = string.Empty;
            if (expression.IndexOf(".") >= 0)
            {
                var propertyValue = obj.GetType()?.GetProperty(expression.Split('.')[1])?.GetValue(obj);
                //value = int.Parse(propertyValue.ToString());
                value = propertyValue.ToString();
            }
            else
            {
                //value = int.Parse(obj.ToString());
                value = obj.ToString();
            }

            return value;
        }
    }
}
