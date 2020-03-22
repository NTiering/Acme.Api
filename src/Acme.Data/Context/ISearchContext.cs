using Acme.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Acme.Data.Context
{
    public interface ISearchContext
    {
        DbSet<ProductCategoryDataModel> ProductCategories { get; }
        DbSet<ProductDataModel> Products { get; }
    }
}