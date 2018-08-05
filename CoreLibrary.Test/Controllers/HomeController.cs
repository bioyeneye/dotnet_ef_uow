using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.AutoFac;
using Microsoft.AspNetCore.Mvc;
using CoreLibrary.Test.Models;
using CoreLibrary.Test.Repository;
using CoreLibrary.Test.Service;

namespace CoreLibrary.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMeetingService _meetingService;
        public HomeController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }
        public IActionResult Index()
        {
            var personalInfoRepository = EngineContext.Resolve<IMeetingRepository>();
            var data = personalInfoRepository.Table;
            _meetingService.Create(new MeetingModel
            {
                ProjectName = "Project Name",
                GroupMeetingDate = DateTime.Now,
                TeamLeadName = "Team Lead name",
                Description = "Description",
                GroupMeetingLeadName = "Group Meeting Lead Name"
            }, "");
            //var datas = _meetingService.GetCount(1, 10, null, null);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
