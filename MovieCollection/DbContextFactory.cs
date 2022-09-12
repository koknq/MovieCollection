using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer("Data Source=SV-APP-014\\IMOSSQL2016;Initial Catalog=FirstDB;Integrated Security=True;User ID=;Password=");

            return new MyContext(optionsBuilder.Options);
        }
    }
}
