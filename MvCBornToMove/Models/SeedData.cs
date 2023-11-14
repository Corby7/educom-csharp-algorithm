using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvCBornToMove.Data;

namespace MvCBornToMove.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvCBornToMoveContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvCBornToMoveContext>>()))
            {
                // Look for any movies.
                if (context.Move.Any())
                {
                    return;   // DB has been seeded already.
                }
                context.Move.AddRange(
                    new Move
                    {
                        Name = "Push Up",
                        Description = "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes",
                        SweatRate = 3
                    },
                    new Move
                    {
                        Name = "Planking",
                        Description = "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast",
                        SweatRate = 3
                    },
                    new Move
                    {
                        Name = "Squat",
                        Description = "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes",
                        SweatRate = 5
                    }
                );
                context.SaveChanges();
            }
        }
    }
}