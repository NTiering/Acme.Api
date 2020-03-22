using Acme.Toolkit.Extensions;
using FluentAssert;
using System;
using System.Linq;
using Xunit;

namespace Acme.Toolkit.Tests
{
    public class IQueryableExtTests
    {
        private IQueryable<string> queryable;

        public IQueryableExtTests()
        {
            queryable = Enum.GetNames(typeof(DayOfWeek)).AsQueryable();
        }

        [Fact]
        public void ItAllowsOverPaging()
        {
            queryable.Paginate(0, 20).Last().ShouldBeEqualTo("Saturday");
            queryable.Paginate(0, 20).Count().ShouldBeEqualTo(7);
        }

        [Fact]
        public void ItObeysPageSize()
        {
            queryable.Paginate(1, 2).First().ShouldBeEqualTo("Tuesday");
            queryable.Paginate(1, 2).Last().ShouldBeEqualTo("Wednesday");
        }

        [Fact]
        public void ItPreservesOrder()
        {
            queryable.Paginate(0, 7).First().ShouldBeEqualTo("Sunday");
            queryable.Paginate(0, 7).Last().ShouldBeEqualTo("Saturday");
        }
    }
}