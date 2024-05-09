using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that moves an object from initial to point A interpolatively
/// </summary>
public class PlatformMovement : MonoBehaviour
{
    public float upTime = 5;
    public float downTime = 5;
    public float speed = 0.1f; // Movement speed
    public Vector3 targetPosition; // Target position for the GameObject

    private Vector3 originalPosition;
    private Vector3 _originalPosition;
    [SerializeField] private float _currentStateTime;
    private float sinTime;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        targetPosition = gameObject.transform.position + new Vector3(0, -20, 0);
        _currentStateTime = downTime;
        _originalPosition = originalPosition;
    }

    private void Update()
    {
        if (_currentStateTime > 0)
        {
            _currentStateTime -= Time.deltaTime;
            return;
        }
        if (transform.position != targetPosition)
        {
            sinTime += Time.deltaTime * speed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = Evaluate(sinTime);
            transform.position = Vector3.Lerp(transform.position, targetPosition, t);
        }
        Swap();
    }

    public void Swap()
    {
        if (transform.position != targetPosition)
        {
            return;
        }
        (targetPosition, _originalPosition) = (_originalPosition, targetPosition);
        if (targetPosition == originalPosition)
        {
            _currentStateTime = downTime;
        } else
        {
            _currentStateTime = upTime;
        }
        sinTime = 0;
    }
    public float Evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f;
    }
}
