using UnityEngine;
using UnityEngine.UI;

public class TimeLimitSlider : MonoBehaviour
{
    [SerializeField] private float maxTime = 30f;

    private Slider slider;
    private float currentTime;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        currentTime = maxTime;
        slider.value = 1f;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            enabled = false;
        }

        slider.value = currentTime / maxTime;
    }

    public float GetTimeValue() 
    {
        return currentTime;
    }
}