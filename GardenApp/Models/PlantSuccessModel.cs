namespace GardenApp.Models
{
    public class PlantSuccessModel
    {
        public int Id { get; set; }
        //One to Many with PlantInfoTable
        public int PlantId { get; set; }
        //One to Many with YearTable
        public int YearId { get; set; }
        public DateTime RecommendedTransplantDate { get; set; }
        public DateTime? ActualPlantingDate { get; set; }
        public int RecommendedUpperTemp { get; set; }
        public int RecommendedLowerTemp { get; set; }
        public bool StartedFromSeedOutdoors { get; set; }
        public string? PestsEncountered { get; set; }
        public string? HowToWinterize { get; set; }
        public string? OtherIssues { get; set; }
        public string? IssuesFixes { get; set; }
        public string? Notes { get; set; }
        public bool IsPreferredMethod { get; set; }

    }
}
