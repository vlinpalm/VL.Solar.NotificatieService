using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Threading.Tasks;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Services.GraphQL;

public class GraphQLService : IGraphQLService
{
    public async Task<GraphQLResponse<T>> ExecuteGraphQLQuery<T>(string query)
    {
        var graphQLClient = new GraphQLHttpClient("https://example.com/graphql", new NewtonsoftJsonSerializer());

        var request = new GraphQLRequest
        {
            Query = query
        };

        var response = await graphQLClient.SendQueryAsync<T>(request);

        if (response.Errors != null)
        {
            // Handle any errors returned by the server
            throw new Exception(response.Errors[0].Message);
        }

        return response;
    }

    public async Task ExecuteExampleQuery()
    {
        var query = @"
            query {
                user(id: 123) {
                    id
                    name
                }
            }";

        var result = await ExecuteGraphQLQuery<Notificatie>(query);
        Console.WriteLine($"Notificatie: {result.Data.NotificatieId}, Name: {result.Data.TeamNaam}");
    }
}

// Example usage
// IGraphQLService service = new GraphQLService();
// await service.ExecuteExampleQuery();
