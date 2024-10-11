using Drammer.Common.Mapping;

namespace Drammer.Common.Tests.Mapping;

public sealed class TestModelMapping : IMapping<SourceModel, DestinationModel>
{
    public DestinationModel? Map(SourceModel? source)
    {
        if (source == null)
        {
            return null;
        }

        return new DestinationModel
        {
            Name = source.Name
        };
    }
}