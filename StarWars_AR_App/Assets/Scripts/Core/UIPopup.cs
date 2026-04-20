using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public float speed = 8f;

    void OnEnable()
    {
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            Vector3.one,
            Time.deltaTime * speed
        );
    }
}