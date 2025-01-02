namespace GardenApp.Models
{
    public class PlantInfoModel
    {
        //One to Many with PlantStartTable
        //One to Many with PlantSuccessTable
        public int Id { get; set; }
        public string? CommonName { get; set; }
        public string? LatinName { get; set; }
        public PlantLifeCycle LifeCycle { get; set; }

        public enum PlantLifeCycle
        {
            Annual,
            Biennial,
            Perennial
        }
    }
}
