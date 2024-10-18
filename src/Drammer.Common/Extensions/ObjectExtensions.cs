using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Drammer.Common.Extensions;

public static class ObjectExtensions
{
    public static bool IsNotNull([NotNullWhen(true)] this object? val)
    {
        return val != null;
    }

    public static bool IsNull([NotNullWhen(false)] this object? val)
    {
        return !val.IsNotNull();
    }

    public static bool IsDefault(this int val)
    {
        return val == 0;
    }

    public static bool IsDefault(this string val)
    {
        return val.Equals(string.Empty, StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsDefault(this DateTime val)
    {
        return val == default;
    }

    public static T WithValue<T, TProperty>(
        this T model,
        Expression<Func<T, TProperty>> propertyPicker,
        TProperty value,
        bool convertDefaultToNull = true)
    {
        if (!value.IsNotNull())
        {
            return model;
        }

        var defaultValue = GetDefaultValueOfNonNullable(value);
        if (convertDefaultToNull && Equals(value, defaultValue))
        {
            model.SetPropertyValueNull(propertyPicker);
        }
        else
        {
            model.SetPropertyValue(propertyPicker, value);
        }

        return model;
    }

    public static T WithNullableValue<T, TProperty>(
        this T model,
        Expression<Func<T, TProperty?>> propertyPicker,
        TProperty? value)
    {
        if (!value.IsNotNull())
        {
            return model;
        }

        model.SetPropertyValue(propertyPicker, value);

        return model;
    }

    private static void SetPropertyValue<T, TValue>(
        this T target,
        Expression<Func<T, TValue>> memberLamda,
        TValue value)
    {
        if (memberLamda.Body is MemberExpression memberSelectorExpression)
        {
            var property = memberSelectorExpression.Member as PropertyInfo;
            if (property != null)
            {
                property.SetValue(target, value, null);
            }
        }
    }

    private static void SetPropertyValueNull<T, TValue>(this T target, Expression<Func<T, TValue>> memberLamda)
    {
        if (memberLamda.Body is MemberExpression memberSelectorExpression)
        {
            var property = memberSelectorExpression.Member as PropertyInfo;
            if (property != null)
            {
                property.SetValue(target, null, null);
            }
        }
    }

    private static object? GetDefaultValueOfNonNullable<TProperty>(TProperty value)
    {
        if (value is string)
        {
            return string.Empty;
        }

        if (value is int)
        {
            return default(int);
        }

        if (value is DateTime)
        {
            return default(DateTime);
        }

        return default(TProperty);
    }
}