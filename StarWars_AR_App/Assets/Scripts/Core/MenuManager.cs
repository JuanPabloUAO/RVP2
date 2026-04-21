using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelNombre;
    public GameObject panelMenu;
    public GameObject panelConfirmExit;

    [Header("Input")]
    public InputField inputNombre;

    void Start()
    {
        string savedName = PlayerPrefs.GetString("PlayerName", "");

        // Menú siempre visible (fondo)
        panelMenu.SetActive(true);
        panelConfirmExit.SetActive(false);

        if (string.IsNullOrEmpty(savedName))
        {
            panelNombre.SetActive(true);
        }
        else
        {
            panelNombre.SetActive(false);
        }
    }

    // =========================
    // GUARDAR NOMBRE
    // =========================
    public void GuardarNombre()
    {
        string nombre = inputNombre.text;

        if (string.IsNullOrEmpty(nombre))
            return;

        PlayerPrefs.SetString("PlayerName", nombre);
        PlayerPrefs.Save();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerName = nombre;
        }

        panelNombre.SetActive(false);
    }

    // =========================
    // BOTONES MENU
    // =========================
    public void GoToAR()
    {
        SceneManager.LoadScene("ARScene");
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void GoToProgress()
    {
        SceneManager.LoadScene("ProgressScene");
    }

    // =========================
    // SALIDA
    // =========================
    public void OpenExitPanel()
    {
        panelConfirmExit.SetActive(true);
    }

    public void CancelExit()
    {
        panelConfirmExit.SetActive(false);
    }

    public void ConfirmExit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}