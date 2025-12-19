using System.IO.Compression;

namespace Nerosoft.Linkyou.Toolkit;

/// <summary>
/// Helper methods for compressing and decompressing strings using GZip and Base64 encoding.
/// </summary>
public static class GzipHelper
{
    /// <summary>
    /// Compresses the specified UTF‑8 string using GZip and returns the compressed bytes encoded as a Base64 string.
    /// </summary>
    /// <param name="source">The input string to compress. If <c>null</c> an exception may be thrown by underlying calls.</param>
    /// <returns>A Base64 encoded string that represents the compressed input.</returns>
    public static string CompressToBase64(string source)
    {
        var buffer = Compress(source);

        return Convert.ToBase64String(buffer);
    }

    /// <summary>
    /// Compresses the specified UTF‑8 string using GZip and returns the compressed bytes.
    /// </summary>
    /// <param name="source">The input string to compress.</param>
    /// <returns>A byte array containing the GZip compressed representation of the input string.</returns>
    public static byte[] Compress(string source)
    {
        var data = Encoding.UTF8.GetBytes(source);

        var stream = new MemoryStream();
        var zip = new GZipStream(stream, CompressionMode.Compress, true);
        zip.Write(data, 0, data.Length);
        zip.Close();
        var buffer = new byte[stream.Length];
        stream.Position = 0;
        _ = stream.Read(buffer, 0, buffer.Length);
        stream.Close();

        return buffer;
    }

    /// <summary>
    /// Decodes the provided Base64 string to bytes and decompresses the result using GZip.
    /// </summary>
    /// <param name="base64Data">A Base64 encoded string that was produced from compressed bytes.</param>
    /// <returns>The original UTF‑8 string after decompression.</returns>
    public static string DecompressFromBase64(string base64Data)
    {
        var data = Convert.FromBase64String(base64Data);
        return Decompress(data);
    }

    /// <summary>
    /// Decompresses the specified GZip compressed byte array and returns the UTF‑8 string.
    /// </summary>
    /// <param name="data">The compressed byte array.</param>
    /// <returns>The decompressed UTF‑8 string.</returns>
    public static string Decompress(byte[] data)
    {
        return Decompress(data, data.Length);
    }

    /// <summary>
    /// Decompresses the specified GZip compressed byte array (using the given length) and returns the UTF‑8 string.
    /// </summary>
    /// <param name="data">The compressed byte array.</param>
    /// <param name="count">The number of bytes from <paramref name="data"/> to consider for decompression.</param>
    /// <returns>The decompressed UTF‑8 string.</returns>
    public static string Decompress(byte[] data, int count)
    {
        var stream = new MemoryStream(data, 0, count);
        var zip = new GZipStream(stream, CompressionMode.Decompress, true);
        var destStream = new MemoryStream();
        var buffer = new byte[0x1000];
        while (true)
        {
            var reader = zip.Read(buffer, 0, buffer.Length);
            if (reader <= 0)
            {
                break;
            }

            destStream.Write(buffer, 0, reader);
        }

        zip.Close();
        stream.Close();
        destStream.Position = 0;
        buffer = destStream.ToArray();
        destStream.Close();
        return Encoding.UTF8.GetString(buffer);
    }
}