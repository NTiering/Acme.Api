using Acme.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Acme.Data.Context
{
    public interface ISearchContext
    {
        DbSet<ProductCategoryDataModel> ProductCategories { get; }
        DbSet<ProductDataModel> Products { get; }
        DbSet<ProductReviewDataModel> ProductReviews { get; }
    }
}