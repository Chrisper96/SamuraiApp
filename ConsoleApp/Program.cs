using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        private static SamuraiContext context = new SamuraiContext();
        static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            GetSamurai("Before Add: ");
            AddSamurai();
            GetSamurai("After Add: ");
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void GetSamurai(string text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{ text }: Samurai count is { samurais.Count }");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Sampson" };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }
    }
}
