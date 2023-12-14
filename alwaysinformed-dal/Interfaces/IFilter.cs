namespace alwaysinformed_dal.Interfaces
{
    public interface IFilter<T>
    {
        Task<List<T>> SearchArticles(string? wordFilter);
        Task<List<T>> FilterByCategory(string? categoryName);
        Task<List<T>> FilterByAuthor(string? firstName, string? lastName);
        Task<List<T>> FilterByDate(DateTime? date);
    }
}