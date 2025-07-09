using Drammer.Common.Text;

namespace Drammer.Common.Tests.Text;

public sealed class RandomTextTests
{
    [Fact]
    public void Generate_ReplaceVowels()
    {
        // act
        var result = RandomText.Generate(1000);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Length.Should().Be(1000);
        result.Select(x => x.ToString()).Should().NotContain(RandomText.Vowels);
    }

    [Fact]
    public void Generate_WithoutReplaceVowels()
    {
        // act
        var result = RandomText.Generate(100, false);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Length.Should().Be(100);
    }
}