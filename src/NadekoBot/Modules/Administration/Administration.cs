#nullable disable
using NadekoBot.Common.Attributes;
using NadekoBot.Common.TypeReaders.Models;
using NadekoBot.Modules.Administration._common.results;
using NadekoBot.Modules.Administration.Services;
using Microsoft.Extensions.Http;

namespace NadekoBot.Modules.Administration;

public partial class Administration : NadekoModule<AdministrationService>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public Administration(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
}