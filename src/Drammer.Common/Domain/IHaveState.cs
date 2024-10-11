namespace Drammer.Common.Domain;

public interface IHaveState<T>
    where T : IEntity
{
    /// <summary>
    /// Returns the current state.
    /// </summary>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    T GetState();
}