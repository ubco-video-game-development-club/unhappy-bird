using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public CanvasGroup startMenu;
    public CanvasGroup scoreMenu;
    public CanvasGroup gameOverMenu;
    public Text currentScoreText;
    public Text finalScoreText;
    public Text bestScoreText;
    public Color highlightColor;
    public float fadeTime = 1f;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public void SetScore(int score) {
        currentScoreText.text = "" + score;
        finalScoreText.text = "" + score;
    }

    public void SetBestScore(int bestScore) {
        bestScoreText.text = "" + bestScore;
    }

    public void HighlightScores() {
        finalScoreText.color = highlightColor;
        bestScoreText.color = highlightColor;
    }

    public void StartGame() {
        EnableMenu(startMenu, false);
        EnableMenu(scoreMenu, true);
    }

    public void EndGame() {
        EnableMenu(scoreMenu, false);
        EnableMenu(gameOverMenu, true);
    }

    private void EnableMenu(CanvasGroup menu, bool enabled) {
        StartCoroutine(FadeMenu(menu, enabled));
        menu.blocksRaycasts = enabled;
        menu.interactable = enabled;
    }

    private IEnumerator FadeMenu(CanvasGroup menu, bool fadeIn) {
        // get the initial alpha of the menu
        float startAlpha = menu.alpha;
        
        // determine whether we are fading in or out
        float targetAlpha = fadeIn ? 1 : 0;

        // fade the menu over fadeTime seconds
        float f = 0;
        while (f < fadeTime) {
            f += Time.deltaTime;
            menu.alpha = Mathf.Lerp(startAlpha, targetAlpha, f / fadeTime);
            yield return null;
        }
    }
}
