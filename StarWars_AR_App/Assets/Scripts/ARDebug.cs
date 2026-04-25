using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARDebug : MonoBehaviour
{
    void Start()
    {
        Debug.Log("AR STATE: " + ARSession.state);
    }

    void Update()
    {
        Debug.Log("AR STATE (Update): " + ARSession.state);
    }
}