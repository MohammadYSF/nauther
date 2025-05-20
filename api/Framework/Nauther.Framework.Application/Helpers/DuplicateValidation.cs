using FluentValidation;

namespace Nauther.Framework.Application.Helpers;

public static class DuplicateValidation
{
    public static void AddDuplicateFailures<TContext, TItem, TProperty>(
        ValidationContext<TContext> context,
        Func<TContext, IEnumerable<TItem>?> collectionSelector,
        Func<TItem, TProperty> selector, 
        Func<string, int, string> propertyNameBuilder,
        string fieldName)
    {
        var items = collectionSelector(context.InstanceToValidate)?.ToList() ?? [];
        
        var duplicates  = items
            .Select((item, index) => new { Value = selector(item)?.ToString(), Index = index })
            .Where(x => string.IsNullOrWhiteSpace(x.Value) == false)
            .GroupBy(x => x.Value)
            .Where(g => g.Count() > 1);

        foreach (var group in duplicates)
        {
            foreach (var item in group)
            {
                var propertyKey = propertyNameBuilder(fieldName, item.Index);
                context.AddFailure(propertyKey, "مقدار تکراری برای این فیلد وارد شده است.");
            }
        }
    }
}