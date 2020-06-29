using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    private float xOffset;

    void Awake() {
        xOffset = GetComponent<BoxCollider2D>().size.x / 2;
    }

    void Update() {
        float screenLeft = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        if (transform.position.x + xOffset < screenLeft) {
            Destroy(gameObject);
        }
    }
}
