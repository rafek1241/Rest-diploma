using NUnit.Framework;
using Rest.Web.Engineer.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FluentAssertions;
using Moq;
using NUnit.Framework.Constraints;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Logic.Tests
{
    [TestFixture()]
    public class CategoryLogicTests
    {
        [Test]
        public void GetCategories_ListNotNull_ReturnTheList()
        {
            var categories = new List<Category>()
            {
                new Category() {CategoryId = 1},
                new Category() {CategoryId = 2}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categories.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());
            var contextMock = new Mock<Entities>();
            contextMock.Setup(p => p.Categories).Returns(mockSet.Object);


            var logic = new CategoryLogic(contextMock.Object);
            var result = logic.GetCategories();

            result.Should().HaveCount(2);
            result.Should().AllBeOfType<Category>();
        }

        [Test]
        public void GetCategories_ListIsNull_ReturnNull()
        {
            var mockSet = new Mock<DbSet<Category>>();
            var contextMock = new Mock<Entities>();
            contextMock.Setup(p => p.Categories).Returns(mockSet.Object);

            var logic = new CategoryLogic(contextMock.Object);
            var result = logic.GetCategories();

            contextMock.Verify(entities => entities.Categories, Times.Once());

        }

        [Test]
        public void GetCategory_CategoryNotNull_ReturnsCategory()
        {
            var category = new Category() { CategoryId = 1 };
            //var contextMock = Mock.Of<Entities>(entities => entities.Categories.Find(1) == category);
            var dbSetMock = Mock.Of<DbSet<Category>>(set => set.Find(1) == category);
            var contextMock = Mock.Of<Entities>(entities => entities.Categories.Find(1) == dbSetMock.Find(1));

            var logic = new CategoryLogic(contextMock);

            logic.Invoking(p => p.GetCategory(1)).ShouldBeEquivalentTo(category);
        }

        [TestCase(0)]
        [TestCase(null)]
        public void GetCategory_InvalidCategoryId_ThrowsNotFound(long id)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            var contextMock = Mock.Of<Entities>();

            var logic = new CategoryLogic(contextMock);

            logic.Invoking(p => p.GetCategory(id)).ShouldThrow<HttpResponseException>().Where(p => p.Response == response);
        }


    }
}