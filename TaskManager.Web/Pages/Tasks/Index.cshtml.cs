using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Domain.Entities;

namespace TaskManager.Web.Pages.Tasks
{
    public class IndexModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("TaskManagerAPI");

        public List<TaskItem> TaskItems { get; set; } = [];

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TaskItem>>("api/Task");
            if (response != null)
                TaskItems = response;
        }
    }
}
