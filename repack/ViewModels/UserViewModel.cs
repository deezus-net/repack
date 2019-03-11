using System.Collections.Generic;
using repack.Entities;

namespace repack.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public User User { get; set; }
        public List<User>Users { get; set; }
        
        public bool Delete { get; set; }
    }
}