using System;

namespace Gameplay
{
    [Serializable]
    public class MoveSpeedConfig
    {
        public float BaseSpeed;
        public float MinSpeed;
        public float MaxSpeed;
        public float AccelerationFactor;
    }
}