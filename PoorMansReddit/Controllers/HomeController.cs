using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PoorMansReddit.Models;

namespace PoorMansReddit.Controllers
{
	public class HomeController : Controller
	{
		public List<Reddit> redditList = new List<Reddit>();
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Reddit()
		{
			Reddit r = new Reddit();
			List<JToken> RedditInfo = GetTitles();
			ViewBag.RedditInfo = redditList;
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		public List<JToken> GetTitles()
		{
			
			string url = "http://www.reddit.com/r/aww/.json";

			HttpWebRequest Request = WebRequest.CreateHttp(url);

			//Request.Headers.Add("OAuth2", value);


			HttpWebResponse response = (HttpWebResponse)Request.GetResponse();

			//Getting the data from a response 
			StreamReader sr = new StreamReader(response.GetResponseStream());

			string APIText = sr.ReadToEnd();
			sr.Close();
			//Now we're moving into parsing
			//JToken parses the JSon info into C# natural language.
			JToken titleData = JToken.Parse(APIText);

			//Making a string that goes into the website 
			//and looks for the name of the field in the API
			List<JToken> titles = titleData["data"]["children"].ToList();
			
			
			for (int i = 1; i < 11; i++)
			{
				Reddit r = new Reddit();
				r.Title = titles[i]["data"]["title"].ToString();
				r.Thumbnail = titles[i]["data"]["thumbnail"].ToString();
				r.URL = titles[i]["data"]["url"].ToString();
				redditList.Add(r);
			}
			return titles;
		}
	}
}