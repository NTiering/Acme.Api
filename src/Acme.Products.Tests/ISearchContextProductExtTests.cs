using Acme.Data.Context;
using Acme.Data.DataModels;
using Acme.Products.Search;
using Acme.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Acme.Products.Tests
{
    public class ISearchContextProductExtTests
    {
        private readonly Guid _catogoryId1;
        private readonly Guid _catogoryId2;
        private readonly DateTime _createdDate;
        private readonly DbSet<ProductCategoryDataModel> _productCategoryDataSet;
        private readonly DbSet<ProductDataModel> _productsDataSet;
        private readonly Mock<ISearchContext> _searchContext;

        public ISearchContextProductExtTests()
        {
            _searchContext = new Mock<ISearchContext>();
            _catogoryId1 = Guid.NewGuid();
            _catogoryId2 = Guid.NewGuid();
            _createdDate = new DateTime(2012, 1, 7);

            _productCategoryDataSet = GetDbSet(
                new[]
                    {
                        new ProductCategoryDataModel{ Id = _catogoryId1 , Name = "catogoryId1" },
                        new ProductCategoryDataModel{ Id = _catogoryId2 , Name = "catogoryId2" }
                    }
                );

            var products = new List<ProductDataModel>();
            for (var i = 0; i < 25; i++)
            {
                products.Add(new ProductDataModel
                {
                    Id = Guid.NewGuid(),
                    CategoryId = _catogoryId1,
                    StockLevel = StockLevel.High,
                    Name = $"Procuct 1-{1}",
                    Price = i,
                    CreatedOn = _createdDate,
                    Description = $"A nice procuct 1-{1}",
                    Sku = $"1-{1}"
                });

                products.Add(new ProductDataModel
                {
                    Id = Guid.NewGuid(),
                    CategoryId = _catogoryId2,
                    StockLevel = StockLevel.Low,
                    Name = $"Procuct 2-{1}",
                    Price = i,
                    CreatedOn = _createdDate,
                    Description = $"A nice procuct 2-{1}",
                    Sku = $"1-{1}"
                });
            }

            _productsDataSet = GetDbSet(products);

            _searchContext.Setup(x => x.ProductCategories).Returns(_productCategoryDataSet);
            _searchContext.Setup(x => x.Products).Returns(_productsDataSet);
        }

        [Fact]
        public void CanGetById()
        {
            // arragne
            var item = _productsDataSet.Skip(2).First();

            // act
            var result = _searchContext.Object.Get(item.Id);

            // assert
            result.ShouldNotBeNull();
            result.ProductId.ShouldBeEqualTo(item.Id);
            result.ProductCategoryId.ShouldBeEqualTo(item.CategoryId);
            result.ProductCategoryName.ShouldBeEqualTo("catogoryId1");
            result.ProductDescription.ShouldBeEqualTo(item.Description);
            result.ProductDiscountAmount.ShouldBeEqualTo(item.Discount);
            result.ProductDiscountText.ShouldBeEqualTo("£0.00");
            result.ProductName.ShouldBeEqualTo(item.Name);
            result.ProductPriceAmount.ShouldBeEqualTo(item.Price);
            result.ProductPriceText.ShouldBeEqualTo("£1.00");
            result.StockLevel.ShouldBeEqualTo(string.Empty);
            result.StockLevelId.ShouldBeEqualTo((int)StockLevel.High);
        }

        [Fact]
        public void CanReturnSku()
        {
            // arragne
            var sku = "112233";
            _productsDataSet.Skip(4).First().Sku = sku;

            // assert
            _searchContext.Object.IsSkuInUse(sku).ShouldBeTrue();
            _searchContext.Object.IsSkuInUse(sku + "not in use").ShouldBeFalse();
        }

        [Fact]
        public void CanSearchByCategory()
        {
            // arragne

            // act
            var results = _searchContext.Object.GetByCategory(_catogoryId1, 0, 25);

            // assert
            results.Items
                .Count()
                .ShouldBeEqualTo(25);

            results.Items
               .Count(x => x.ProductCategoryId == _catogoryId1)
               .ShouldBeEqualTo(25);

            results.PageCount
                .ShouldBeEqualTo(0);

            results.PageSize
                .ShouldBeEqualTo(25);
        }

        [Fact]
        public void CanSearchByDiscount()
        {
            // arragne
            var discounted = _productsDataSet.Skip(8).First();
            discounted.Discount = int.MaxValue;

            // act
            var results = _searchContext.Object.GetByDiscount(0, 25);

            // assert
            results.Items.First().ProductId.ShouldBeEqualTo(discounted.Id);
            results.Items.First().ProductDiscountAmount.ShouldBeEqualTo(discounted.Discount);

            results.PageCount
                .ShouldBeEqualTo(0);

            results.PageSize
                .ShouldBeEqualTo(25);
        }

        [Fact]
        public void CanSearchByPrice()
        {
            // arragne
            var min = 2.00m;
            var max = 8.00m;

            // act
            var results = _searchContext.Object.GetByPrice(min, max, 0, 25);

            // assert
            results.Items.First().ProductPriceAmount.ShouldBeEqualTo(max);
            results.Items.Last().ProductPriceAmount.ShouldBeEqualTo(min);
            results.Items.Count().ShouldBeEqualTo(14);

            results.PageCount
                .ShouldBeEqualTo(0);

            results.PageSize
                .ShouldBeEqualTo(25);
        }

        [Fact]
        public void CanSearchByStockLevels()
        {
            // arragne
            _productsDataSet.Skip(4).First().StockLevel = StockLevel.SoldOutMoreNoRestock;
            _productsDataSet.Skip(5).First().StockLevel = StockLevel.SoldOutMoreSoon;

            // act
            var results = _searchContext.Object.GetByStockLevels(new[] { StockLevel.SoldOutMoreNoRestock, StockLevel.SoldOutMoreSoon }, 0, 25);

            // assert
            results.Items.Any(x => x.StockLevelId == (int)StockLevel.SoldOutMoreNoRestock).ShouldBeTrue();
            results.Items.Any(x => x.StockLevelId == (int)StockLevel.SoldOutMoreSoon).ShouldBeTrue();
        }

        [Fact]
        public void CanSearchByText()
        {
            // arragne
            var item1 = _productsDataSet.Skip(4).First();
            item1.Name = "A STRING";

            var item2 = _productsDataSet.Skip(8).First();
            item2.Description = "A string";

            // act
            var results = _searchContext.Object.GetByText("a string", 0, 25);

            // assert
            results.Items.Any(x => x.ProductId == item1.Id).ShouldBeTrue();
            results.Items.Any(x => x.ProductId == item2.Id).ShouldBeTrue();
        }

        [Fact]
        public void WontreturnDeletedItems()
        {
            // arragne
            _productsDataSet.Skip(4).Take(100).ToList().ForEach(x => x.DeletedOn = DateTime.Now.AddDays(-1)); // deleted
            _productsDataSet.Skip(2).Take(2).ToList().ForEach(x => x.DeletedOn = DateTime.Now.AddDays(1)); // not deleted yet
            _productsDataSet.Take(2).ToList().ForEach(x => x.DeletedOn = null); // never deleted

            // act
            var results = _searchContext.Object.GetByPrice(-1, 10000, 0, 25);

            // assert
            results.Items.Count().ShouldBeEqualTo(4);
        }

        private static DbSet<T> GetDbSet<T>(IEnumerable<T> sourceArray) where T : class
        {
            var queryable = sourceArray.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet.Object;
        }
    }
}