using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ImageTrackingHandler : MonoBehaviour
{
    private ARTrackedImageManager manager;

    void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        manager.trackedImagesChanged += OnChanged;
    }

    void OnDisable()
    {
        manager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var image in args.added)
        {
            string name = image.referenceImage.name;

            MovieID movie = GetMovieFromImage(name);

            PlayerPrefs.SetInt("SelectedMovie", (int)movie);

            SceneManager.LoadScene("QuizScene");
        }
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