using System.ComponentModel;
using System.Reflection;

namespace Drammer.Common.ComponentModel;

[AttributeUsage(AttributeTargets.Field)]
public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
{
    /// <summary>
    /// The name property.
    /// </summary>
    private PropertyInfo? _nameProperty;

    /// <summary>
    /// The resource type.
    /// </summary>
    private Type? _resourceType;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute"/> class.
    /// </summary>
    /// <param name="displayNameKey">
    /// The display name key.
    /// </param>
    public LocalizedDisplayNameAttribute(string displayNameKey)
        : base(displayNameKey)
    {
    }

    /// <summary>
    /// Gets or sets the name resource type.
    /// </summary>
    public Type? NameResourceType
    {
        get => _resourceType;

        set
        {
            _resourceType = value;

            // initialize nameProperty when type property is provided by setter
            _nameProperty = _resourceType?.GetProperty(
                base.DisplayName,
                BindingFlags.Static | BindingFlags.Public);
        }
    }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public override string DisplayName
    {
        get
        {
            // check if nameProperty is null and return original display name value
            if (_nameProperty == null)
            {
                return base.DisplayName;
            }

            return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null)!;
        }
    }
}