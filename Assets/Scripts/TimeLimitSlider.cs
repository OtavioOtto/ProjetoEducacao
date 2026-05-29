using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLimitSlider : MonoBehaviour
{
    public float maxTime = 30f;

    private Slider slider;
    private float currentTime;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name != "CookingScene")
        {
            currentTime = maxTime;
            slider.value = 1f;
        }

        else 
        {
            //OrderInfo info = GameObject.Find("OrderInfo(Clone)").GetComponent<OrderInfo>();
            //if(info != null) 
            //{
            //    slider.value = info.GetCurrentTime();
            //    slider.maxValue = info.GetTime();
            //}
        }
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