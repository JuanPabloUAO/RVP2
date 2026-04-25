using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}