using UnityEngine;

public class SmoothDampLooper : MonoBehaviour
{
    public Vector3 offset = new Vector3(3f,0f,0f);
    public float smoothTime = 0.3f;
    public bool useLocal = true;

    Vector3 start;
    Vector3 target;
    Vector3 velocity;
    void Start()
    {
        start = useLocal ? transform.localPosition : transform.position;
        target = start + offset;
    }

    void Update()
    {
        Vector3 current = useLocal ? transform.localPosition : transform.position;
        Vector3 newPos = Vector3.SmoothDamp(current,target,ref velocity,smoothTime);
        if (useLocal) transform.localPosition = newPos;
        else transform.position = newPos;

        if ((newPos - target).sqrMagnitude < 0.001f)
        {
            target = (target == start) ? start + offset : start;
        }
    }
}
