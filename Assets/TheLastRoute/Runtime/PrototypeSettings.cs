using UnityEngine;

namespace TheLastRoute
{
    [CreateAssetMenu(menuName = "The Last Route/Prototype Settings")]
    public sealed class PrototypeSettings : ScriptableObject
    {
        [Min(0.1f)] public float movementSpeed = 3f;
        [Min(0.1f)] public float interactionRange = 1.8f;
        [Min(1f)] public float uvDuration = 3f;
        [Min(1f)] public float mirrorDuration = 5f;
        [Min(0f)] public float uvDrainPerSecond = 1f;
        [Min(0f)] public float flashlightDrainPerSecond = 1f;
        [Min(1f)] public float maxBattery = 100f;
        [Min(1f)] public float maxPower = 100f;
    }
}
