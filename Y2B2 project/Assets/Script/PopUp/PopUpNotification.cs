using UnityEngine;

public class PopUpNotification : MonoBehaviour
{
    [Header("Pop Up Settings")]
    [Tooltip("The minimum scale of the object")]
    [SerializeField] private float minScale = 0.8f;
    [Tooltip("The maximum scale of the object")]
    [SerializeField] private float maxScale = 1.2f;
    [Tooltip("The speed of the the effect")]
    [SerializeField] private float speed = 2f;

    void Update()
    {
        float number = Mathf.PingPong(Time.time * speed, 1.0f);
        float currentScale = Mathf.Lerp(minScale, maxScale, number);
        transform.localScale = new Vector2(currentScale, currentScale);
    }
}
