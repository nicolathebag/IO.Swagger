using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Job
    {
        public string Id { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        public Job(string id, string State)
        {
            this.Id = id;
            this.State = State;
        }
    }

    
}
