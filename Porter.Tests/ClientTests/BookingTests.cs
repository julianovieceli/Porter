using Porter.Common;
using System.Net;
using System.Text.Json;


namespace Porter.Tests.ClientTests;

public class BookingControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    // Injeção da Factory no construtor
    public BookingControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetBoookings_Success()
    {
        // ARRANGE (Configurado no construtor e na Factory)

        // ACT (Fazer a requisição HTTP real)
        var response = await _client.GetAsync("/booking/fetch-all");

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