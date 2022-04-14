using System.IO.Compression;

using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;

namespace Fusionary.Web.ConfigureOptions;

/// <summary>
/// Response compression configuration.
/// </summary>
public class ConfigureResponseCompressionOptions :
    IConfigureOptions<ResponseCompressionOptions>,
    IConfigureOptions<BrotliCompressionProviderOptions>,
    IConfigureOptions<GzipCompressionProviderOptions> {
    /// <inheritdoc />
    public void Configure(BrotliCompressionProviderOptions options) => options.Level = CompressionLevel.Optimal;

    /// <inheritdoc />
    public void Configure(GzipCompressionProviderOptions options) => options.Level = CompressionLevel.Optimal;

    /// <inheritdoc />
    public void Configure(ResponseCompressionOptions options) {
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    }
}