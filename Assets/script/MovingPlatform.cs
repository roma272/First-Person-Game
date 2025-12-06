using UnityEngine;

[DisallowMultipleComponent]

public class MovingPlatform : MonoBehaviour
{

    public Vector3 Velocity { get; private set; } = Vector3.zero;

    Vector3 previosPosition;

    void Start()
    {
        previosPosition = transform.position;
    }

    void Update()
    {
        Vector3 current = transform.position;
        if (Time.deltaTime > 0f)
            Velocity = (current - previosPosition) / Time.deltaTime;
        else
            Velocity = Vector3.zero;
        previosPosition = current;
    }
}
