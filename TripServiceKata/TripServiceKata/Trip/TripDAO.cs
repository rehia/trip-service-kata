using System;
using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
    public class TripDAO : ITripDao
    {
        [Obsolete("Use instance call FindTripsByUserNonStatic instead.")]
        public static List<Trip> FindTripsByUser(User.User user)
        {
            return new TripDAO().FindTripsByUserNonStatic(user);
        }

        public List<Trip> FindTripsByUserNonStatic(User.User user)
        {
            throw new DependencyCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }
    }
}
