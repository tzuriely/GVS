using DataMining.Shared.Interfaces;
using DataMining.Shared.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataMining.Robots.Sport5
{
    public class Sport5Robot : IRobot<List<Sport5GameModel>>
    {
        private readonly ILogger<Sport5Robot> _logger;

        public Sport5Robot(ILogger<Sport5Robot> logger)
        {
            _logger = logger;
        }

        private readonly Dictionary<string, string> _urlList = new Dictionary<string, string>()
        { 
            {"champions league","http://vod.sport5.co.il/?Vc=7958"  },
            { "Isreal-league", "http://vod.sport5.co.il/?Vc=3058" } ,
            { "Isreal-Toto league","http://vod.sport5.co.il/?Vc=3058"},
            {"Isreal-Second league","http://vod.sport5.co.il/?Vc=3785" },
            {"Italy-league","http://vod.sport5.co.il/?Vc=3197" } ,
            {"France-league","http://vod.sport5.co.il/?Vc=3084" },
            {"scotland-league","http://vod.sport5.co.il/?Vc=4427" },
            {"switzerland-league","http://vod.sport5.co.il/?Vc=4428" }
        };

        public async Task<RobotResult<List<Sport5GameModel>>> Run()
        {
            try
            {
                DateTime startTime = DateTime.Now;
                var games = new List<Sport5GameModel>();

                foreach (var link in _urlList)
                {
                    string sportMaarivFile = await Download(link.Value);
                    HtmlDocument htmlFile = new HtmlDocument();
                    htmlFile.LoadHtml(sportMaarivFile);
                    var videoDiv = htmlFile.DocumentNode
                        .Descendants("li")
                        .Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("ContentPlaceHolderMain_ucVideoBrowser_ucVideoBrowserItems_rptVideoItems_liVideoItem"));
                    var linkAndTitle = htmlFile.DocumentNode
                        .Descendants("a")
                        .Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("ContentPlaceHolderMain_ucVideoBrowser_ucVideoBrowserItems_rptVideoItems_ancTitle"));

                    var linkList = linkAndTitle.Select(x => x.Attributes[0].Value).Distinct().ToList();
                    var titleList = linkAndTitle.Select(x => x.InnerText).Distinct().ToList();

                    games.AddRange(linkList
                        .Select((v, i) => MapGame(link.Key, v, titleList[i]))
                        .ToList());
                }

                return new RobotResult<List<Sport5GameModel>>()
                {
                    StartTime = startTime,
                    EndTime = DateTime.Now,
                    NumOfGames = games.Count,
                    Content = games
                };
            }
            catch (Exception c)
            {

                throw;
            }
        }


        private Sport5GameModel MapGame(string league, string game, string title)
        {
            try
            {
                return new Sport5GameModel()
                {
                    LeagueId = 1,
                    Language = "Heb",
                    Link = game,
                    Location = league == "champions league" ? "International" : league.Split('-')[0],
                    Title = title,
                    UploadTime = DateTime.Now
                };
            }
            catch (Exception)
            {

                throw;
            }
        }


        private async Task<string> Download(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            var response = await request.GetResponseAsync();

            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream, Encoding.Default);
            // Read the content.  
            string responseFromServer = await reader.ReadToEndAsync();
            Encoding latinEncoding = Encoding.GetEncoding("Windows-1252");
            Encoding hebrewEncoding = Encoding.GetEncoding("Windows-1255");

            byte[] latinBytes = latinEncoding.GetBytes(responseFromServer);

            string hebrewString = hebrewEncoding.GetString(latinBytes);
            reader.Close();
            response.Close();


            return hebrewString;
        }
    }
}