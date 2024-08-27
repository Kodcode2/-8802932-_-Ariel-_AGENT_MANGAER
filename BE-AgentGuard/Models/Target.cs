

using BE_AgentGuard.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_AgentGuard.Models
{
    public class Target : IPerson
    {
        public string name { get; set; }
        public string position { get; set; }
        public Target() { is_active = true; }
    }
}
