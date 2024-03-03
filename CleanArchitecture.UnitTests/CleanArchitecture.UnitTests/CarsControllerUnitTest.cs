using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTests;

public class CarsControllerUnitTest
{
    [Fact]
    public async void Create_ReturnsOkResult_WhenRequestIsValid()
    {
        //Arrange
        var mediatorMock= new Mock<IMediator>();
        CreateCarCommand createCarCommand=new CreateCarCommand("Toyota","Corolla",300);
        MessageResponse messageResponse=new("Araç başarıyla kaydedildi.");
        CancellationToken cancellationToken=new();
        mediatorMock.Setup(m => m.Send(createCarCommand, cancellationToken))
            .ReturnsAsync(messageResponse);
        CarsController carController=new (mediatorMock.Object);
        //Act
        var result=await carController.Create(createCarCommand,cancellationToken);

        //Assertion
        var okResult=Assert.IsType<OkObjectResult>(result);
        var returnValue=Assert.IsType<MessageResponse>(okResult.Value);
        Assert.Equal(messageResponse, returnValue);
        mediatorMock.Verify(m=>m.Send(createCarCommand, cancellationToken),Times.Once);
    }
}
