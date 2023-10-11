using UnityEngine;

namespace Runner.Level.Platform
{
    /// <summary>
    /// Тайл платформы
    /// </summary>
    public class PlatformTile : MonoBehaviour
    {
        public Vector3 GetSize() => transform.localScale;
    }
}