// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WordBook.Models;

#nullable disable

namespace WordBook.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220527120754_AddWordBookWithId")]
    partial class AddWordBookWithId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WordBook.Models.Letter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Anotation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Translate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WordBookId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordBookId");

                    b.ToTable("Letter");
                });

            modelBuilder.Entity("WordBook.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("WordBook.Models.WordBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("language")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("WordBook");
                });

            modelBuilder.Entity("WordBook.Models.Letter", b =>
                {
                    b.HasOne("WordBook.Models.WordBook", "WordBook")
                        .WithMany("Letters")
                        .HasForeignKey("WordBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WordBook");
                });

            modelBuilder.Entity("WordBook.Models.WordBook", b =>
                {
                    b.HasOne("WordBook.Models.Student", "Author")
                        .WithMany("WordBooks")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("WordBook.Models.Student", b =>
                {
                    b.Navigation("WordBooks");
                });

            modelBuilder.Entity("WordBook.Models.WordBook", b =>
                {
                    b.Navigation("Letters");
                });
#pragma warning restore 612, 618
        }
    }
}
