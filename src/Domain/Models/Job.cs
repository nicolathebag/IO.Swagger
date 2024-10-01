using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        public bool Equals(Job other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) &&
                (
                    State == other.State ||
                    State != null &&
                    State.Equals(other.State)
                );
        }
    }

    
}
