using System.Collections.Generic;
using repack.Entities;

namespace repack.ViewModels
{
    public class StackViewModel : BaseViewModel
    {
        public Stack Stack { get; set; } 
        public List<Stack> Stacks { get; set; }
        
        public bool Delete { get; set; }
    }
}