using Acme.Data.Context;
using Acme.Data.DataModels;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Acme.Web.Api.Config
{
    public static class RandomExt
    {
        public static decimal NextDecimal(this Random rnd, int maxValue)
        {
            var t = rnd.NextDouble() * maxValue;
            var rtn = Convert.ToDecimal(Math.Round(t, 2));
            return rtn;
        }
    }

    public class ExampleDataSetup
    {
        internal static async Task Setup(IDataContext dataContext, IIdentity identity)
        {
            var hatsCategory = new ProductCategoryDataModel { Name = "Hats" };
            var scarfCategory = new ProductCategoryDataModel { Name = "Scarves" };
            var glovesCategory = new ProductCategoryDataModel { Name = "Gloves" };

            await dataContext.Add(hatsCategory, identity);
            await dataContext.Add(scarfCategory, identity);
            await dataContext.Add(glovesCategory, identity);

            var rnd = new Random(DateTime.Now.Millisecond);
            foreach (var colour in Enum.GetNames(typeof(KnownColor)).Where(x => x.Length < 6))
            {
                var gloveProduct = new ProductDataModel
                {
                    CategoryId = glovesCategory.Id,
                    Name = $"{colour} Gloves",
                    Description = $"A nice pair of {colour.ToLower()} gloves. Now in pairs",
                    Price = rnd.NextDecimal(20),
                    Sku = $"g-{colour}".ToLower(),
                    Discount = rnd.NextDecimal(2),
                    StockLevel = (StockLevel)rnd.Next(0, 3)
                };

                await dataContext.Add(gloveProduct, identity);

                for (var i = 0; i <= 12; i++)
                {
                    var reviewBad = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {gloveProduct.Name}, not nice",
                        ProductId = gloveProduct.Id,
                        Score = rnd.Next(1,3)
                    };
                    await dataContext.Add(reviewBad, identity);

                    var reviewOk = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {gloveProduct.Name}, just Ok",
                        ProductId = gloveProduct.Id,
                        Score = rnd.Next(4,8)
                    };
                    await dataContext.Add(reviewOk, identity);

                    var reviewGood = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {gloveProduct.Name}, very good",
                        ProductId = gloveProduct.Id,
                        Score = rnd.Next(9,10)
                    };
                    await dataContext.Add(reviewGood, identity);
                }

                var scarfProduct = new ProductDataModel
                {
                    CategoryId = scarfCategory.Id,
                    Name = $"{colour} Scarf",
                    Description = $"A nice {colour.ToLower()} scarf. Available in 3ft & 8ft lengths.",
                    Price = rnd.NextDecimal(20),
                    Sku = $"s-{colour}".ToLower(),
                    Discount = rnd.NextDecimal(2),
                    StockLevel = (StockLevel)rnd.Next(0, 3)
                };

                await dataContext.Add(scarfProduct, identity);

                for (var i = 0; i <= 12; i++)
                {
                    var reviewBad = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {scarfProduct.Name}, not nice",
                        ProductId = scarfProduct.Id,
                        Score = rnd.Next(1, 3)
                    };
                    await dataContext.Add(reviewBad, identity);

                    var reviewOk = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {scarfProduct.Name}, just Ok",
                        ProductId = scarfProduct.Id,
                        Score = rnd.Next(4, 8)
                    };
                    await dataContext.Add(reviewOk, identity);

                    var reviewGood = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {scarfProduct.Name}, very good",
                        ProductId = scarfProduct.Id,
                        Score = rnd.Next(9, 10)
                    };
                    await dataContext.Add(reviewGood, identity);
                }

                var hatProduct = new ProductDataModel
                {
                    CategoryId = glovesCategory.Id,
                    Name = $"{colour} hat",
                    Description = $"A nice {colour.ToLower()} bowler hat. Very flammable.",
                    Price = rnd.NextDecimal(20),
                    Sku = $"h-{colour}".ToLower(),
                    Discount = rnd.NextDecimal(2),
                    StockLevel = (StockLevel)rnd.Next(0, 3)
                };

                await dataContext.Add(hatProduct, identity);

                for (var i = 0; i <= 12; i++)
                {
                    var reviewBad = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {hatProduct.Name}, not nice",
                        ProductId = hatProduct.Id,
                        Score = rnd.Next(1, 3)
                    };
                    await dataContext.Add(reviewBad, identity);

                    var reviewOk = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {hatProduct.Name}, just Ok",
                        ProductId = hatProduct.Id,
                        Score = rnd.Next(4, 8)
                    };
                    await dataContext.Add(reviewOk, identity);

                    var reviewGood = new ProductReviewDataModel
                    {
                        ReviewText = $"review for {hatProduct.Name}, very good",
                        ProductId = hatProduct.Id,
                        Score = rnd.Next(9, 10)
                    };
                    await dataContext.Add(reviewGood, identity);
                }
            }

            
        }
    }
}