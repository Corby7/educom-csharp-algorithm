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
                        Description = "Lie horizontally on tiptoes and hands. Slowly lower the body until the nose almost touches the ground. Push the body back up until the elbows are almost straight. Then lower it again. Repeat this 20 times without pauses.",
                        SweatRate = 3
                    },
                    new Move
                    {
                        Name = "Planking",
                        Description = "Lie horizontally on tiptoes and forearms. Hold this position for 1 minute.",
                        SweatRate = 3
                    },
                    new Move
                    {
                        Name = "Squat",
                        Description = "Stand with arms extended. Bend your knees until your buttocks almost touch the ground. Stand up fully again. Repeat this 20 times without pauses.",
                        SweatRate = 5
                    }
                );
                context.SaveChanges();
            }
        }
    }
}