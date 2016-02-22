using System.Collections.Generic;

namespace TripServiceKata.Trip
{
    public interface ITripDao
    {
        List<Trip> FindTripsByUserNonStatic(User.User user);
    }
}