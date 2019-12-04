namespace Assure.Core.DataAccessLayer.Interfaces
{
    public interface IPageSortValidator
    {
        void ValidateSortByProperty<T>(string sortBy);
    }
}
