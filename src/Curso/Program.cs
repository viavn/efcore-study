using System;
using System.Linq;
using CursoEFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();
            // Dangerous executing on prod
            // db.Database.Migrate();
            var hasPendingMigration = db.Database.GetPendingMigrations().Any();
            if (hasPendingMigration)
            {
                // logic..
            }

            Console.WriteLine("Hello World!");
        }
    }
}
