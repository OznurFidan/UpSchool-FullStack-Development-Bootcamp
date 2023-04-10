using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations.Application
{
    public class NoteCategoryConfiguration : IEntityTypeConfiguration<NoteCategory>
    {
        public void Configure(EntityTypeBuilder<NoteCategory> builder)
        {

            //ID
            builder.HasKey(x => new { x.NoteId, x.CategoryId });


            //Reletionships
            builder.HasOne<Note>(x => x.Note)
                 .WithMany(x => x.NoteCategories)
                 .HasForeignKey(x => x.NoteId);

            builder.HasOne<Category>(x => x.Category)
                .WithMany(x => x.NoteCategories)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
