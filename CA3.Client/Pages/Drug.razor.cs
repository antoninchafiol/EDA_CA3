using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CA3.Client.Pages;
public class DrugService
{
    private readonly HttpClient _httpClient = new();

    public DrugService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FullDrug>> GetDrugsAsync()
    {
        string url = "https://api.fda.gov/drug/label.json?search=_exists_:openfda.route&limit=10";

        try
        {
            var response = await _httpClient.GetFromJsonAsync<DrugListResponse>(url);

            if (response?.Results != null)
            {
                return response.Results.ToList();
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
// API Response Models
public class DrugListResponse
{
    public List<FullDrug>? Results { get; set; }
}

public class FullDrug
{
    public string? EffectiveTime { get; set; }
    public List<string>? InactiveIngredient { get; set; }
    public List<string>? Purpose { get; set; }
    public List<string>? KeepOutOfReachOfChildren { get; set; }
    public List<string>? Warnings { get; set; }
    public List<string>? Questions { get; set; }
    public List<string>? SplProductDataElements { get; set; }
    public OpenFDA? Openfda { get; set; }
    public string? SetId { get; set; }
    public string? Id { get; set; }
    public string? Version { get; set; }
}

public class OpenFDA
{
    [JsonPropertyName("application_number")]
    public List<string>? ApplicationNumber { get; set; }
    [JsonPropertyName("brand_name")]
    public List<string>? BrandName { get; set; }
    [JsonPropertyName("generic_name")]
    public List<string>? GenericName { get; set; }
}

public class PaginationState
{
    public int CurrentPage { get; private set; } = 1;
    public int PageSize { get; private set; }

    public PaginationState(int pageSize)
    {
        PageSize = pageSize;
    }

    public int Skip => (CurrentPage - 1) * PageSize;

    public void NextPage()
    {
        CurrentPage++;
    }

    public void PreviousPage()
    {
        if (CurrentPage > 1)
            CurrentPage--;
    }

    public void GoToPage(int page)
    {
        if (page > 0)
            CurrentPage = page;
    }
}
