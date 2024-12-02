using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Net.Http;

namespace CA3.Client.Pages;
public class DrugService
{
    private readonly HttpClient _httpClient;

    public DrugService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FullDrug>> GetDrugsAsync(string filter)
    {
        string url = "https://api.fda.gov/drug/label.json?search=_exists_:openfda.route+AND+_exists_:openfda.application_number&limit=100";
        if (!string.IsNullOrWhiteSpace(filter) && filter != "*")
        {
            url = $"https://api.fda.gov/drug/label.json?search=_exists_:openfda.route+AND+_exists_:openfda.application_number+AND+openfda.generic_name:{filter.ToUpper()}";
        }
        try
        {
            var response = await _httpClient.GetFromJsonAsync<DrugListResponse>(url);
            if (response?.Results != null)
            {
                var res = response.Results.Select(drug => { drug.IsExpanded = false; return drug; }).ToList();
                return res;
            }

            return new List<FullDrug>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching drugs: {ex.Message}");
            throw;
        }
    }
}


public class DrugListResponse
{
    public List<FullDrug>? Results { get; set; }
}

public class FullDrug
{
    public bool IsExpanded { get; set; }
    public string? EffectiveTime { get; set; } = string.Empty;
    public List<string>? InactiveIngredient { get; set; }
    public List<string>? Purpose { get; set; }
    public List<string>? KeepOutOfReachOfChildren { get; set; }
    public List<string>? Warnings { get; set; }
    public List<string>? Questions { get; set; }
    public List<string>? SplProductDataElements { get; set; }
    public OpenFDA? Openfda { get; set; }
    public string? SetId { get; set; } = string.Empty;
    public string? Id { get; set; } = string.Empty;
    public string? Version { get; set; } = string.Empty;
}

public class OpenFDA
{
    [JsonPropertyName("application_number")]
    public List<string>? ApplicationNumber { get; set; }
    [JsonPropertyName("substance_name")]
    public List<string>? SubstanceName { get; set; }
    [JsonPropertyName("brand_name")]
    public List<string>? BrandName { get; set; }
    [JsonPropertyName("generic_name")]
    public List<string>? GenericName { get; set; }
    [JsonPropertyName("route")]
    public List<string>? Route { get; set; }
    [JsonPropertyName("pharm_class_cs")]
    public List<string>? PharmClassCS { get; set; }
}

