
using System;

namespace LuckyDucky.MVC.Models
{
    public class ProjectSettingsModel
    {
        public DateTime NextLotteryUtcDateTime { get; set; }

        public string LotteryAdminSecret { get; set; }
}
}