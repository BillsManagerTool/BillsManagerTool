namespace BillsManagement.Tests
{
    using BillsManagement.API.Controllers;
    using BillsManagement.DomainModel;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class OccupantsControllerTests
    {
        private Mock<IOccupantService> _service;
        private OccupantsController _controller;

        [TestInitialize]
        public void Init()
        {
            this._service = new Mock<IOccupantService>();
            this._controller = new OccupantsController(this._service.Object);

            this._controller.ControllerContext = new ControllerContext();
            this._controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Arrange
            this._service.Setup(x => x.GetOccupantDetailsById(It.IsAny<int>()))
                .Returns(new DetailedOccupant());
        }

        [TestMethod]
        public void GetOccupant_OccupantFound_ShouldReturnDetailedOccupant()
        {
            // Arrange            
            this._controller.HttpContext.Items["UserId"] = 123;
            var occupantId = int.Parse(this._controller.HttpContext.Items["UserId"].ToString());

            var expectedResult = new DetailedOccupant()
            {
                OccupantId = 123
            };

            this._service.Setup(x => x.GetOccupantDetailsById(occupantId))
                .Returns(expectedResult);

            // Act
            var result = this._controller.GetOccupant();
            var actualResult = (DetailedOccupant)((ActionResult<DetailedOccupant>)result).Value;

            // Assert
            Assert.AreEqual(expectedResult.OccupantId, actualResult.OccupantId);
        }
    }
}
