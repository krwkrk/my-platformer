using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.3f)] private float defaultDuration = 0.08f;
    [SerializeField, Range(0.01f, 0.5f)] private float defaultMagnitude = 0.08f;

    private Vector3 originalPos;
    private float timer;
    private float currentMagnitude;

    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    public void Shake(float duration = -1f, float magnitude = -1f)
    {
        timer = duration > 0f ? duration : defaultDuration;
        currentMagnitude = magnitude > 0f ? magnitude : defaultMagnitude;
    }

    private void LateUpdate()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            Vector2 offset = Random.insideUnitCircle * currentMagnitude;
            transform.localPosition = originalPos + new Vector3(offset.x, offset.y, 0f);

            if (timer <= 0f)
                transform.localPosition = originalPos;
        }
    }
}