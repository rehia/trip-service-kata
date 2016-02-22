using System;
using System.Collections.Generic;
using TripServiceKata.Tests;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        private static readonly Trip.Trip ToChina = new Trip.Trip();
        private static readonly Trip.Trip ToNewZealand = new Trip.Trip();
        private static readonly User.User AnotherFriend = new User.User();
        private static readonly User.User Me = new User.User();
        private static readonly User.User Guest = null;

        [Test]
        public void ShouldRaiseAnErrorWhenGuest()
        {
            TripService tripService = NewTripService(Guest);

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(new User.User()));
        }

        private static TripService NewTripService(User.User loggedUser)
        {
            return new TripService(new TripDAOStub(), new UserSessionStub(loggedUser));
        }

        [Test]
        public void ShouldReturnNoTripsWhenNotFriend()
        {
            TripService tripService = NewTripService(Me);

            var user = new User.User();
            user.AddTrip(ToChina);
            user.AddTrip(ToNewZealand);
            user.AddFriend(AnotherFriend);

            var trips = tripService.GetTripsByUser(user);

            Assert.That(trips, Is.Empty);
        }

        [Test]
        public void ShouldReturnFriendsTrips()
        {
            TripService tripService = NewTripService(Me);

            var user = new User.User();
            user.AddTrip(ToChina);
            user.AddTrip(ToNewZealand);
            user.AddFriend(AnotherFriend);
            user.AddFriend(Me);

            var trips = tripService.GetTripsByUser(user);

            Assert.That(trips.Count, Is.EqualTo(2));
        }
    }

    class TripDAOStub : ITripDao
    {
        public List<Trip.Trip> FindTripsByUserNonStatic(User.User user)
        {
            return user.Trips();
        }
    }

    class UserSessionStub : IUserSession
    {
        private User.User loggedUser;

        public UserSessionStub(User.User loggedUser)
        {
            this.loggedUser = loggedUser;
        }

        public bool IsUserLoggedIn(User.User user)
        {
            return this.loggedUser != null;
        }

        public User.User GetLoggedUser()
        {
            return this.loggedUser;
        }
    }
}
