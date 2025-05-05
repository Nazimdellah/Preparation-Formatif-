using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{
    [TestMethod]
    public void Testing_Ok()
    {
        var SeatService = new Mock<SeatsService>();
        Mock<SeatsController> SeatContoller = new Mock<SeatsController>(SeatService.Object) { CallBase = true };
        SeatContoller.Setup(u => u.UserId).Returns("2");
        var seat = new Seat();

        SeatService.Setup(s => s.ReserveSeat(SeatContoller.Object.UserId, It.IsAny<int>())).Returns(seat);
        var actionResult = SeatContoller.Object.ReserveSeat(1);
        var result = actionResult.Result as OkObjectResult;
        Assert.IsNotNull(result);




    }
    [TestMethod]
    public void testNotfound()
    {
        var SeatService = new Mock<SeatsService>();
        Mock<SeatsController> SeatContoller = new Mock<SeatsController>(SeatService.Object) { CallBase = true };
        SeatContoller.Setup(u => u.UserId).Returns("2");
        var seat = new Seat();
        seat.Id = 2;
        seat.Number = 101;
      

        SeatService.Setup(s => s.ReserveSeat(SeatContoller.Object.UserId, It.IsAny<int>())).Throws(new SeatOutOfBoundsException());
        var actionResult = SeatContoller.Object.ReserveSeat(seat.Number);
        var result = actionResult.Result as NotFoundObjectResult;
        Assert.IsNotNull(result);




    }

}
