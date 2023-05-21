
using GraphQL;
namespace VL.Solar.NotificatieService.Services.GraphQL;

public interface IGraphQLService
{
    Task<GraphQLResponse<T>> ExecuteGraphQLQuery<T>(string query);
    Task ExecuteExampleQuery();
}