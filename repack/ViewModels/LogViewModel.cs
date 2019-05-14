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

        public int Page { get; set; } = 1;
        public SearchResult<SystemLog> SearchResult { get; set; }
        

        public LogViewModel()
        {
            GlobalMenu = "log";
        }
    }
}