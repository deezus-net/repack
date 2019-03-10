using System.Collections.Generic;
using repack.Entities;

namespace repack.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        public int StackId { get; set; }
        
        public int Id { get; set; }
        public Task Task { get; set; } 
        public List<Task> Tasks { get; set; }
        
        public TaskContent TaskContent { get; set; }
        
        public List<SentLog>Logs { get; set; }
        
        public bool Delete { get; set; }
    }
}