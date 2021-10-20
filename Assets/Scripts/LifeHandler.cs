using UnityEngine;

public class LifeHandler : MonoBehaviour
{
    [SerializeField] int startingHearts = 3;
    [SerializeField] float heartSize = 21;
    [SerializeField] RectTransform currentHearts = null;
    [SerializeField] RectTransform heartsFrame = null;
    [SerializeField] ScoreUpdater score = null;

    private void Awake() { SetHeartsSize(startingHearts, true); }

    public void SetHeartsSize(float heartsToAdd, bool adjustFrame)
    {
        Vector2 newSize = currentHearts.sizeDelta;
        newSize.x += heartsToAdd * heartSize;
        currentHearts.sizeDelta = newSize;

        if (adjustFrame) { heartsFrame.sizeDelta = newSize; }

        if (Mathf.Approximately(newSize.x, 0) || newSize.x < 0)
        {
            score.EndGame();
        }
    }
}
