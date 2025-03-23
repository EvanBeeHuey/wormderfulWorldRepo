using TMPro;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public TMP_Text livesText;
    public TMP_Text collectText;

    private void Start()
    {
        livesText.text = $"Lives: {GameManager.Instance.lives}";
        collectText.text = $"Eyes Collected: {GameManager.Instance.score}";

        GameManager.Instance.OnLifeValueChanged.AddListener(LifeValueChanged);
        GameManager.Instance.OnScoreValueChanged.AddListener(ScoreValueChanged);

        LifeValueChanged(GameManager.Instance.lives);
        ScoreValueChanged(GameManager.Instance.score);
    }

    private void LifeValueChanged(int value) => livesText.text = $"Lives: {value}";

    private void ScoreValueChanged(int value) => collectText.text = $"Eyes Collected: {value}";
}
