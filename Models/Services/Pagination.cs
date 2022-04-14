namespace KIT.Models.Services;

public class Pagination
{
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalNumberOfItems { get; set; }
}