using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float bottomOffset = 2.5f;

    void Start() {
        float screenTop = Camera.main.ScreenToWorldPoint(Screen.height * Vector2.up).y;
        float screenBottom = Camera.main.ScreenToWorldPoint(Vector2.zero).y + bottomOffset;
        float screenHeight = screenTop - screenBottom;
        GetComponent<SpriteRenderer>().size = new Vector2(screenHeight, screenHeight);
    }

    void FixedUpdate() {

        Vector2 cameraPos = Camera.main.transform.position;
        transform.position = cameraPos + (bottomOffset / 2 * Vector2.up);
    }
}
