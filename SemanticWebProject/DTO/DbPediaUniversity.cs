using Newtonsoft.Json;

namespace SemanticWebProject.DTO;

public class DbPediaUniversity
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("country")]
    public string Country { get; set; }
    
    [JsonProperty("establishedOn")]
    public string EstablishedOn { get; set; }
    
    [JsonProperty("city")]
    public string City { get; set; }
    
    [JsonProperty("abstract")]
    public string Abstract { get; set; }
}