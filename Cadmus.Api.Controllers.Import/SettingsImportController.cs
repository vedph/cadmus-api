using Cadmus.Api.Models;
using Cadmus.Core;
using Cadmus.Core.Storage;
using Cadmus.Import;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Cadmus.Api.Controllers.Import;

/// <summary>
/// Settings import controller.
/// </summary>
/// <seealso cref="ControllerBase" />
[Authorize]
[ApiController]
[Tags("Settings")]
public sealed class SettingsImportController : ControllerBase
{
    private readonly IRepositoryProvider _repositoryProvider;
    private readonly ILogger<SettingsImportController> _logger;

    public SettingsImportController(IRepositoryProvider repositoryProvider,
        ILogger<SettingsImportController> logger)
    {
        _repositoryProvider = repositoryProvider;
        _logger = logger;
    }

    /// <summary>
    /// Uploads one or more facets importing them into the Cadmus database.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <param name="model">The import model.</param>
    /// <returns>Result.</returns>
    /// <exception cref="InvalidOperationException">No ID for thesaurus</exception>
    [Authorize(Roles = "admin")]
    [HttpPost("api/settings/import")]
    public ImportResult UploadSettings(
        // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#handle-forms-and-file-uploads
        IFormFile file,
        [FromQuery] bool dryRun)
    {
        _logger?.LogInformation("User {UserName} importing settings from " +
            "{FileName} from {IP} (dry={IsDry})",
            User.Identity!.Name,
            file.FileName,
            HttpContext.Connection.RemoteIpAddress,
            dryRun == true);

        ICadmusRepository repository = _repositoryProvider.CreateRepository();

        try
        {
            Stream stream = file.OpenReadStream();
            using JsonSettingsReader reader = new(stream);

            List<string> ids = [];

            while ((reader.Next()))
            {
                // each settings entry is a JSON object with as many properties
                // as setting entries to import, each named after the setting ID,
                // and having as value the JSON object with the setting definition
                JsonElement settings = reader.Current!;

                foreach (JsonProperty prop in settings.EnumerateObject())
                {
                    // read the setting ID from the property name
                    string id = prop.Name;
                    if (string.IsNullOrEmpty(id)) continue;
                    _logger?.LogInformation("Importing setting ID: {Id}", id);

                    // ensure that the property value is a JSON object
                    if (prop.Value.ValueKind != JsonValueKind.Object)
                    {
                        _logger?.LogError("  - skipped setting ID {Id} because " +
                            "it is not a JSON object", id);
                        continue;
                    }

                    // read the setting definition as JSON string and add it
                    // to the repository
                    ids.Add(id);
                    JsonElement setting = prop.Value;
                    if (!dryRun) repository.AddSetting(id, setting.ToJson());
                }
            }

            return new ImportResult
            {
                ImportedIds = ids
            };
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error importing settings: {Message}",
                ex.Message);
            return new ImportResult
            {
                Error = ex.Message
            };
        }
    }
}
