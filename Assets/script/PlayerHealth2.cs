using UnityEngine;

public class PlayerHealth2 : MonoBehaviour, IDamageable
{
    [Header("Health")]

    public float maxHealth = 100f;

    public float health = 100f;

    [Header("Feedback")]

    public float damageFlashDuration = 0.15f;

    Renderer[] renderers;

    Color[] originalColors;

    void Start()
    {
        health = maxHealth;
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        originalColors[i] = renderers[i].material.color;
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0) return;
        health -= amount;
        Debug.Log($"Player took {amount:F1} damage.Health = {health:F1}");

        if (renderers != null && renderers.Length > 0)
           StartCoroutine(DamageFlash());
        if (health <= 0f) Die();
    }
    System.Collections.IEnumerator DamageFlash()
    {
        foreach (var r in renderers)
           r.material.color = Color.red;
           yield return new WaitForSeconds(damageFlashDuration);
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];
    }

    void Die()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(false);
    }
}
