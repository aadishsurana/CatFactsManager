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
            _InputFile = @"./FactsText1.json";
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

        [Fact]
        public async Task CheckIfUserCollection_ProcessedSuccessfully_WhenUniqueUsersProvided()
        {
            //Arrange
            _sut = new CatFactService();

            //Act
            var result = await _sut.ReadAllFacts(@"./FactsText2.json");

            //Assert
            Assert.True(result.Count == 4);
            Assert.True(result["Tom Brady"] == 3);
            Assert.True(result["Josh Wells"] == 11);
            Assert.True(result["Alex Cappa"] == 5);
            Assert.True(result["David Jones"] == 1);
        }

        [Fact]
        public async Task CheckIfUserCollection_SortedInDescendingOrder_ByValue()
        {
            //Arrange
            _sut = new CatFactService();

            //Act
            var result = await _sut.ReadAllFacts(_InputFile);

            //Assert
            Assert.Equal("Josh Wells", result.FirstOrDefault().Key);
            Assert.True(result.FirstOrDefault().Value == 11);

            Assert.Equal("Alex Cappa", result.LastOrDefault().Key);
            Assert.True(result.LastOrDefault().Value == 7);
        }

        [Fact]
        public async Task WhenFileNotFound_NullIsReturned()
        {
            //Arrange
            _sut = new CatFactService();

            //Act
            var result = await _sut.ReadAllFacts("text.json");

            //Assert
           Assert.Null(result);
        }
    }
}