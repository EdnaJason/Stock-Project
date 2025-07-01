//using FinShark.api.Models;
using FinShark.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinShark.api.Data
{
    public class ApplicationDBContext : DbContext   //allow us to access/specify which table we want
    //ORM : the classes become tables and the properties in them become the rows
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)     // constructor //base here refers to the DbContext itself (inherited)
        {
            
        }
        public DbSet<Stock> Stocks { get; set; }        //this will create the db for us
        public DbSet<Comment> Comments { get; set; }
    }
}
