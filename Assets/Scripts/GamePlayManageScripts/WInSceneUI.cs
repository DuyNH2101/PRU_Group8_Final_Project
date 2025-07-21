using TMPro;
using UnityEngine;

public class WInSceneUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        int score = GameSessionScript.instance.stageScore;
        scoreText.text = "Score: " + score;
    }
}
