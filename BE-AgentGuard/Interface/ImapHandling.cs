using BE_AgentGuard.Models;
using BE_AgentGuard.RouteModel;

namespace BE_AgentGuard.Interface
{
    public interface ImapHandling
    {
        public IPerson ChangeFree(Directions direction);
        public void ChangeToTarget(Target target );        
        public bool Start();
    }
}
