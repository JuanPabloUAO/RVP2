using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public SlotUI[] slots;

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Movie_" + i, 0) == 1;

            if (unlocked)
            {
                int score = PlayerPrefs.GetInt("Score_" + i, 0);

                slots[i].SetUnlocked(score);
            }
            else
            {
                slots[i].SetLocked();
            }
        }
    }
}