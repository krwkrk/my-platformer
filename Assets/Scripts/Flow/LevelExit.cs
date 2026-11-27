using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.TryGetComponent<PlayerRespawn>(out _)) return;

        triggered = true;
        Flow.NextLevel();
    }
}