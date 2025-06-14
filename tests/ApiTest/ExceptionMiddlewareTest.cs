using Api.Middleware;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApiTest;

public class ExceptionMiddlewareTest
{

    [Fact]
    public async Task Middlare_ShouldThrowException()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
        var nextMock = new Mock<RequestDelegate>();
        var env = new Mock<IHostEnvironment>();
        var context = new DefaultHttpContext();
        var middleware = new ExceptionMiddleware(loggerMock.Object, env.Object);


        //Action
        await middleware.InvokeAsync(context, nextMock.Object);

        //Assert
        loggerMock.Verify(logger => logger.Log(
        It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
        It.Is<EventId>(eventId => eventId.Id == 0),
        It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "myMessage" && @type.Name == "FormattedLogValues"),
        It.IsAny<Exception>(),
        It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Never);
    }
}
