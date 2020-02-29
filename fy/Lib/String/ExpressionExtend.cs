using System;
using System.Linq.Expressions;

namespace Lib
{
    /// <summary>
    /// 合并表达式 And Or  Not扩展
    /// </summary>
    public static class ExpressionExtend
    {
        /// <summary>
        /// 合并表达式 expr1 AND expr2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
                return expr2;
            else if (expr2 == null)
                return expr1;            
            ParameterExpression newParameter = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(newParameter);
            var left = visitor.Replace(expr1.Body);
            var right = visitor.Replace(expr2.Body);
            var body = Expression.And(left, right);
            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }
        /// <summary>
        /// 合并表达式 expr1 or expr2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
                return expr2;
            else if (expr2 == null)
                return expr1;
            ParameterExpression newParameter = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(newParameter);
            var left = visitor.Replace(expr1.Body);
            var right = visitor.Replace(expr2.Body);
            var body = Expression.Or(left, right);
            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            if (expr == null)
                return null;
            var candidateExpr = expr.Parameters[0];
            var body = Expression.Not(expr.Body);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }
    /// <summary>
    /// 建立新表达式
    /// </summary>
    internal class NewExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _NewParameter { get; private set; }
        public NewExpressionVisitor(ParameterExpression param)
        {
            this._NewParameter = param;
        }
        public Expression Replace(Expression exp)
        {
            return this.Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this._NewParameter;
        }
    }
}
//实例：
//Expression<Func<People, bool>> lambda1 = x => x.Age > 5;
//Expression<Func<People, bool>> lambda2 = x => x.Id > 5;
//Expression<Func<People, bool>> lambda3 = lambda1.And(lambda2);
//Expression<Func<People, bool>> lambda4 = lambda1.Or(lambda2);
//Expression<Func<People, bool>> lambda5 = lambda1.Not();