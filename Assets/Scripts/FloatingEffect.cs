using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float speed = 2f;
    public float height = 0.5f;
    private float timeOffset;

    private Vector2 startPosition;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        timeOffset = Random.Range(0f, Mathf.PI * 2);
    }

    private void Update()
    {
        timeOffset += Time.deltaTime * speed;
        float newY = startPosition.y + Mathf.Sin(timeOffset) * height;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}