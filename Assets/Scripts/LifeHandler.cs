using UnityEngine;

public class LifeHandler : MonoBehaviour
{
    [SerializeField] float heartSize = 21;
    [SerializeField] RectTransform currentHearts = null;
    [SerializeField] ScoreUpdater score = null;

    public void SetHeartsSize(float heartsToAdd)
    {
        Vector2 newSize = currentHearts.sizeDelta;
        newSize.x += heartsToAdd * heartSize;
        currentHearts.sizeDelta = newSize;

        if (Mathf.Approximately(newSize.x, 0) || newSize.x < 0)
        {
            score.EndGame();
        }
    }
}
