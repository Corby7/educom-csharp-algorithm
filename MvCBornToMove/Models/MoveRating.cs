namespace MvCBornToMove.Models
{
    public class MoveRating
    {
        public int Id { get; set; }

        public Move? Move { get; set; }

        public double Rating { get; set; }

        public double Intensity { get; set; }

    }
}

