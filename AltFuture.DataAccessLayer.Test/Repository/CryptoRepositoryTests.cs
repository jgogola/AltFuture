using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Repository;
using AltFuture.DataAccessLayer.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AltFuture.WebApp.Test.Repository
{
    public class CryptoRepositoryTests
    {

        private async Task<AppDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (!await databaseContext.Cryptos.AnyAsync())
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Cryptos.Add(
                      new Crypto()
                      {
                          CryptoName = "Bitcoin",
                          TickerSymbol = "BTC"
                      });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }


        [Fact]
        public async void CyrptoRepository_GetAllAsync_ReturnsListOfCrypto()
        {
            //Arrange:
            var dbContext = await GetDbContext();
            var cryptoRepository = new CryptoRepository(dbContext);
            var origCount = dbContext.Cryptos.Count();

            //Act:
            var result = await cryptoRepository.GetAllAsync();

            //Assertion:
            result.Should().NotBeEmpty()
                .And.BeOfType<List<Crypto>>()
                .And.HaveCount(origCount);
        }

        [Fact]
        public async void CyrptoRepository_GetByIdAsync_ReturnsCrypto()
        {
            //Arrange:
            var id = 1;
            var dbContext = await GetDbContext();
            var cryptoRepository = new CryptoRepository(dbContext);

            //Act:
            var result = cryptoRepository.GetByIdAsync(id);

            //Assert:
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Crypto>>();
        }


        [Fact]
        public async void CryptoRepository_GetCountAsnyc_ReturnsInt()
        {
            //Arrange:
            var dbContext = await GetDbContext();
            var cryptoRepository = new CryptoRepository(dbContext);
            var origCount = dbContext.Cryptos.Count();

            //Acct:
            var result = await cryptoRepository.GetCountAsync();

            //Assertion:
            result.Should().Be(origCount);
        }

        [Fact]
        public async void CryptoRepository_Add_ReturnsTrue()
        {
            //Arrange:
            var crypto = new Crypto()
            {
                CryptoName = "Cardano",
                TickerSymbol = "ADA"
            };
            var dbContext = await GetDbContext();
            var cryptoRepository = new CryptoRepository(dbContext);
            var origCount = dbContext.Cryptos.Count();

            //Act:
            var result = cryptoRepository.Add(crypto);
            var newCount = dbContext.Cryptos.Count();


            result.Should().BeTrue();
            newCount.Should().Be(origCount+1);
        }

        //[Fact]
        //public async void CryyptoRepository_Update_ReturnsTrue()
        //{
        //    //Arrange:
        //    var id = 1;
        //    var crypto = new Crypto()
        //    {
        //        CryptoId = id,
        //        CryptoName = "Bitcoin",
        //        TickerSymbol = "XXX"
        //    };

        //    var dbContext = await GetDbContext();
        //    var cryptoRepository = new CryptoRepository(dbContext);

        //    //Act:
        //    var result = cryptoRepository.Update(crypto);
        //    var updatedCrypto = await cryptoRepository.GetAsync(id);

        //    //Assertion:
        //    result.Should().BeTrue();
        //    updatedCrypto.Should().BeEquivalentTo(crypto);
        //}

        [Fact]
        public async void CryptoRepository_Delete_ReturnsTrue()
        {
            //Arrange:
            var crypto = new Crypto()
            {
                CryptoName = "TestCoin",
                TickerSymbol = "TCX"
            };
            var dbContext = await GetDbContext();
            var cryptoRepository = new CryptoRepository(dbContext);
            var origCount = dbContext.Cryptos.Count();

            //Act:
            cryptoRepository.Add(crypto); // Add TestCoin
            var result = cryptoRepository.Delete(crypto); //Remove TestCoin
            var newCount = dbContext.Cryptos.Count();


            //Assertion:
            result.Should().BeTrue();
            newCount.Should().Be(origCount);
        }


    }
}