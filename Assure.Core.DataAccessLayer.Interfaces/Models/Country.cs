namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }

        public override string ToString()
        {
            return $"{CountryId}; {Alpha2}; {Name}";
        }
    }
}
