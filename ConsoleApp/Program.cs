using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        // Database context variable
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            //GetSamurai("Before Add: ");
            //AddSamurai();
            //GetSamurai("After Add: ");
            //GetSamuraisSimpler();
            //InsertVariousTypes();
            //InsertMultipleSamurais();
            //QueryFilters();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurai();
            //RetrieveAndDeleteASamurai();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void RetrieveAndDeleteASamurai()
        {
            var samurai = _context.Samurais.Find(18);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurai()
        {
            var samurais = _context.Samurais.Skip(1).Take(3).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.Samurais.Add(new Samurai { Name = "Kikuchiyo" });
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void QueryFilters()
        {
            var name = "Sampson";
            var samurais = _context.Samurais.Where(s => s.Name == name).ToList();
            //var samurais = _context.Samurais.Where(s => s.Name == name).FirstOrDefault();
            //var samurais = _context.Samurais.Where(s => s.Id == 2).FirstOrDefault();
            //var samurai = _context.Samurais.Find(2);
            var last = _context.Samurais.OrderBy(s => s.Id).LastOrDefault(s => s.Name == name);
            //var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "J%")).ToList();
        }

        private static void GetSamuraisSimpler()
        {
            //var samurais = context.Samurais.ToList();
            var query = _context.Samurais;
            //var samurais = query.ToList();
            foreach (var samurai in query)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void InsertVariousTypes()
        {
            var samurai = new Samurai { Name = "Kikuchio" };
            var clan = new Clan { ClanName = "Imperial Clan" };
            _context.AddRange(samurai, clan);
            _context.SaveChanges();
        }

        // Inserts multiple samurais via the AddRange method
        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Luffytaro" };
            var samurai2 = new Samurai { Name = "Raizo" };
            var samurai3 = new Samurai { Name = "Yasui" };
            var samurai4 = new Samurai { Name = "Yasuo" };
            _context.Samurais.AddRange(samurai, samurai2, samurai3, samurai4);
            _context.SaveChanges();
        }

        private static void GetSamurai(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{ text }: Samurai count is { samurais.Count }");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Kinemon" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
    }
}
