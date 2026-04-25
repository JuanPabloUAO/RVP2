using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class ImageTrackingHandler : MonoBehaviour
{
    private ARTrackedImageManager manager;
    private bool quizLaunched = false;

    // 🔥 BONUS PRO: evitar detección inmediata al entrar
    private bool canScan = false;

    void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();

        if (manager == null)
        {
            Debug.LogError("❌ ARTrackedImageManager NO encontrado");
        }
    }

    void Start()
    {
        // Espera 2 segundos antes de permitir escaneo
        Invoke(nameof(EnableScanning), 2f);
    }

    void EnableScanning()
    {
        canScan = true;
        Debug.Log("✅ Escaneo habilitado");
    }

    void OnEnable()
    {
        if (manager != null)
        {
            manager.trackablesChanged.AddListener(OnTrackedImagesChanged);
        }
    }

    void OnDisable()
    {
        if (manager != null)
        {
            manager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
        }
    }

    void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        Debug.Log("📸 Evento NUEVO de tracking");

        foreach (var image in args.added)
            ProcessImage(image);

        foreach (var image in args.updated)
            ProcessImage(image);
    }

    void ProcessImage(ARTrackedImage image)
    {
        // 🔴 No escanear si aún no está habilitado
        if (!canScan)
            return;

        if (image.trackingState != TrackingState.Tracking)
            return;

        string imageName = image.referenceImage.name;
        Debug.Log("🟢 DETECTADA: " + imageName);

        // 🚫 Evitar repetir escaneo del mismo poster
        if (PlayerPrefs.GetInt("Scanned_" + imageName, 0) == 1)
        {
            Debug.Log("⚠️ Ya escaneado antes: " + imageName);
            return;
        }

        if (!quizLaunched)
        {
            quizLaunched = true;

            int movieIndex = GetMovieIndex(imageName);

            // ✅ Guardar como escaneado
            PlayerPrefs.SetInt("Scanned_" + imageName, 1);

            Debug.Log("🎬 Cargando película índice: " + movieIndex);

            PlayerPrefs.SetInt("SelectedMovie", movieIndex);

            Invoke(nameof(LoadQuiz), 1f);
        }
    }

    int GetMovieIndex(string name)
    {
        switch (name)
        {
            case "poster1": return 0;
            case "poster2": return 1;
            case "poster3": return 2;
            case "poster4": return 3;
            case "poster5": return 4;
            case "poster6": return 5;
            default:
                Debug.LogWarning("⚠️ Imagen no reconocida: " + name);
                return 0;
        }
    }

    void LoadQuiz()
    {
        SceneManager.LoadScene("QuizScene");
    }
}