﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArtistPalace.Data;
using Microsoft.AspNetCore.Mvc;
using ArtistPalace.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ArtistPalace.TwitterApi;
using ArtistPalace.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ArtistPalace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ConnectionFactory connection, ILogger<HomeController> logger)
        {
            _connectionFactory = connection;
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var suggestArtists = connection.Query<SuggestArtists>("select top 5 * from SuggestArtists order by Id desc ").ToList();
                return View(suggestArtists);
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        private string GetRank(int followersCount)
        {
            if (followersCount > 74999)
            {
                return "S";
            }
            else if (followersCount > 24999)
            {
                return "A";
            }
            else if (followersCount > 1000)
            {
                return "B";
            }
            else if (followersCount < 1000)
            {
                return "C";
            }

            return null;
        }
    }
}