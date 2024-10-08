﻿using BE_AgentGuard.Interface;
using BE_AgentGuard.Models;
using BE_AgentGuard.RouteModel;
using System.Drawing;

namespace BE_AgentGuard.FuncMove
{
    public class Move : ImapHandling
    {
        public RouteModel.Point _point;
        public IPerson person;
        public Move(RouteModel.Point point, IPerson person)
        {
            _point = point;
            this.person = person;
        }
        public IPerson  ChangeFree(Directions directions)
        {
            if(directions.direction is null) { return person; }
            if (directions.direction.Contains("n"))
            {
                _point.Y++;
            }
            if (directions.direction.Contains("s"))
            {
                _point.Y--;
            }
            if (directions.direction.Contains("e"))
            {
                _point.X++;
            }
            if (directions.direction.Contains("w"))
            {
                _point.X--;
            }
            if(_point.X <= 0 || _point.Y <= 0 || _point.X > 25 || _point.Y >= 25) { return person; }
            person.point = _point;
            return person;
        }
        public bool Start()
        {
            if (person.point.OnTheMap)
            {
                return false;
            }
            person.point = _point;
            return true;
        }

        public void ChangeToTarget(Target target) 
        {
            
        }

    }
}
