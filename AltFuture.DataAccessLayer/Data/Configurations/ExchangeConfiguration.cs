using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AltFuture.DataAccessLayer.Data.Configurations
{
    public class ExchangeConfiguration : IEntityTypeConfiguration<Exchange>
    {
        public void Configure(EntityTypeBuilder<Exchange> builder)
        {
            builder.HasData
            (
              Enum.GetValues(typeof(ExchangeEnum))
                .Cast<ExchangeEnum>()
                .Where(e => (int)e != 0)
                .Select(e =>
                {
                    var displayAttribute = e.GetType()
                                            .GetMember(e.ToString())
                                            .First()
                                            .GetCustomAttribute<DisplayAttribute>();

                    string displayName = displayAttribute?.Name ?? e.ToString();

                    return new Exchange
                    {
                        ExchangeId = (int)e,
                        ExchangeName = displayName
                    };


                })
            );
        }
    }
}
