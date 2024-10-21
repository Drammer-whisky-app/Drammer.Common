namespace Drammer.Common.Extensions;

/// <summary>
/// The integer extensions.
/// </summary>
public static class IntegerExtensions
{
    /// <summary>
    /// Floors the integer to the nearest multiple of the specified value.
    /// </summary>
    /// <param name="i">The input value.</param>
    /// <param name="nearest">The nearest integer value (multiple of 10).</param>
    /// <returns>An <see cref="int"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the nearest value is invalid.</exception>
    public static int FloorToNearest(this int i, int nearest)
    {
        if (nearest <= 0 || nearest % 10 != 0)
        {
            throw new ArgumentOutOfRangeException(nameof(nearest), "Must round to a positive multiple of 10");
        }

        return (int)(Math.Floor(i / (double)nearest) * nearest);
    }
}