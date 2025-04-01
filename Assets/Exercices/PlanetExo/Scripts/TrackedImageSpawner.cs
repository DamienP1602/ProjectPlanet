using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageSpawner : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager trackedManager;
    [SerializeField] TMP_Text text;

    void Start()
    {
        trackedManager.trackedImagesChanged += SpawnImage;
    }

    void SpawnImage(ARTrackedImagesChangedEventArgs _args)
    {
        //Debug.Log(_args.ToString());
        foreach(ARTrackedImage _image in _args.added)
        {
            print(_image.name);
            Debug.Log(_image.name);
            text.text = _image.referenceImage.name;
        }
    }
}
