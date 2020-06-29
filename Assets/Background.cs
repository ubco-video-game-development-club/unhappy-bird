using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void Start() {
        float screenTop = Camera.main.ScreenToWorldPoint(Screen.height * Vector2.up).y;
        float screenBottom = Camera.main.ScreenToWorldPoint(Vector2.zero).y;
        float screenHeight = screenTop - screenBottom;
        GetComponent<SpriteRenderer>().size = new Vector2(screenHeight, screenHeight);
    }

    void FixedUpdate() {

        Vector2 targetPos = Camera.main.transform.position;
        transform.position = targetPos;
    }
}
