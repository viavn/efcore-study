using System;
using CursoEFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dangerous executing on prod
            // using var db = new ApplicationContext();
            // db.Database.Migrate();

            Console.WriteLine("Hello World!");
        }
    }
}
