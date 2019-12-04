namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Currency
    {
        public string Alpha3 { get; set; }
        public string Name { get; set; }
        
        public override string ToString()
        {
            return $"{Alpha3}; {Name}";
        }
    }
}
