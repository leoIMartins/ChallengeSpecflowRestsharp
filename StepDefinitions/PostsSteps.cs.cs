using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowExample.StepDefinitions
{
    [Binding]
    public class PostsSteps
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;
        private string existingUserId = "74";
        private string existingUserUserId = "8";
        private string existingUserTitle = "\"enim unde ratione doloribus quas enim ut sit sapiente\"";
        private string newUserUserId = "12345";
        private string newUserTitle = "title test";
        private string newUserBody = "body test";


        public PostsSteps()
        {
            client = new RestClient("https://jsonplaceholder.typicode.com");
            request = new RestRequest();
            response = new RestResponse();
        }

        [Given(@"I send a GET request by ID")]
        public void GivenISendAGETRequestByID()
        {
            request = new RestRequest("/posts/" + this.existingUserId, Method.Get);
            response = client.Execute(request);
        }

        [Given(@"I send a POST request")]
        public void GivenISendAPOSTRequest()
        {
            
            request = new RestRequest("/posts", Method.Post);
            request.AddJsonBody(new
            {
                userId = this.newUserUserId,
                title = this.newUserTitle,
                body = this.newUserBody
            });
            response = client.Execute(request);
        }

        [Then(@"the response status code should be ""(.*)""")]
        public void ThenTheResponseStatusCodeShouldBe(string statusCode)
        {
            HttpStatusCode expectedStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode);
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [Then(@"the response body should contain existing user data")]
        public void ThenTheResponseBodyShouldContainExistingUserData()
        {
            Assert.That(response.Content.Contains("\"userId\": " + this.existingUserUserId));
            Assert.That(response.Content.Contains("\"id\": " + this.existingUserId));
            Assert.That(response.Content.Contains("\"title\": " + this.existingUserTitle));
        }

        [Then(@"the response body should contain new user data")]
        public void ThenTheResponseBodyShouldContainNewUserData()
        {
            Console.WriteLine(response.Content);
            Assert.That(response.Content.Contains("\"userId\": \"" + this.newUserUserId + "\""));
            Assert.That(response.Content.Contains("\"title\": \"" + this.newUserTitle + "\""));
            Assert.That(response.Content.Contains("\"body\": \"" + this.newUserBody + "\""));
        }
    }
}
