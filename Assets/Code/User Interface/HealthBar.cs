using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHealth(float healthNormalized)
    {
        health.transform.localScale = new Vector3(healthNormalized, 1f);
    }

    public IEnumerator SetHealthSmooth(float newHealth)
    {
        float currentHealth = health.transform.localScale.x;
        float changedAmount = currentHealth - newHealth;

        while (currentHealth - newHealth > Mathf.Epsilon)
        {
            currentHealth -= changedAmount * Time.deltaTime * 3;
            health.transform.localScale = new Vector3(currentHealth, 1f);
            yield return null;
        }
        health.transform.localScale = new Vector3(newHealth, 1f);
    }

    public IEnumerator SetDrainHealthSmooth(float newHealth)
    {
        float currentHealth = health.transform.localScale.x;
        float changedAmount = newHealth - currentHealth; /// Should be the same as the one above

        while (newHealth - currentHealth > Mathf.Epsilon)
        {
            currentHealth += changedAmount * Time.deltaTime * 3;
            health.transform.localScale = new Vector3(currentHealth, 1f);
            yield return null;
        }
        health.transform.localScale = new Vector3(newHealth, 1f);
    }
}

