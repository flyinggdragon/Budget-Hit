using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomInfoUI : MonoBehaviour {
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text health;
    [SerializeField] private Player player;

    void Update() {
        level.text = $"Lv. {player.level}";
        health.text = $"{player.health}/{player.maxHealth}";
    }
}
