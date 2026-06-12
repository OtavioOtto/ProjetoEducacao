using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class GoToShop : MonoBehaviour
{
    [SerializeField] private Canvas final;
    [SerializeField] private MoneyHandler handler;
    [SerializeField] private GameObject clients;
    [SerializeField] private GameObject clientsParent;
    [SerializeField] private GameObject ordersParent;
    [SerializeField] private GameObject message;
    [Header("Transitions")]
    [SerializeField] private RawImage transition;
    [SerializeField] private RawImage transition2;
    private Slider slider;

    private int day;
    private bool playing;

    private void Start()
    {
        playing = false;
        day = handler.GetDay();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if(slider.value == 0f)
            day = handler.GetDay();
        if (slider.value == 0f && day == 3) 
        {
            clients.SetActive(false);
            final.enabled = true;
        }

        else if(slider.value == 0f && day != 3 && !playing) 
        {
            StartCoroutine(PlayTransition());
        }
    }

    private IEnumerator PlayTransition() 
    {
        playing = true;
        clients.SetActive(false);
        if(day == 1) 
        {
            transition.enabled = true;
            transition.gameObject.GetComponent<VideoPlayer>().Play();
        }

        else if(day == 2) 
        {
            transition2.enabled = true;
            transition2.gameObject.GetComponent<VideoPlayer>().Play();
        }
            
        yield return new WaitForSeconds(4.5f);
        transition.gameObject.GetComponent<VideoPlayer>().Stop();
        transition2.gameObject.GetComponent<VideoPlayer>().Stop();
        transition.enabled = false;
        transition2.enabled = false;
        slider.value = 1f;
        slider.gameObject.GetComponent<TimeLimitSlider>().currentTime = 155f;
        playing = false;
        clients.SetActive(true);

        if(clientsParent.transform.childCount > 0)
            Destroy(clientsParent.transform.GetChild(0).gameObject);

        if(ordersParent.transform.childCount > 0) 
        {
            foreach (Transform child in ordersParent.transform)
            {
                Destroy(child.gameObject.gameObject);
            }
        }

        if (message.activeSelf)
            message.SetActive(false);
        Time.timeScale = 1;
        handler.ChangeDay();
    }
}
