using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        // Database context variable
        private static SamuraiContext context = new SamuraiContext();

        static void Main(string[] args)
        {
            //GetSamurai("Before Add: ");
            //AddSamurai();
            GetSamurai("After Add: ");
            InsertMultipleSamurais();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        // Inserts multiple samurais via the AddRange method
        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Luffytaro" };
            var samurai2 = new Samurai { Name = "Raizo" };
            var samurai3 = new Samurai { Name = "Yasui" };
            var samurai4 = new Samurai { Name = "Yasuo" };
            context.Samurais.AddRange(samurai, samurai2, samurai3, samurai4);

            context.SaveChanges();
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
            var samurai = new Samurai { Name = "Kinemon" };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }
    }
}
