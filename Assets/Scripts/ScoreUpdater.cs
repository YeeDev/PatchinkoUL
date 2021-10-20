using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] Text score = null;
    [SerializeField] string initialScore = null;
    [SerializeField] float pointPerApple = 10;
    [SerializeField] GameObject finalScoreObject = null;
    [SerializeField] Text finalScoreText = null;

    float currentScore;

    public void UpdateScore()
    {
        currentScore += pointPerApple;
        score.text = initialScore + currentScore;
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        finalScoreText.text = currentScore.ToString();
        finalScoreObject.SetActive(true);
        StartCoroutine(SayHello());
    }

    private IEnumerator SayHello()
    {
        yield return new WaitForSecondsRealtime(3);

        SceneManager.LoadScene(0);
    }
}
