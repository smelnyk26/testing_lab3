using NUnit.Framework;
using TechTalk.SpecFlow;
using ApiTestingProject.Services;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ApiTestingProject.Steps
{
    [Binding]
    public class BookingApiSteps
    {
        private readonly BookingApiService _bookingApiService = new BookingApiService();
        private RestResponse response;
        private string bookingId;

        [Given(@"I am authenticated")]
        public void GivenIAmAuthenticated()
        {
            _bookingApiService.Authenticate();
        }

        [When(@"I create a new booking with the following details")]
        public void WhenICreateANewBookingWithTheFollowingDetails(Table table)
        {
            var bookingDetails = new
            {
                firstname = table.Rows[0]["firstname"],
                lastname = table.Rows[0]["lastname"],
                totalprice = int.Parse(table.Rows[0]["totalprice"]),
                depositpaid = bool.Parse(table.Rows[0]["depositpaid"]),
                bookingdates = new
                {
                    checkin = table.Rows[0]["checkin"],
                    checkout = table.Rows[0]["checkout"]
                },
                additionalneeds = table.Rows[0]["additionalneeds"]
            };
            response = _bookingApiService.CreateBooking(bookingDetails);
            var jsonResponse = JObject.Parse(response.Content);
            bookingId = jsonResponse["bookingid"]?.ToString();
        }

        [Then(@"the booking should be created successfully")]
        public void ThenTheBookingShouldBeCreatedSuccessfully()
        {
            Assert.AreEqual(200, (int)response.StatusCode, "Failed to create booking");
            Assert.IsNotNull(bookingId, "Booking ID is not present in the response");
        }
    }
}
