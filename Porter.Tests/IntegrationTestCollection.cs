using Porter.Tests;
using Xunit;

// 1. Define o nome da coleção
[CollectionDefinition("Integration Test Collection")]
// 2. Associa a coleção com a sua CustomWebApplicationFactory
public class IntegrationTestCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>
{
    // Esta classe não precisa de código, ela é apenas um ponto de referência.
}