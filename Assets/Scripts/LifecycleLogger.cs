using UnityEngine;

public class LifecycleLogger : MonoBehaviour
{
    private bool _updateLogged = false;
    private bool _fixedUpdateLogged = false;
    private bool _lateUpdateLogged = false;

    private void Awake()
    {
        Debug.Log("[LifecycleLogger] Awake() викликано");
    }

    private void OnEnable()
    {
        Debug.Log("[LifecycleLogger] OnEnable() викликано");
    }

    private void Start()
    {
        Debug.Log("[LifecycleLogger] Start() викликано");
    }

    private void FixedUpdate()
    {
        if (!_fixedUpdateLogged)
        {
            Debug.Log("[LifecycleLogger] FixedUpdate() викликано (фізичний крок)");
            _fixedUpdateLogged = true;
        }
    }

    private void Update()
    {
        if (!_updateLogged)
        {
            Debug.Log("[LifecycleLogger] Update() викликано (кадр)");
            _updateLogged = true;
        }
    }

    private void LateUpdate()
    {
        if (!_lateUpdateLogged)
        {
            Debug.Log("[LifecycleLogger] LateUpdate() викликано (після Update)");
            _lateUpdateLogged = true;
        }
    }

    private void OnDisable()
    {
        Debug.Log("[LifecycleLogger] OnDisable() викликано");
    }

    private void OnDestroy()
    {
        Debug.Log("[LifecycleLogger] OnDestroy() викликано");
    }
}