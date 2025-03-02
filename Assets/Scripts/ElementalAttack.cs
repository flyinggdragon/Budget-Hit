using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalAttack : MonoBehaviour {
    [SerializeField] public Element element;
    [SerializeField] public bool isBurst;
    [SerializeField] public BoxCollider boxCollider;
    public bool hasHit { get; set; } = false;
    private Player player;
    private ElementalReaction? reaction;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(LifetimeCoroutine());
    }

    public void HandleHit(Enemy enemy, bool isReaction, Element? reactingWith = null) {
        if (hasHit) { return; }

        AttackType attackType = isBurst ? AttackType.Burst : AttackType.ElementalSkill;

        if (!isReaction || reactingWith == null) {
            float dmg = Damage.CalculateDamage(
                attackType,
                false,
                element,
                null,
                player.baseATK,
                player.critRate,
                player.critDMG,
                player.proficiency
            );

            Debug.Log("Non-Reaction DMG: " + dmg);
            enemy.GetHit(dmg, element);
        } 
        
        else {
            reaction = new ElementalReaction(element, reactingWith.Value);

            float dmg = Damage.CalculateDamage(
                attackType,
                true,
                element,
                reaction.reactingWith,
                player.baseATK,
                player.critRate,
                player.critDMG,
                player.proficiency
            );

            Debug.Log($"Reaction of {element} and {reactingWith}: {dmg}");
            enemy.GetHit(dmg, element);
            reaction = null;
        }

        hasHit = true;
    }

    private IEnumerator LifetimeCoroutine() {
        yield return new WaitForSeconds(6f);

        DestroySelf();
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }
}