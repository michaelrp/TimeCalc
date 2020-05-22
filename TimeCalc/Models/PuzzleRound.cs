namespace TimeCalc.Models
{
    public class PuzzleRound
    {
        public string Id { get; set; }
        public string Puzzle { get; set; }
        public string Name { get; set; }
        public Solve[] Solves { get; set; }
    }
}