using Microsoft.EntityFrameworkCore;
using SpendingTrackerAPI.Entities;

namespace SpendingTrackerAPI.Database;

public static class DatabaseSeed
{
    /// <summary>
    /// Seed database on start 
    /// </summary>
    /// <param name="app"></param>
    public static void Seed(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        void SeedData(AppDbContext context)
        {
            context.Database.Migrate();
            if (!context.ExpenseCategories.Any())
            {
                context.ExpenseCategories.AddRange(
                    new ExpenseCategory()
                    {
                        Name = "Flat"
                    },
                    new ExpenseCategory()
                    {
                        Name = "Car"
                    }
                );
                context.SaveChanges();
            }

            if (!context.IncomeCategories.Any())
            {
                context.IncomeCategories.AddRange(
                    new IncomeCategory()
                    {
                        Name = "Work"
                    },
                    new IncomeCategory()
                    {
                        Name = "Renting"
                    }
                );
                context.SaveChanges();
            }
        }
    }
    
}