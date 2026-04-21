using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class ImageTrackingHandler : MonoBehaviour
{
    private ARTrackedImageManager manager;
    private bool quizLaunched = false;

    void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();

        if (manager == null)
        {
            Debug.LogError("No se encontró ARTrackedImageManager en este objeto.");
        }
    }

    void OnEnable()
    {
        if (manager != null)
            manager.trackedImagesChanged += OnChanged;
    }

    void OnDisable()
    {
        if (manager != null)
            manager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        if (quizLaunched) return;

        foreach (var image in args.added)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                quizLaunched = true;

                string imageName = image.referenceImage.name;
                Debug.Log("Imagen detectada: " + imageName);

                MovieID movie = GetMovieFromImage(imageName);

                PlayerPrefs.SetInt("SelectedMovie", (int)movie);

                Invoke("LoadQuiz", 0.5f);
                break;
            }
        }
    }

    void LoadQuiz()
    {
        SceneManager.LoadScene("QuizScene");
    }

    MovieID GetMovieFromImage(string name)
    {
        switch (name)
        {
            case "poster1": return MovieID.Episode1;
            case "poster2": return MovieID.Episode2;
            case "poster3": return MovieID.Episode3;
            case "poster4": return MovieID.Episode4;
            case "poster5": return MovieID.Episode5;
            case "poster6": return MovieID.Episode6;
            default: return MovieID.Episode1;
        }
    }
}