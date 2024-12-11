using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Interfaces;
using Model;

namespace AppServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all departments
        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            var departments = await _httpClient.GetFromJsonAsync<List<Department>>("api/department");
            return departments ?? new List<Department>();
        }

        // Fetch a single department by ID
        public async Task<Department?> GetDepartmentById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Department>($"api/department/{id}");
        }

        // Create a new department
        public async Task<Department> Create(Department department)
        {
            var response = await _httpClient.PostAsJsonAsync("api/department", department);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Department>();
        }

        // Update an existing department
        public async Task<Department?> Update(Department department)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/department/{department.Id}", department);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Department>();
        }

        // Remove a department
        public async Task<Department?> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/department/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Department>();
        }
    }
}
