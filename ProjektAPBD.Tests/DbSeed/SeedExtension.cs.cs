﻿using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.Tests.DbSeed
{
    public static class SeedExtension
    {
        public static ManagementDbContext TestSeed(this ManagementDbContext context) => context
            .SeedCompanies()
            .SeedPhysicalPersons()
            .SeedProducts()
            .SeedDiscounts()
            .SeedSales()
            .SeedSalesPayments();

        private static ManagementDbContext SeedCompanies(this ManagementDbContext context)
        {
            if (context.CompanyClients.Any(c => c.IdClient == 1))
                return context;

            List<Company> entities = new() {
                new Company() { IdClient = 1, Address = "Koszykowa 24", Email = "evil@corp.com", Phone = "666-333-111", Name = "EvilCorp", KrsNumber = "0000772427" },
                new Company() { IdClient = 2, Address = "Instalatorów 9/24C", Email = "instpol@gmail.com", Phone = "738-091-364", Name = "InstPol", KrsNumber = "0000766811" },
                new Company() { IdClient = 3, Address = "Banacha 50A", Email = "plytex@company.pl", Phone = "(22)824-79-21", Name = "PłyteX", KrsNumber = "0000724592" }
            };

            context.CompanyClients.AddRange(entities);
            context.SaveChanges();
            return context;
        }

        private static ManagementDbContext SeedPhysicalPersons(this ManagementDbContext context)
        {
            if (context.PersonClients.Any(c => c.IdClient == 4))
                return context;

            List<PhysicalPerson> entities = new() {
                new PhysicalPerson() { IdClient = 4, Address = "Duracza 76/21", Email = "amichalecki@gmail.com", Phone = "726-487-621", Pesel = "89050729992", FirstName = "Andrzej", LastName = "Michalecki" },
                new PhysicalPerson() { IdClient = 5, Address = "Duracza 76/21", Email = "asia94@gmail.com", Phone = "669-910-436", Pesel = "94070507007", FirstName = "Joanna", LastName = "Michalecka" },
                new PhysicalPerson() { IdClient = 6, Address = "Partyzantki 12C", Email = "plytex@company.pl", Phone = "(+48)842-279-216", Pesel = "08280732893", FirstName = "Anna", LastName = "Łącka" }
            };

            context.PersonClients.AddRange(entities);
            context.SaveChanges();
            return context;
        }

        private static ManagementDbContext SeedProducts(this ManagementDbContext context)
        {
            if (context.Products.Any(c => c.IdSoftwareProduct == 1))
                return context;

            List<SoftwareProduct> entities = new() {
                new SoftwareProduct() { IdSoftwareProduct = 1, Name = "SuperSoft", Category = "Rozrywka", Description = "Super aplikacja do rozrywki", ActualVersion = "2.1", ActualVersionReleaseDate = DateTime.Now.AddDays(-100) },
                new SoftwareProduct() { IdSoftwareProduct = 2, Name = "GamexPro", Category = "Design gier", Description = "Interaktywny progam do tworenia gier", ActualVersion = "7.8", ActualVersionReleaseDate = DateTime.Now.AddDays(-20) },
                new SoftwareProduct() { IdSoftwareProduct = 3, Name = "Alerter", Category = "Rozwój osobisty", Description = "Inteligentny organizer, ktory zawsze przypomni o najważniejszych wydarzeniach", ActualVersion = "1.0", ActualVersionReleaseDate = DateTime.Now.AddDays(-65) }
            };

            context.Products.AddRange(entities);
            context.SaveChanges();
            return context;
        }

        private static ManagementDbContext SeedDiscounts(this ManagementDbContext context)
        {
            if (context.Discounts.Any(c => c.IdDiscount == 1))
                return context;

            List<Discount> entities = new() {
                new Discount() { IdDiscount = 1, DateFrom = DateTime.Now.AddDays(-20), DateTo = DateTime.Now.AddDays(40), IdSoftwareProduct = 1, PercentageValue = 20 },
                new Discount() { IdDiscount = 2, DateFrom = DateTime.Now.AddDays(-10), DateTo = DateTime.Now.AddDays(50), IdSoftwareProduct = 1, PercentageValue = 25 },
                new Discount() { IdDiscount = 3, DateFrom = DateTime.Now.AddDays(-20), DateTo = DateTime.Now.AddDays(-10), IdSoftwareProduct = 2, PercentageValue = 50 },
                new Discount() { IdDiscount = 4, DateFrom = DateTime.Now.AddDays(-1), DateTo = DateTime.Now.AddDays(30), IdSoftwareProduct = 2, PercentageValue = 20 }
            };

            context.Discounts.AddRange(entities);
            context.SaveChanges();
            return context;
        }

        private static ManagementDbContext SeedSales(this ManagementDbContext context)
        {
            if (context.Sales.Any(c => c.IdContract == 1))
                return context;

            List<Sale> entities = new() {
                new Sale { IdContract = 1, IdSoftwareProduct = 1, IdClient = 1, CreationDate = DateTime.Now.AddDays(-5), ExpirationDate = DateTime.Now.AddDays(10), SupportYearsAmount = 3, Price = 2000 },
                new Sale { IdContract = 2, IdSoftwareProduct = 1, IdClient = 2, CreationDate = DateTime.Now.AddDays(5), ExpirationDate = DateTime.Now.AddDays(30), SupportYearsAmount = 3, Price = 2000 },
                new Sale { IdContract = 3, IdSoftwareProduct = 1, IdClient = 2, CreationDate = DateTime.Now.AddDays(-50), ExpirationDate = DateTime.Now.AddDays(-30), SupportYearsAmount = 3, Price = 2000 },
                new Sale { IdContract = 4, IdSoftwareProduct = 2, IdClient = 4, CreationDate = DateTime.Now.AddDays(-1), ExpirationDate = DateTime.Now.AddDays(10), SupportYearsAmount = 4, Price = 10000 },
            };

            context.Sales.AddRange(entities);
            context.SaveChanges();
            return context;
        }

        private static ManagementDbContext SeedSalesPayments(this ManagementDbContext context)
        {
            if (context.Payments.Any(c => c.IdPayment == 1))
                return context;

            List<Payment> entities = new() {
                new Payment { IdPayment = 1, IdContract = 1, IdClient = 1, Date = DateTime.Now.AddDays(-3), Value = 500 },
                new Payment { IdPayment = 2, IdContract = 1, IdClient = 1, Date = DateTime.Now.AddDays(-2), Value = 500 },
                new Payment { IdPayment = 3, IdContract = 1, IdClient = 1, Date = DateTime.Now.AddDays(-1), Value = 500 },
                new Payment { IdPayment = 4, IdContract = 2, IdClient = 2, Date = DateTime.Now.AddDays(-40), Value = 2000 },
                new Payment { IdPayment = 5, IdContract = 4, IdClient = 4, Date = DateTime.Now.AddHours(-5), Value = 5000 },
                new Payment { IdPayment = 6, IdContract = 4, IdClient = 4, Date = DateTime.Now, Value = 5000 },
            };

            context.Payments.AddRange(entities);
            context.SaveChanges();
            return context;
        }
    }
}
