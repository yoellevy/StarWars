// Author: Yoel & Alicia
using UnityEngine;
using System.Collections.Generic;
using StarWars.Actions;
using Infra.Utils;

namespace StarWars.Brains
{
    public class TickBrain : SpaceshipBrain
    {

        float safeDis = 3f;


        public override string DefaultName
        {
            get
            {
                return "TickShipSpaceship";
            }
        }

        public override Color PrimaryColor
        {
            get
            {
                return Color.black;
            }
        }

        public override SpaceshipBody.Type BodyType
        {
            get
            {
                return SpaceshipBody.Type.TieFighter;
            }
        }

        public override Action NextAction()
        {
            foreach (var ship in Space.Spaceships)
            {
                if (ship != spaceship && ship.IsAlive)
                {
                    if (spaceship.ClosestRelativePosition(ship).magnitude <= safeDis)
                    {
                        if (spaceship.CanRaiseShield)
                        {
                            return ShieldUp.action;
                        }
                    }
                }
            }

            Spaceship farest = null;
            var minDistance = 0f;
            foreach (var ship in Space.Spaceships)
            {
                if (!ship.IsAlive) continue;
                var distance = spaceship.ClosestRelativePosition(ship).magnitude;
                if ((farest == null || distance < minDistance) && (ship != spaceship))
                {
                    farest = ship;
                    minDistance = distance;
                }
            }


            if (farest == null)
            {
                return DoNothing.action;
            }

            var angle = farest.ClosestRelativePosition(spaceship).GetAngle(spaceship.Forward);
            if (farest.ClosestRelativePosition(spaceship).magnitude <= 10f && spaceship.CanShoot)
                return Shoot.action;

            if (angle <= 10f)
            {
                return TurnLeft.action;
            }
            else if (angle >= -10f)
            {
                return TurnRight.action;
            }
            else if (spaceship.CanShoot)
            {
                return Shoot.action;
            }

            return DoNothing.action;
        }
    }

}