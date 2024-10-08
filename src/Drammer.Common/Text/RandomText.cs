﻿using System.Text;

namespace Drammer.Common.Text;

public static class RandomText
{
    internal static readonly string[] Vowels = { "a", "e", "i", "o", "u", "y" };

    /// <summary>
    /// Generate a random string
    /// </summary>
    /// <param name="length">
    /// The length.
    /// </param>
    /// <param name="replaceVowels">
    /// The replace vowels.
    /// </param>
    /// <returns>
    /// The random string.
    /// </returns>
    public static string Generate(int length = 6, bool replaceVowels = true)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            var ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * System.Random.Shared.NextDouble()) + 65)));
            builder.Append(ch);
        }

        var ret = builder.ToString().ToLowerInvariant();

        // replace vowels and other chars that can form a word
        return replaceVowels ? ret.MultiReplace(Vowels) : ret;
    }

    private static string MultiReplace(this string s, string[] needles)
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        // ReSharper disable once LoopCanBeConvertedToQuery
        for (var replacementNum = 0; replacementNum < needles.Length; ++replacementNum)
        {
            s = s.Replace(needles[replacementNum], System.Random.Shared.Next(0, 9).ToString());
        }

        return s;
    }
}