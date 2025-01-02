using System.Diagnostics.Eventing.Reader;

namespace GardenApp.Models
{
    public class AnnualPlantStarterModel
    {
        public int Id { get; set; }
        //One to Many with PlantInfoTable
        public int PlantInfoId { get; set; }
        //One to Many with YearTable
        public int YearId { get; set; } 
        public DateTime RecommendedStartDate { get; set; }
        public DateTime ActualStartDate { get; set; }
        public string? SeedlingEnvironment { get; set; }
        public string? GerminationRate { get; set; }
        public string? Issues { get; set; }
        public string? IssuesFixes { get; set; }
        public bool? IsPreferredMethod { get; set; }

    }
}
