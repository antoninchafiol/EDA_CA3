using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using Microsoft.VisualBasic;
using System;

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
                foreach (var r in res)
                {
                    if (r.Openfda == null)
                    {
                        throw new ArgumentNullException("OpenFDA is null");
                        //Console.WriteLine(string.Join("", r.Purpose) ?? "unknoen");
                    }

                }
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

    [JsonPropertyName("active_ingredient")]
    public List<string>? ActiveIngredients { get; set; }
// --------------------------------------------------------
    [JsonPropertyName("indications_and_usage")]
    public List<string>? IndicationAndUsage { get; set; }
// --------------------------------------------------------
    [JsonPropertyName("warnings")]
    public List<string>? Warnings { get; set; }
// --------------------------------------------------------
    [JsonPropertyName("warnings_and_precautions")]
    public List<string>? WarningsAndPrecautions { get; set; }
// --------------------------------------------------------
    [JsonPropertyName("adverse_reactions")]
    public List<string>? AdverseReactions { get; set; }
// --------------------------------------------------------
    [JsonPropertyName("dosage_and_administration")]
    public List<string>? DosageAndAdministration { get; set; }
// --------------------------------------------------------
    [JsonPropertyName("drug_interactions")]
    public List<string>? DrugInteractions { get; set; }
// --------------------------------------------------------
    [JsonPropertyName("contraindications")]
    public List<string>? Contraindications { get; set; }
// --------------------------------------------------------
    [JsonPropertyName("storage_and_handling")]
    public List<string>? StorageAndHandling { get; set; }


    [JsonPropertyName("purpose")]
    public List<string>? Purpose { get; set; }
    public OpenFDA? Openfda { get; set; }
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
    [JsonPropertyName("manufacturer_name")]
    public List<string>? ManufacturerName {  get; set; }
    [JsonPropertyName("product_type")]
    public List<string>? ProductType { get; set; }
}

public static class BlazorListStringHelper
{
    public static string Display(IEnumerable<String>? list, string ifNullString)
    {
        return (list != null && list.Any()) ? string.Join(", ", list) : ifNullString;
    }
}