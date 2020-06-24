using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public bool freezePlayer = false;

    void OnTriggerEnter2D(Collider2D col) {
        Player player;
        if (col.TryGetComponent<Player>(out player)) {
            player.Die();
            if (freezePlayer) {
                player.Freeze();
            }
        }
    }
}
