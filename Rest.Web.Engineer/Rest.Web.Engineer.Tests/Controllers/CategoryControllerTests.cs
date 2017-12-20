using NUnit.Framework;
using Rest.Web.Engineer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Controllers.Tests
{
    [TestFixture()]
    public class CategoryControllerTests
    {
        [Test()]
        public void GetCategories_ReturnsListOfCategories()
        {
            //Arrange
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic => logic.GetCategories() ==
            new List<ICategory>
            {
                Mock.Of<ICategory>(category=>category.CategoryId == 1),
                Mock.Of<ICategory>(category=>category.CategoryId == 2),
                Mock.Of<ICategory>(category=>category.CategoryId == 3)
            }.AsEnumerable());
            //Act
            var controller = new CategoryController(categoryLogicMock);
            var categories = controller.Get();
            //Assert
            categories.Should().HaveCount(3);
        }

        [Test()]
        public void GetCategory_ReturnsCategory()
        {
            var categoryMock = Mock.Of<ICategory>(cat => cat.CategoryId == 1);
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic => logic.GetCategory(1) == categoryMock);

            var controller = new CategoryController(categoryLogicMock);
            var category = controller.Get(1);

            category.ShouldBeEquivalentTo(categoryMock);
        }

        [Test()]
        public void GetCategory_ReturnsNull()
        {
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic => logic.GetCategory(0) == null);

            var controller = new CategoryController(categoryLogicMock);
            var category = controller.Get(0);

            category.ShouldBeEquivalentTo(null);
        }

        [Test()]
        public void SetCategory_PostCategory_ReturnsOK()
        {
            var categoryInputMock = Mock.Of<ICategory>(p => p.Name == "Test1");
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic =>
                logic.SetCategory(categoryInputMock) ==
                new HttpResponseMessage(HttpStatusCode.OK)
            );

            var controller = new CategoryController(categoryLogicMock);
            var result = controller.Post(categoryInputMock);

            result.ShouldBeEquivalentTo(new HttpResponseMessage(HttpStatusCode.OK));
        }

        [Test()]
        public void SetCategory_PostNull_ReturnsNull()
        {
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic =>
                logic.SetCategory(null) ==
                new HttpResponseMessage(HttpStatusCode.NotAcceptable)
            );

            var controller = new CategoryController(categoryLogicMock);
            var result = controller.Post(null);

            result.ShouldBeEquivalentTo(new HttpResponseMessage(HttpStatusCode.NotAcceptable));
        }

        [Test()]
        public void UpdateCategory_PutCategory_returnOK()
        {
            var categoryInputMock = Mock.Of<ICategory>(p => p.Name == "Test1"
            );
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic =>
                logic.UpdateCategory(1, categoryInputMock) ==
                new HttpResponseMessage(HttpStatusCode.OK)
            );

            var controller = new CategoryController(categoryLogicMock);
            var result = controller.Put(1, categoryInputMock);

            result.ShouldBeEquivalentTo(new HttpResponseMessage(HttpStatusCode.OK));
        }
        [Test()]
        public void UpdateCategory_PutNull_returnNotAccept()
        {
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic =>
                logic.UpdateCategory(1, null) ==
                new HttpResponseMessage(HttpStatusCode.NotAcceptable)
            );

            var controller = new CategoryController(categoryLogicMock);
            var result = controller.Put(1, null);

            result.ShouldBeEquivalentTo(new HttpResponseMessage(HttpStatusCode.NotAcceptable));
        }
        [Test]
        public void UpdateCategory_PutCategoryWithWrongId_returnNotFound()
        {
            var categoryInputMock = Mock.Of<ICategory>(p => p.Name == "Test1"
            );
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic =>
                logic.UpdateCategory(0, categoryInputMock) ==
                new HttpResponseMessage(HttpStatusCode.NotFound)
                &&
                logic.UpdateCategory(0, null) == new HttpResponseMessage(HttpStatusCode.NotFound)
            );

            var controller = new CategoryController(categoryLogicMock);
            var resultWithCategory = controller.Put(0, categoryInputMock);
            var resultWithNull = controller.Put(0, null);

            resultWithCategory.ShouldBeEquivalentTo(new HttpResponseMessage(HttpStatusCode.NotFound));
            resultWithNull.ShouldBeEquivalentTo(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        [Test()]
        public void RemoveCategory_DeleteCategoryWithId1_ReturnsOK()
        {
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic =>
                logic.RemoveCategory(1) == new HttpResponseMessage(HttpStatusCode.OK));

            var controller = new CategoryController(categoryLogicMock);
            var result = controller.Delete(1);

            result.ShouldBeEquivalentTo(new HttpResponseMessage(HttpStatusCode.OK));
        }
        [Test()]
        public void RemoveCategory_DeleteCategoryWithId_ReturnsNotFound()
        {
            var categoryLogicMock = Mock.Of<ICategoryLogic>(logic =>
                logic.RemoveCategory(0) == new HttpResponseMessage(HttpStatusCode.NotFound));

            var controller = new CategoryController(categoryLogicMock);
            var result = controller.Delete(0);

            result.ShouldBeEquivalentTo(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}