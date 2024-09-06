using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image healthBar;
    public float maxHealth;
    public float health;
    float lerpSpeed;

    void Update() {
        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
        healthBar.color = Color.Lerp(Color.red, Color.green, (health / maxHealth));
    }

    public void Damage(float damageReceived) {
        if (health > 0) {
            health -= damageReceived;
        }
    }
}