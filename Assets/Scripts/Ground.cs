using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject groundPrefab;

    private List<GameObject> groundObjects;
    private float groundObjectWidth;
    private float spawnX;
    private float spawnY;
    private int leftIdx;

    void Start() {
        // Get the width of the prefab object
        groundObjectWidth = groundPrefab.GetComponent<SpriteRenderer>().size.x;

        // Get the initial left-most spawn position
        float cameraLeft = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        spawnX = cameraLeft + groundObjectWidth / 2;

        // Get the vertical spawn position
        float cameraBottom = Camera.main.ScreenToWorldPoint(Vector2.zero).y;
        float groundObjectHeight = groundPrefab.GetComponent<SpriteRenderer>().size.y;
        spawnY = cameraBottom + groundObjectHeight / 2;

        // Spawn the game objects to fit the screen
        float cameraRight = Camera.main.ScreenToWorldPoint(Screen.width * Vector2.right).x;
        float screenWidth = cameraRight - cameraLeft;
        int count = Mathf.CeilToInt(screenWidth / groundObjectWidth) + 1;
        groundObjects = new List<GameObject>();
        for (int i = 0; i < count; i++) {
            Vector2 spawnPos = new Vector2(spawnX, spawnY);
            groundObjects.Add(Instantiate(groundPrefab, spawnPos, Quaternion.identity, transform));
            spawnX += groundObjectWidth;
        }

        leftIdx = 0;
    }

    void Update() {
        // Move the left-most game object to the right when it goes offscreen
        float cameraLeft = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        if (groundObjects[leftIdx].transform.position.x < cameraLeft - groundObjectWidth / 2) {
            Vector2 spawnPos = new Vector2(spawnX, spawnY);
            groundObjects[leftIdx].transform.position = spawnPos;
            spawnX += groundObjectWidth;

            leftIdx = (leftIdx + 1) % groundObjects.Count;
        }
    }
}
