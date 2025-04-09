namespace Drammer.Common.Cryptography;

public static class TruncatedSHA256
{
    /// <summary>
    /// Computes the SHA256 hash of the input data and truncates it to 128 bits (16 bytes).
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>The hashed data.</returns>
    public static byte[] Compute(byte[] data)
    {
        var hash = System.Security.Cryptography.SHA256.HashData(data);
        var truncatedHash = new byte[16];
        Buffer.BlockCopy(hash, 0, truncatedHash, 0, truncatedHash.Length);
        return truncatedHash;
    }

    /// <summary>
    /// Computes the SHA256 hash of the input string and truncates it to 128 bits (16 bytes).
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>The hashed data as base 64 string.</returns>
    public static string ComputeToBase64(string data)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        var hash = Compute(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Computes the SHA256 hash of the input string and truncates it to 128 bits (16 bytes).
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>The hashed data as hex string.</returns>
    public static string ComputeToHex(string data)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        var hash = Compute(bytes);
        return Convert.ToHexString(hash);
    }
}