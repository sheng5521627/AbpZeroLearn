using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpZeroLearn
{
    public class MyDbContextDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionBuilder.UseMySql("server=127.0.0.1;database=lpw-test;uid=root;pwd=123456");
            return new MyDbContext(optionBuilder.Options);
        }
    }
}
