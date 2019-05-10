using System;
using System.Collections.Generic;
using repack.Entities;

namespace repack.ViewModels
{
    public class LogViewModel : BaseViewModel
    {
        public string Type { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        
        public List<SystemLog> SystemLogs { get; set; }
        

        public LogViewModel()
        {
            GlobalMenu = "log";
        }
    }
}