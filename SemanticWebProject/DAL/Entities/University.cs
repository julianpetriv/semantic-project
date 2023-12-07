using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SemanticWebProject.DAL.Entities;

public class University
{
    [Key] 
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Country { get; set; }
    
    public string EstablishedOn { get; set; }
    
    public string City { get; set; }
    
    public string Abstract { get; set; }
}