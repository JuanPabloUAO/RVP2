using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ImageTrackingHandler : MonoBehaviour
{
    public GameObject cardPrefab;

    private ARTrackedImageManager manager;
    private Dictionary<string, GameObject> spawned = new Dictionary<string, GameObject>();

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
            GameObject obj = Instantiate(cardPrefab, image.transform);
            spawned[image.referenceImage.name] = obj;
        }

        foreach (var image in args.updated)
        {
            if (spawned.ContainsKey(image.referenceImage.name))
            {
                spawned[image.referenceImage.name].SetActive(image.trackingState == TrackingState.Tracking);
            }
        }
    }
}