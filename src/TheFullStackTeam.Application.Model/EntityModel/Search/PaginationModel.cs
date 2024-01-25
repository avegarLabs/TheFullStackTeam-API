namespace TheFullStackTeam.Application.Model.EntityModel.Search;

/// <summary>Pagination class.</summary>
public class PaginationModel
{
    /// <summary>Page number.</summary>
    public int Page { get; set; }

    /// <summary>Number of items per page.</summary>
    public int ItemsPerPage { get; set; }

    /// <summary>Instantiates new Pagination object.</summary>
    public PaginationModel() : this(1, 10)
    {
    }

    /// <summary>Instantiates new Pagination object.</summary>
    /// <param name="page">Page number.</param>
    public PaginationModel(int page) : this(page, 10)
    {
    }

    /// <summary>Instantiates new Pagination object.</summary>
    /// <param name="page">Page number.</param>
    /// <param name="itemsPerPage">Number of items per page.</param>
    public PaginationModel(int page, int itemsPerPage)
    {
        Page = page;
        ItemsPerPage = itemsPerPage;
    }
}
