using System.Collections.Generic;

namespace repack.ViewModels
{
    public class BaseViewModel
    {
        public string BaseURL { get; set; }
        public string GlobalMenu { get; set; }

        public Dictionary<string, List<string>> ErrorMessages { get; set; } = new Dictionary<string, List<string>>();
    }
}