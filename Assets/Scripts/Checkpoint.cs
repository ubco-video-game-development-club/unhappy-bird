using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        Player player;
        if (col.TryGetComponent<Player>(out player)) {
            GameController.instance.AddScore();
            Destroy(gameObject);
        }
    }
}
