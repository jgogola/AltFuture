using AltFuture.DataAccessLayer.Data.Enums;
using AltFuture.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AltFuture.DataAccessLayer.Data.Configurations
{
    public class DataImportTypeConfiguration : IEntityTypeConfiguration<DataImportType>
    {
        public void Configure(EntityTypeBuilder<DataImportType> builder)
        {
            builder.HasData
            (
              Enum.GetValues(typeof(DataImportTypeEnum))
                .Cast<DataImportTypeEnum>()
                .Where(e => (int)e != 0)
                .Select(e => 
                {

                    var displayAttribute = e.GetType()
                                            .GetMember(e.ToString())
                                            .First()
                                            .GetCustomAttribute<DisplayAttribute>();

                    string displayName = displayAttribute?.Name ?? e.ToString();

                    return new DataImportType
                    {
                        DataImportTypeId = (int)e,
                        DataImportTypeName = displayName
                    };

           
                })
            );
        }
    }
}
