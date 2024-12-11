using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image healthBar;
    public float maxHealth { get; set; }
    public float health { get; set; }
    public float lerpSpeed;
    public ICharacter target;

    void Start() {
        target = GetComponentInParent<ICharacter>() ?? FindFirstObjectByType<Player>();
        
        maxHealth = target.maxHealth;
        health = target.health;
    }

    void Update() {
        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
        healthBar.color = Color.Lerp(Color.red, Color.cyan, health / maxHealth);

        transform.LookAt(GameObject.Find("Player").transform);
    }

    public void Damage(float damageReceived) {
        if (health > 0) {
            health -= damageReceived;
        }
    }
}