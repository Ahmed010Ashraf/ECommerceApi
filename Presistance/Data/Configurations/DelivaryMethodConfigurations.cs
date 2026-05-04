using Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Data.Configurations
{
    public class DelivaryMethodConfigurations : IEntityTypeConfiguration<DelivaryMethod>
    {
        public void Configure(EntityTypeBuilder<DelivaryMethod> builder)
        {
            builder.ToTable("DelivaryMethods");
            builder.Property(d=>d.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(d => d.ShortName)
                .HasColumnType("varchar(20)");

            builder.Property(d => d.DeliveryTime)
                .HasColumnType("varchar(50)");


            builder.Property(d => d.Description)
                .HasColumnType("varchar(200)");
        }
    }
}
