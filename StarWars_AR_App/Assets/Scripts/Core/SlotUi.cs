using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image poster;
    public Text titleText;
    public Text scoreText;
    public GameObject lockedOverlay;

    public Sprite posterImage;
    public string movieTitle;

    public void SetUnlocked(int score)
    {
        poster.sprite = posterImage;
        titleText.text = movieTitle;

        scoreText.text = "Score: " + score + "/10";

        lockedOverlay.SetActive(false);
    }

    public void SetLocked()
    {
        titleText.text = "???";
        scoreText.text = "";

        lockedOverlay.SetActive(true);
    }
}