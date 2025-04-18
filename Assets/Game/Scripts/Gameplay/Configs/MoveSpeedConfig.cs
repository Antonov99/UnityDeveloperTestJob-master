using UnityEngine;

namespace Gameplay
{
    public class MoveSpeedConfig : ScriptableObject
    {
        public float BaseSpeed { get; set; }
        public float MinSpeed { get; set; }
        public float MaxSpeed { get; set; }
        public float AccelerationFactor { get; set; }
    }
}