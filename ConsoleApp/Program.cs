using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

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
            //InsertBattle();
            //QueryAndUpdateBattle_Disconnected();
            //InsertNewSamuraiWithAQuote();
            //InsertNewSamuraiWithManyQuotes();
            //AddQuoteToExistingSamuraiWhileTracked();
            //AddQuoteToExistingSamuraiNotTracked();
            //AddQuoteToExistingSamuraiNotTracked_Easy();
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //ProjectSamuraiWithQuotes();
            //ExplicitLoadQuotes();
            //LazyLoadingQuotes();
            //FilteringWithRelatedData();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();
            //JoinBattleAndSamurai();
            //EnlistSamuraiIntoBattle();
            //RemoveJoinBetweenSamuraiAndBattleSimple();
            GetSamuraiWithBattles();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattle = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(samurai => samurai.Id == 2);
        }

        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 2 };
            _context.Remove(join);
            _context.SaveChanges();
        }

        private static void EnlistSamuraiIntoBattle()
        {
            var battle = _context.Battles.Find(1);
            battle.SamuraiBattles
                .Add(new SamuraiBattle { SamuraiId = 21 });
            _context.SaveChanges();
        }

        private static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle { SamuraiId = 1, BattleId = 3 };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            var quote = samurai.Quotes[0];
            quote.Text += " Did you hear that again?";
            using (var newContext = new SamuraiContext())
            {
                //newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 1);
            samurai.Quotes[0].Text = " Did you hear that?";
            _context.Quotes.Remove(samurai.Quotes[0]);
            _context.SaveChanges();
        }

        private static void FilteringWithRelatedData()
        {
            var samurais = _context.Samurais
                                    .Where(s => s.Quotes.Any(Queryable => Queryable.Text.Contains("happy")))
                                    .ToList();
        }

        private static void LazyLoadingQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(samurai => samurai.Name.Contains("Sampson"));

            var quoteCount = samurai.Quotes.Count();
        }

        private static void ExplicitLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(samurai => samurai.Name.Contains("Sampson"));
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.Horse).Load();
        }

        private static void ProjectSamuraiWithQuotes()
        {
            //var somePropertiesWithQuotes = _context.Samurais.Select(s => new { s.Id, s.Name, s.Quotes }).ToList();
            //var somePropertiesWithQutoes = _context.Samurais
            //    .Select(s => new { s.Id, s.Name,
            //        HappyQuotes = s.Quotes.Where(q => q.Text.Contains("Happy"))})
            //    .ToList();
            //var somePropertiesWithQutoes = _context.Samurais
            //    .Select(s => new {s.Id, s.Name,
            //        HappyQuotes = s.Quotes.Where(q => q.Text.Contains("Happy")) })
            //    .ToList();
            var samuraisWithHappyQuotes = _context.Samurais
                .Select(s => new
                {
                    Samurai = s,
                    HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy"))
                })
                .ToList();
        }

        private static void ProjectSomeProperties()
        {
            //var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
            //var idsAndNames = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }

        public struct IdAndName
        {
            public IdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id;
            public string Name;
        }



        private static void EagerLoadSamuraiWithQuotes()
        {
            //var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
            //var samuraiWithQuotes = _context.Samurais.Where(s => s.Name.Contains("Julie"))
            //                                            .Include(s => s.Quotes).ToList();
            var samuraiWithQuotes = _context.Samurais.Where(s => s.Name.Contains("Julie"))
                                            .Include(s => s.Quotes).ToList().FirstOrDefault();
        }

        private static void AddQuoteToExistingSamuraiNotTracked_Easy(int samuraiId)
        {
            var quote = new Quote
            {
                Text = "Now that I saved you, will you feed me Dinner again?",
                SamuraiId = samuraiId
            };
            using (var newContext = new SamuraiContext())
            {
                newContext.Quotes.Add(quote);
                newContext.SaveChanges();
            }
        }

        private static void AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?"
            });
            using (var newContext = new SamuraiContext())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        }

        private static void AddQuoteToExistingSamuraiWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that i saved you!"
            });
            _context.SaveChanges();
        }

        private static void InsertNewSamuraiWithManyQuotes()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "Watch out for my sharp sword!"},
                    new Quote { Text = "I told you to watch out for my sharp sword! Oh well!!"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "I've come to save you" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using(var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            _context.SaveChanges();
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
