using BornToMove.DAL;
using BornToMove;

public class FillDatabase
{
    public void FillDatabaseMethod()
    {
        using (var context = new MoveContext())
        {
            Move pushUp = new Move
            {
                Name = "Push Up",
                Description = "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes",
                SweatRate = 3
            };

            Move planking = new Move
            {
                Name = "Planking",
                Description = "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast",
                SweatRate = 3
            };

            Move squat = new Move
            {
                Name = "Squat",
                Description = "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes",
                SweatRate = 5
            };

            context.AddRange(pushUp, planking, squat);
            context.SaveChanges(); // This will insert the new records into the database
        }
    }
}
