using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Lava : MonoBehaviour
{
    [Header("Damage")]
    public float damagePerSecond = 20f;

    [Header("Filter")]
    public string requiredTag = "player";

    private readonly HashSet<IDamageable> inside = new HashSet<IDamageable>();

    private readonly Dictionary<IDamageable, GameObject> damageableToGameObject = new Dictionary<IDamageable,GameObject>();

    void Awake()
    {
        var col = GetComponent<Collider>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"{name}: Collider is not a trigger.Setting IsTrigger = true.");
            col.isTrigger = true;
        }
    }

    void Update()
    {
        if (inside.Count == 0) return;

        float dmg = damagePerSecond * Time.deltaTime;
        var copy = new List<IDamageable>(inside);
        foreach (var d in copy)
        {
            d.TakeDamage(dmg);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(requiredTag) && other.gameObject.tag == requiredTag)
           return;
        
        var dmg = other.GetComponentInParent<IDamageable>();
        if (dmg != null && !inside.Contains(dmg))
        {
            inside.Add(dmg);
            damageableToGameObject[dmg] = other.gameObject;

        }
    }
    void OnTriggerExit(Collider other)
    {
        var dmg = other.GetComponentInParent<IDamageable>();
        if (dmg != null && inside.Contains(dmg))
        {
            inside.Remove(dmg);
            damageableToGameObject.Remove(dmg);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var col = GetComponent<Collider>();
        if (col is BoxCollider box)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(box.center,box.size);
        }
        else
        {
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}

