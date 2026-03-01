using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Models
{
    public class AgentDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
