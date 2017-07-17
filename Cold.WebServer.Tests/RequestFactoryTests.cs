using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xunit;

namespace Cold.WebServer.Tests
{
    public class RequestFactoryTests
    {
        [Fact]
        public void RequestFactory_GETHTTP1RequestBuffer_ReturnsResponseOKStatus()
        {
        //Arrange
            var factory = new RequestFactory();
            var simpleRequest = Encoding.ASCII.GetBytes("GET / HTTP1.0");
            
            //Act
            var response = factory.Create(simpleRequest);
            
            //Assert
//            response.Sta.Should().Be(HttpStatusCode.Ok);

        }
        
    }
}