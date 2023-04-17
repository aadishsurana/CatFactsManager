using AutoFixture;
using AutoFixture.AutoMoq;
using CatFacts.Services;
using Moq;
using System.Numerics;

namespace CatFacts.Tests
{
    public class CatFactServiceTests
    {
        private readonly IFixture _fixture;
        private ICatFactService _sut;
        private readonly string _InputFile;

        public CatFactServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _InputFile = @"./FactsText.json";
        }

        [Fact]
        public async Task CheckIfUserCollection_WithCorrectUpVotes_IsReturned()
        {
            //Arrange
            _sut = new CatFactService();

            //Act
            var result = await _sut.ReadAllFacts(_InputFile);

            //Assert
            Assert.True(result.Count == 3);
            Assert.True(result["Kasimir Schulz"] == 8);
            Assert.True(result["Josh Wells"] == 11);
            Assert.True(result["Alex Cappa"] == 7);
        }
    }
}