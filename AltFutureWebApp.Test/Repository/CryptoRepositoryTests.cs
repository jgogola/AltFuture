using AltFutureWebApp.Repository;
using AltFutureWebApp.Data;
using AltFutureWebApp.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AltFutureWebApp.Test.Repository
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

            //Act:
            var result = await cryptoRepository.GetAllAsync();

            //Assertion:
            result.Should().NotBeEmpty()
                .And.BeOfType<List<Crypto>>()
                .And.HaveCount(10);
        }

        [Fact]
        public async void CyrptoRepository_GetByIdAsync_ReturnsCrypto()
        {
            //Arrange:
            var id = 12211;
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

            //Acct:
            var result = await cryptoRepository.GetCountAsync();

            //Assertion:
            result.Should().Be(10);
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

            //Act:
            var result = cryptoRepository.Add(crypto);
            var count = await cryptoRepository.GetCountAsync();


            result.Should().BeTrue();
            count.Should().Be(11);
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
                CryptoName = "Bitcoin",
                TickerSymbol = "BTC"
            };
            var dbContext = await GetDbContext();
            var cryptoRepository = new CryptoRepository(dbContext);

            //Act:
            cryptoRepository.Add(crypto);
            var result = cryptoRepository.Delete(crypto);
            var count = await cryptoRepository.GetCountAsync();


            //Assertion:
            result.Should().BeTrue();
            count.Should().Be(10);
        }


    }
}