using Model;
using Interfaces;
namespace AppServices;

public class ActivitiesService: ITransportService, IStairsService,IVolunteeringService,ISavingFoodService,ICarPoolService{
    private readonly HttpClient _httpClient;

    public ActivitiesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<Transport>> GetAllTransport()
    {
        return await _httpClient.GetFromJsonAsync<List<Transport>>("api/transport")
                   ?? new List<Transport>();
    }

    public async Task<Transport?> GetTransportById(int id)
    {
        return await _httpClient.GetFromJsonAsync<Transport>($"api/transport/{id}");
    }
    public async Task<Transport> CreateTransport(Transport transport)
    {
        var response = await _httpClient.PostAsJsonAsync("api/transport", transport);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Transport>();
    }
    public async Task<Transport> RemoveTransport(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/transport/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Transport>();
    }
    public async Task<Transport?> UpdateTransport(Transport transport)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/transport/{transport.Id}", transport);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Transport>();
    }
    public async Task<IEnumerable<Stairs>> GetAllStairsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Stairs>>("api/stairs")
                   ?? new List<Stairs>();
    }

    public async Task<Stairs?> GetStairsByIdAsync(int id){
        return await _httpClient.GetFromJsonAsync<Stairs>($"api/stairs/{id}");
    }
    public async Task<Stairs> CreateStairsAsync(Stairs stairs)
    {
        var response = await _httpClient.PostAsJsonAsync("api/stairs", stairs);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Stairs>();
    }
    public async Task<Stairs> RemoveStairsAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/stairs/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Stairs>();
    }
    public async Task<Stairs?> UpdateStairsAsync(Stairs stairs)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/stairs/{stairs.Id}", stairs);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Stairs>();
    }
    public async Task<IEnumerable<Volunteering>> GetAllVolunteeringAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Volunteering>>("api/volunteering")
                   ?? new List<Volunteering>();
    }

    public async Task<Volunteering?> GetVolunteeringByIdAsync(int id){
        return await _httpClient.GetFromJsonAsync<Volunteering>($"api/volunteering/{id}");
    }

    public async Task<Volunteering> CreateVolunteeringAsync(Volunteering volunteering)
    {
        var response = await _httpClient.PostAsJsonAsync("api/volunteering", volunteering);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Volunteering>();
    }

    public async Task<Volunteering> RemoveVolunteeringAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/volunteering/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Volunteering>();
    }

    public async Task<Volunteering?> UpdateVolunteeringAsync(Volunteering volunteering)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/volunteering/{volunteering.Id}", volunteering);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Volunteering>();
    }

    public async Task<IEnumerable<SavingFood>> GetAllSavingFoodAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<SavingFood>>("api/savingfood")
                   ?? new List<SavingFood>();
    }

    public async Task<SavingFood?> GetSavingFoodByIdAsync(int id){
        return await _httpClient.GetFromJsonAsync<SavingFood>($"api/savingfood/{id}");
    }

    public async Task<SavingFood> CreateSavingFoodAsync(SavingFood savingFood)
    {
        var response = await _httpClient.PostAsJsonAsync("api/savingfood", savingFood);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SavingFood>();
    }

    public async Task<SavingFood> RemoveSavingFoodAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/savingfood/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SavingFood>();
    }

    public async Task<SavingFood?> UpdateSavingFoodAsync(SavingFood savingFood)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/savingfood/{savingFood.Id}", savingFood);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SavingFood>();
    }

    public async Task<CarPool> CreateCarPool(CarPool carPool)
    {
        var response = await _httpClient.PostAsJsonAsync("api/carpool", carPool);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CarPool>();
    }

    public async Task<IEnumerable<CarPool>> GetAllCarPool()
    {
        return await _httpClient.GetFromJsonAsync<List<CarPool>>("api/carpool")
                   ?? new List<CarPool>();
    }

    public async Task<CarPool?> GetCarPoolById(int id){
        return await _httpClient.GetFromJsonAsync<CarPool>($"api/carpool/{id}");
    }

    public async Task<CarPool> RemoveCarPool(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/carpool/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CarPool>();
    }

    public async Task<CarPool?> UpdateCarPool(CarPool carPool)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/carpool", carPool);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CarPool>();
    }
}
