using Microsoft.VisualStudio.TestTools.UnitTesting;
using Metrics.Domain;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Metrics.Domain.Tests
{
    [TestClass]
    public class Playground
    {
        [TestMethod]
        public async void TestMethod()
        {
            var urls = new List<string> { "google.com", "microsoft.com" };

            /*foreach(var url in urls)
            {
                Thread thread = new Thread(() => {
                            HttpClient client = new HttpClient();
                            HttpResponseMessage response = client.GetAsync(url).Result;
                            string html = response.Content.ReadAsStringAsync().Result;
                        }
                );


                thread.Start();

            }*/

            /*List<Task<string>> tasks = new List<Task<string>>();

            foreach (var url in urls)
            {
                Task<string> task = Task<string>.Run(() =>
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    return response.Content.ReadAsStringAsync().Result;
                });


                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            foreach(var task in tasks)
            {
                Console.Write(task.Result);
            }*/

            HttpClient client = new();
            Task<HttpResponseMessage> response = client.GetAsync("google.com");
            HttpResponseMessage response2 = await client.GetAsync("microsoft.com");
        } 
    }
}
