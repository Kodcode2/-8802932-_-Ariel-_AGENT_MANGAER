using BE_AgentGuard.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_AgentGuard.Models
{
    public class Agent : IPerson
    {
        public string nickname { get; set; }
    } 
}
    

