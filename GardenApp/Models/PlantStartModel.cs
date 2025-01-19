using System.Diagnostics.Eventing.Reader;

namespace GardenApp.Models
{
    public class PlantStartModel
    {
        public int Id { get; set; }
        //One to Many with PlantInfoTable
        public int PlantInfoId { get; set; }
        public DateTime RecommendedIndoorStartDate { get; set; }
        public DateTime ActualIndoorStartDate { get; set; }
        public string? SeedlingEnvironment { get; set; }
        public string? GerminationRate { get; set; }
        public string? Issues { get; set; }
        public string? IssuesFixes { get; set; }
        public bool? IsPreferredMethod { get; set; }
        //Recommended planting dates from https://www.almanac.com/gardening/planting-calendar/ if API not available, possibly webScrape?
        //Other info on days to maturity chrome-extension://efaidnbmnnnibpcajpcglclefindmkaj/https://cms3.revize.com/revize/clearwaternew/Planning%20an%20Idaho%20Vegetable%20Garden%20Manual.pdf
        //Perenual api has awesome info on plants
    }
}
