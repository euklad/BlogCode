namespace TestingPrivate.Old;

public class BookingHandler
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
        if (flights.AvailableFlights.ContainsKey(pickUpLocation.LocationCode))
        {
            pickUpLocation.ArrivalFlight = flights.AvailableFlights[pickUpLocation.LocationCode];
        }
    }
}
