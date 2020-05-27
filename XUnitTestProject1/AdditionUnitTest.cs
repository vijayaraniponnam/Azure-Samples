
using com.sun.org.apache.bcel.@internal.util;
using FunctionApp1;
using FunctionApp1.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace XUnitTestProject1
{
    /// <summary>
    /// Unit test cases for addition of two numbers
    /// </summary>
    public class AdditionUnitTest
    {
        /// <summary>
        /// Test case for add two positive numbers
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Add_Two_PositiveNumbers()
        {
            var uri = "https://functionapp19296.azurewebsites.net/api/Addition";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            Elements elements = new Elements();
            elements.number1 = 2;
            elements.number2 = 3;
            var expectedResult = 5;
            request.Content = new StringContent(
                JsonConvert.SerializeObject(elements),
                Encoding.UTF8, "application/json");

            // Function app call
            var httpResponse = Addition.Run(request);
            var stringContent = await httpResponse.Result.Content.ReadAsStringAsync();
            var elementsResult = JsonConvert.DeserializeObject<Elements>(stringContent);

            Assert.Equal(elementsResult.result, expectedResult);
        }

        /// <summary>
        /// Test case for add two negative numbers
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Add_Two_NegativeNumbers()
        {
            var uri = "https://functionapp19296.azurewebsites.net/api/Addition";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            Elements elements = new Elements();
            elements.number1 = -2;
            elements.number2 = -3;
            var expectedResult = -5;
            request.Content = new StringContent(
                JsonConvert.SerializeObject(elements),
                Encoding.UTF8, "application/json");

            // Function app call
            var httpResponse = Addition.Run(request);
            var stringContent = await httpResponse.Result.Content.ReadAsStringAsync();
            var elementsResult = JsonConvert.DeserializeObject<Elements>(stringContent);

            Assert.Equal(elementsResult.result, expectedResult);
        }

        /// <summary>
        /// All positive test cases using theory
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 2, 2)]
        [InlineData(3, 0, 3)]
        [InlineData(100, 200, 300)]
        public async Task Add_All_PositiveCase(int num1, int num2, int expected)
        {
            var uri = "https://functionapp19296.azurewebsites.net/api/Addition";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            Elements elements = new Elements();
            elements.number1 = num1;
            elements.number2 = num2;
            var expectedResult = expected;
            request.Content = new StringContent(
                JsonConvert.SerializeObject(elements),
                Encoding.UTF8, "application/json");

            // Function app call
            var httpResponse = Addition.Run(request);
            var stringContent = await httpResponse.Result.Content.ReadAsStringAsync();
            var elementsResult = JsonConvert.DeserializeObject<Elements>(stringContent);

            Assert.Equal(elementsResult.result, expectedResult);
        }

        /// <summary>
        /// Addition of two numbers with negative test case using theory
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(0, 0, 10)]
        [InlineData(5, 2, 2)]
        public async Task Add_With_NegativeCase(int num1, int num2, int expected)
        {
            var uri = "https://functionapp19296.azurewebsites.net/api/Addition";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            Elements elements = new Elements();
            elements.number1 = num1;
            elements.number2 = num2;
            var expectedResult = expected;
            request.Content = new StringContent(
                JsonConvert.SerializeObject(elements),
                Encoding.UTF8, "application/json");

            // Function app call
            var httpResponse = Addition.Run(request);
            var stringContent = await httpResponse.Result.Content.ReadAsStringAsync();
            var elementsResult = JsonConvert.DeserializeObject<Elements>(stringContent);

            Assert.NotEqual(elementsResult.result, expectedResult);
        }
    }

}
