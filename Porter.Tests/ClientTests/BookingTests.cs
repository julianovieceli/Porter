using Porter.Application.Commands.Booking;
using Porter.Common;
using Porter.Dto;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace Porter.Tests.ClientTests;

[Collection("Integration Test Collection")]
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


    [Fact]
    public async Task RegisterBooking_ConflictError()
    {
        // ARRANGE (Configurado no construtor e na Factory)
        
        RegisterBookingCommand registerBookingCommand = new RegisterBookingCommand()
        {
            DoctoReservedBy = Constants.Docto,
            StartDate = DateTime.Now.AddMinutes(1),
            EndDate = DateTime.Now.AddMinutes(10),
            Obs = """{"teste": "dfd" }""",

            RoomName = Constants.Sala1
        };

        var serviceParams = JsonSerializer.Serialize(registerBookingCommand);

        using HttpContent serviceRequestContent = new StringContent(serviceParams, Encoding.UTF8, new MediaTypeHeaderValue("application/json"));

        //Act
        var response = await _client.PostAsync("/booking", serviceRequestContent);


        //ASSERT
        response.EnsureSuccessStatusCode(); // Espera 201 Created (ou 200 OK)
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // ASSERT (Verificar o conteúdo)
        //var clients = await response.Content.ReadFromJsonAsync<Result<List<ResponseClientDto>>>();
        var clientsString = await response.Content.ReadAsStringAsync();
        var responseClient = JsonSerializer.Deserialize<Result>(clientsString);
        Assert.False(responseClient.IsFailure);

        ErrorResponseDto responseClientConflict;
        using HttpContent serviceRequestContentLoop = new StringContent(serviceParams, Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
      
            registerBookingCommand = new RegisterBookingCommand()
            {
                DoctoReservedBy = Constants.Docto,
                StartDate = DateTime.Now.AddMinutes(5),
                EndDate = DateTime.Now.AddMinutes(5),
                Obs = "Teste",
                RoomName = Constants.Sala1
            };


            //Act
            var responseConflict = await _client.PostAsync("/booking", serviceRequestContentLoop);

            //ASSERT
            Assert.Equal(HttpStatusCode.BadRequest, responseConflict.StatusCode);

            var clientsStringConflict = await responseConflict.Content.ReadAsStringAsync();

            responseClientConflict = JsonSerializer.Deserialize<ErrorResponseDto>(clientsStringConflict, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.True(responseClientConflict.ErrorCode == "400");
            Assert.True(responseClientConflict.Message.Contains("Ja existe reserva para esta sala neste período"));
        

    }

}