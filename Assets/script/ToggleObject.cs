using UnityEngine;
using System.Collections;

public class ToggleObject : MonoBehaviour
{

    public GameObject target;

    public float interval = 1f;
    private void Start()
    {
        if (target != null)
           StartCoroutine(ToggleLoop());
        else
            Debug.LogWarning("ToggleObject: target not assigned. ");
    }


    private IEnumerator ToggleLoop()
    {
        while (true)
        {
            target.SetActive(!target.activeSelf);

            yield return new WaitForSeconds(interval);
        }
    }
}
