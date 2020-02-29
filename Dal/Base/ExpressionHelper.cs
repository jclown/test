using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dal
{
    public class ExpressionHelper
    {
        public static LambdaExpression Build(List<BinaryExpression> expressionList, ParameterExpression parameterExpression)
        {
            var expression = Build(expressionList);
            if (expression == null) return null;
            return Expression.Lambda(expression, parameterExpression);
        }

        public static BinaryExpression Build(List<BinaryExpression> expressionList)
        {
            if (expressionList.Count == 0) return null;
            if (expressionList.Count == 1) return expressionList[0];

            var expression1 = expressionList[0];
            for (int i = 1; i < expressionList.Count; i++)
            {
                if (expressionList.Count <= i) break;
                expression1 = Expression.AndAlso(expression1, expressionList[i]);
            }

            return expression1;
        }

        public static BinaryExpression Build<T>(ParameterExpression parameterExpression, string columnName, T value)
        {
            var callExpression = Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(T) }, parameterExpression, Expression.Constant(columnName));
            return Expression.Equal(callExpression, Expression.Constant(value));
        }
    }
}
