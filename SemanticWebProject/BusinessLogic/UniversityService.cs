using Microsoft.EntityFrameworkCore;
using SemanticWebProject.DAL;
using SemanticWebProject.DAL.Entities;
using SemanticWebProject.DTO;
using VDS.RDF;
using VDS.RDF.Nodes;
using VDS.RDF.Query;

namespace SemanticWebProject.BusinessLogic;

public class UniversityService
{
    private EFContext _context;
    
    public UniversityService(EFContext context)
    {
        _context = context;
    }
    
    public IEnumerable<DbPediaUniversity> GetUniversitiesFromDbPedia()
    {
        var universities = new List<DbPediaUniversity>();
        
        var endpoint =
            new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");

        string q = @"
            PREFIX dbo: <http://dbpedia.org/ontology/>
            PREFIX dbp: <http://dbpedia.org/property/>
            PREFIX dbr: <http://dbpedia.org/resource/>

            SELECT DISTINCT 
                SAMPLE(?name) AS ?name 
                SAMPLE(?countryName) AS ?country
                SAMPLE(?establishedOn) AS ?establishedOn
                SAMPLE(?cityName) AS ?city
                SAMPLE(?abstract) AS ?abstract
                WHERE {
                    ?university dbo:affiliation dbr:International_Association_of_Universities.
                    ?university dbp:country ?country.
                    ?country dbp:commonName ?countryName.
                    ?university dbp:established ?establishedOn.
                    ?university dbo:city ?city.
                    ?city dbp:name ?cityName.
                    ?university dbo:abstract ?abstract.
                    ?university dbp:name ?name.

                    FILTER(LANG(?abstract) = 'en')
            } 
            ORDER BY ?university
        ";
        
        SparqlResultSet results = endpoint.QueryWithResultSet(q);

        foreach (var result in results)
        {
            universities.Add(new DbPediaUniversity()
            {
                Name = ((ILiteralNode)result["name"]).Value,
                City = ((ILiteralNode)result["city"]).Value,
                Country = ((ILiteralNode)result["country"]).Value,
                EstablishedOn = ((ILiteralNode)result["establishedOn"]).Value,
                Abstract = ((ILiteralNode)result["abstract"]).Value
            });
        }

        return universities;
    }

    public async Task SaveUniversitiesToDbAsync(IEnumerable<DbPediaUniversity> universities)
    {
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Universities\"");

        var universitiesToAdd = universities.Select(u => new University()
        {
            Name = u.Name,
            Country = u.Country,
            City = u.City,
            Abstract = u.Abstract,
            EstablishedOn = u.EstablishedOn
        });
        
       _context.Universities.AddRange(universitiesToAdd);
       await _context.SaveChangesAsync();
       ;
    }

    public async Task<IEnumerable<University>> GetAllUniversitiesAsync()
    {
        return await _context.Universities.ToListAsync();
    }

    public async Task DeleteUniversitiesAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Universities\"");
    }
}