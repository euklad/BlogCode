using FluentAssertions;
using JsonPathToModel;
using static TestingPrivate.BookingHandler;

namespace TestingPrivate;

public class Tests
{
    [Fact]
    public void Reflection_Private_Execution_Success_Test()
    {
        // act
        var info = GetTestData();
        typeof(Old.BookingHandler).ExecutePrivateStatic("SetArrivalLocationInfo", info.PickUpLocation, info.Flights);

        //assert
        info.PickUpLocation.ArrivalFlight.Should().NotBeNull();
    }

    [Fact]
    public void Mapper_Execution_Success_Test()
    {
        // act
        var info = GetTestData();
        FlightInfo? flight = new PickUpLocationArrivalFlightInfoMapper(info.PickUpLocation, info.Flights);

        //assert
        flight.Should().NotBeNull();
    }

    private PassengerInfo GetTestData()
    {
        return new PassengerInfo()
        {
            PickUpLocation = new PickUpLocationInfo { LocationCode = "SYD" },
            Flights = new FlightsInfo { AvailableFlights = new() { ["SYD"] = new() } }
        };
    }
}
