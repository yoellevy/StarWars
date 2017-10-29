using UnityEngine;
using System.Collections.Generic;
using StarWars.Actions;
using Infra.Utils;

namespace StarWars.Brains
{
    public class UnderStressSpaceShipBrain : SpaceshipBrain
    {
        public override string DefaultName
        {
            get { return "StressShip"; }
        }

        public override Color PrimaryColor
        {
            get { return Color.magenta; }
        }

        public override SpaceshipBody.Type BodyType
        {
            get
            {
                return SpaceshipBody.Type.XWing;
            }
        }

        public override Action NextAction()
        {
            Spaceship nearest = null;
            var minDis = float.MaxValue;

            foreach(var ship in Space.Spaceships)
            {
                if (!ship.IsAlive) continue;
                var dis = spaceship.ClosestRelativePosition(ship).magnitude;
                if((nearest == null || dis < minDis) && (ship != spaceship))
                {
                    nearest = ship; //muhahahaha
                    minDis = dis;
                }
            }

            if(nearest != null && minDis < 3f)
            {
                if (spaceship.CanRaiseShield)
                    return ShieldUp.action;
            }

            if (spaceship.CanShoot)
                return Shoot.action;

            return TurnLeft.action;
        }
    }
}
