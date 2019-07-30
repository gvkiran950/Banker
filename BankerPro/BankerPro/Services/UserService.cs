using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BankerPro.Models;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Essentials;

namespace BankerPro.Services
{
    public class UserService : IUserService
    {
        public string Login(User user)
        {
            try
            {               
                var client1 = new RestClient(App.BaseUrl);               
                var request = new RestRequest("api/Accounts/login", Method.POST);
                request.AddJsonBody(user);                                 
                // execute the request
                var resp = client1.Execute(request);
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //string[] res = resp.Content.Split(':');
                    string response = JsonConvert.DeserializeObject<string>(resp.Content);
                    return response;
                }
                else
                    throw new Exception("something went wrong");
                #region .Net Api Call(native)
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri("http://192.168.0.104/");
                //    client.DefaultRequestHeaders.Accept.Clear();
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    //Send HTTP requests from here.  
                //    // var result = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos/1");
                //    var department = new User() { UserName = "Test Department", Password = "sdfkjlkdgjldsfkjgsdljf" };
                //  HttpResponseMessage response = await client.PostAsJsonAsync("api/Accounts/login", department);

                //    if (response.IsSuccessStatusCode)
                //    {
                //        // Get the URI of the created resource.  
                //        Uri returnUrl = response.Headers.Location;
                //        //Console.WriteLine(returnUrl);
                //    }

                //    return await response.Content.ReadAsStringAsync();
                //}
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetValues()
        {
            try
            {
                List<string> values = new List<string>();
                var client1 = new RestClient(App.BaseUrl);
                
                var request = new RestRequest("api/Accounts/SomeGetinfo", Method.GET);
                var authToken = await SecureStorage.GetAsync("auth_token");
                request.AddParameter("Authorization", authToken,ParameterType.HttpHeader);
                //request.AddJsonBody(user);
                // execute the request
                var resp = client1.Execute(request);
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    values = JsonConvert.DeserializeObject<List<string>>(resp.Content);
                }
                else
                    throw new Exception("something went wrong");

                return values;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<string> LoginAsync(User user)
        //{
        //    try
        //    {
        //        var client = new RestClient(App.BaseUrl);
        //        var request = new RestRequest("api/Accounts/login", Method.POST);
        //        request.AddJsonBody(user);
        //        // execute the request
        //        var resp = await client.ExecuteAsync(request,);
        //        if (resp.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            //string[] res = resp.Content.Split(':');
        //            string response = JsonConvert.DeserializeObject<string>(resp.Content);
        //            return response;
        //        }
        //        else
        //            throw new Exception("something went wrong");
              
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
