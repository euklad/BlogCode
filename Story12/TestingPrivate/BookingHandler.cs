namespace TestingPrivate;

// partial main
public partial class BookingHandler
{
    public BookingHandler(/* lots of dependecies here
        ILogger<BookingHandler> logger,
        IMapper mapper,
        IRepository repository,
        ... */)
    {
    }

    public async Task Handle(Guid passengerId)
    {
        PassengerInfo passengerInfo;
        // get passenger by id should be here
        passengerInfo = new PassengerInfo();

        // some more logic and calls here
        // ...

        SetArrivalLocationInfo(passengerInfo.PickUpLocation, passengerInfo.Flights);

        // some more logic and calls here
        // ...
    }

    private static void SetArrivalLocationInfo(PickUpLocationInfo pickUpLocation, FlightsInfo flights)
    {
        pickUpLocation.ArrivalFlight = new PickUpLocationArrivalFlightInfoMapper(pickUpLocation, flights);
    }
}

// partial with records
public partial class BookingHandler
{
    public sealed record PickUpLocationArrivalFlightInfoMapper(PickUpLocationInfo PickUpLocation, FlightsInfo Flights)
    {
        public static implicit operator FlightInfo?(
            PickUpLocationArrivalFlightInfoMapper mapper)
        {
            if (mapper.PickUpLocation.LocationCode == null || !mapper.Flights.AvailableFlights.ContainsKey(mapper.PickUpLocation.LocationCode))
            {
                return null;
            }

            return mapper.Flights.AvailableFlights[mapper.PickUpLocation.LocationCode];
        }
    }
}

public class PassengerInfo
{
    public PickUpLocationInfo PickUpLocation { get; set; } = new();
    public FlightsInfo Flights { get; set; } = new();
}

public class PickUpLocationInfo
{
    public string? LocationCode { get; set; }
    public FlightInfo? ArrivalFlight { get; set; }
}

public class FlightInfo
{
    public string? LocationCode { get; set; }
    public string? FlightCode { get; set; }
    public DateTime ArrivalDateTime { get; set; }
}

public class FlightsInfo
{
    public Dictionary<string, FlightInfo> AvailableFlights { get; set; } = [];
}

