using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLimitSlider : MonoBehaviour
{
    public float maxTime = 30f;
    public float currentTime;

    private Slider slider;
    

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
        }

        slider.value = currentTime / maxTime;
    }

    public float GetTimeValue() 
    {
        return currentTime;
    }
}