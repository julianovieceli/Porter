using Porter.Common;
using Porter.Dto;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Porter.Tests.ClientTests;

public class ClientControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    // Injeção da Factory no construtor
    public ClientControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetClients_Success()
    {
        // ARRANGE (Configurado no construtor e na Factory)

        // ACT (Fazer a requisição HTTP real)
        var response = await _client.GetAsync("/Client/fetch-all");

        // ASSERT (Verificar o resultado da requisição e da pipeline da API)

        response.EnsureSuccessStatusCode(); // Espera 201 Created (ou 200 OK)
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // ASSERT (Verificar o conteúdo)
        //var clients = await response.Content.ReadFromJsonAsync<Result<List<ResponseClientDto>>>();
        var clientsString = await response.Content.ReadAsStringAsync();

        var responseClient = JsonSerializer.Deserialize<Result>(clientsString);

        Assert.False(responseClient.IsFailure);
    }
}