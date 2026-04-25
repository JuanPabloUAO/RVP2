using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{

    void Start()
    {
        string result = "";

        foreach (MovieID movie in System.Enum.GetValues(typeof(MovieID)))
        {
            if (GameManager.Instance.IsCompleted(movie))
            {
                int score = GameManager.Instance.GetScore(movie);
                result += movie + ": " + score + "/10\n";
            }
            else
            {
                result += movie + ": ❌ No jugado\n";
            }
        }

    }
}