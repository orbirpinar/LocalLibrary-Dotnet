using Newtonsoft.Json;

namespace WebApp.Dto
{
    public class SearchParamDto
    {
        [JsonProperty("title")] public string Title { get; set; } = default!;
    }
}