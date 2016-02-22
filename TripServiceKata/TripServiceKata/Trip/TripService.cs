using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private readonly ITripDao tripDao;
        private readonly IUserSession userSession;

        public TripService() : this(new TripDAO(), UserSession.GetInstance())
        { }

        public TripService(ITripDao tripDao, IUserSession userSession)
        {
            this.tripDao = tripDao;
            this.userSession = userSession;
        }

        public List<Trip> GetTripsByUser(User.User user)
        {
            var loggedUser = GetLoggedUser();
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }

            if (user.IsFriendWith(loggedUser))
            {
                return FindTripsByUser(user);
            }
            return new List<Trip>();
        }

        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            return tripDao.FindTripsByUserNonStatic(user);
        }

        protected virtual User.User GetLoggedUser()
        {
            return userSession.GetLoggedUser();
        }
    }
}
