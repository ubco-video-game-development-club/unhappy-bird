using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject topPipePrefab;
    public GameObject bottomPipePrefab;
    public GameObject checkpointPrefab;
    public float pipeSpawnGapDistance = 5f;
    public float pipeWidth = 1.5f;
    public float pipeGapHeight = 3f;
    public float maxTopPipeOffset = 0.5f;
    public float minBottomPipeOffset = 2.5f;
    public float checkpointWidth = 0.1f;

    private bool isSpawning;
    private float screenTop;
    private float screenBottom;
    private float prevSpawnX;
    private int score;
    private int bestScore;

    private const string savePath = "Assets/Resources/Best.txt";

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Start() {
        isSpawning = false;
        screenTop = Camera.main.ScreenToWorldPoint(Screen.height * Vector2.up).y;
        screenBottom = Camera.main.ScreenToWorldPoint(Vector2.zero).y;
        bestScore = LoadBestScore();
        HUD.instance.SetBestScore(bestScore);
    }

    public void AddScore() {
        score++;
        HUD.instance.SetScore(score);
    }

    public void StartGame() {
        HUD.instance.StartGame();
        StartCoroutine(SpawnPipes());
    }

    public void EndGame() {
        HUD.instance.EndGame();
        if (score > bestScore) {
            bestScore = score;
            HUD.instance.SetBestScore(bestScore);
            HUD.instance.HighlightScores();
            SaveBestScore();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator SpawnPipes() {
        prevSpawnX = 0;
        isSpawning = true;
        while (isSpawning) {
            // Get the current camera position
            float currentX = Camera.main.transform.position.x;
            if (currentX - prevSpawnX > pipeSpawnGapDistance) {
                // Spawn pipe if we've passed a certain gap distance
                SpawnPipe();
                prevSpawnX = currentX;
            }
            yield return null;
        }
    }

    private void SpawnPipe() {
        float screenRight = Camera.main.ScreenToWorldPoint(Screen.width * Vector2.right).x;
        float spawnPositionX = screenRight + (pipeWidth / 2);

        // Get a random gap position
        float minGapPositionY = (screenBottom + minBottomPipeOffset) + (pipeGapHeight / 2);
        float maxGapPositionY = (screenTop - maxTopPipeOffset) - (pipeGapHeight / 2);
        float gapPositionY = Random.Range(minGapPositionY, maxGapPositionY);

        // Spawn the top pipe
        float topPipeOffset = gapPositionY + (pipeGapHeight / 2);
        float topPipeHeight = screenTop - topPipeOffset;
        float topPipePositionY = topPipeOffset + topPipeHeight / 2;
        Vector2 topPipeSpawnPos = new Vector2(spawnPositionX, topPipePositionY);
        GameObject topPipe = Instantiate(topPipePrefab, topPipeSpawnPos, Quaternion.identity);
        topPipe.GetComponent<SpriteRenderer>().size = new Vector2(pipeWidth, topPipeHeight);
        topPipe.GetComponent<BoxCollider2D>().size = new Vector2(pipeWidth, topPipeHeight);

        // Spawn the bottom pipe
        float bottomPipeOffset = gapPositionY - (pipeGapHeight / 2);
        float bottomPipeHeight = bottomPipeOffset - screenBottom;
        float bottomPipePositionY = bottomPipeOffset - bottomPipeHeight / 2;
        Vector2 bottomPipeSpawnPos = new Vector2(spawnPositionX, bottomPipePositionY);
        GameObject bottomPipe = Instantiate(bottomPipePrefab, bottomPipeSpawnPos, Quaternion.identity);
        bottomPipe.GetComponent<SpriteRenderer>().size = new Vector2(pipeWidth, bottomPipeHeight);
        bottomPipe.GetComponent<BoxCollider2D>().size = new Vector2(pipeWidth, bottomPipeHeight);

        // Spawn the checkpoint between pipes
        float checkpointHeight = topPipeOffset - bottomPipeOffset;
        Vector2 checkpointSpawnPos = new Vector2(spawnPositionX, gapPositionY);
        GameObject checkpoint = Instantiate(checkpointPrefab, checkpointSpawnPos, Quaternion.identity);
        bottomPipe.GetComponent<BoxCollider2D>().size = new Vector2(checkpointWidth, checkpointHeight);
    }

    private void SaveBestScore() {
        StreamWriter writer = new StreamWriter(savePath);
        writer.Write(score);
        writer.Close();
    }

    private int LoadBestScore() {
        StreamReader reader = new StreamReader(savePath);
        int best;
        if (!System.Int32.TryParse(reader.ReadLine(), out best)) {
            best = 0;
        }
        reader.Close();
        return best;
    }
}
