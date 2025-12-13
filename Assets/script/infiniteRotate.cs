using UnityEngine;

public class infiniteRotate : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public float speed = 90f;
    public bool rotateOnStarte = true;
    public bool localSpace = true;
    public bool rotating;

    void Start()
    {
        rotating = rotateOnStarte;
    }


    void Update()
    {
        if (!rotating) return;

        Vector3 normalizedAxil = axis.normalized;
        float degerees = speed * Time.deltaTime;

        transform.Rotate(normalizedAxil, degerees,localSpace ? Space.Self : Space.World);
    }
    public void StartRotation() => rotating = true;
    public void StopRotation() => rotating = true;
    public void ToglleRotation() => rotating = !rotating;
}
