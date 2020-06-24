using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatSprite : MonoBehaviour
{
    public GameObject prefab;
    public float spawnY = 0;

    private List<GameObject> objects;
    private float objectWidth;
    private float spawnX;
    private int leftIdx;

    void Start() {
        // Get the width of the prefab object
        objectWidth = prefab.GetComponent<BoxCollider2D>().size.x;

        // Get the initial left-most spawn position
        float cameraLeft = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        spawnX = cameraLeft + objectWidth / 2;

        // Spawn the game objects to fit the screen
        float cameraRight = Camera.main.ScreenToWorldPoint(Screen.width * Vector2.right).x;
        float screenWidth = cameraRight - cameraLeft;
        int count = Mathf.CeilToInt(screenWidth / objectWidth) + 1;
        objects = new List<GameObject>();
        for (int i = 0; i < count; i++) {
            Vector2 spawnPos = new Vector2(spawnX, spawnY);
            objects.Add(Instantiate(prefab, spawnPos, Quaternion.identity, transform));
            spawnX += objectWidth;
        }

        leftIdx = 0;
    }

    void Update() {
        // Move the left-most game object to the right when it goes offscreen
        float cameraLeft = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        if (objects[leftIdx].transform.position.x < cameraLeft - objectWidth / 2) {
            Vector2 spawnPos = new Vector2(spawnX, spawnY);
            objects[leftIdx].transform.position = spawnPos;
            spawnX += objectWidth;

            leftIdx = (leftIdx + 1) % objects.Count;
        }
    }
}
