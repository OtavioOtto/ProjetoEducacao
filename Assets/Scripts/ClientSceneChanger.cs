using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientSceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject prefabOrder;
    [SerializeField] private Canvas cookingCanvas;
    [SerializeField] private Canvas clientsCanvas;
    [SerializeField] private Transform orderPos;
    [SerializeField] private Slider orderSlider;
    [SerializeField] private Slider cookingSlider;

    public void GoCook() 
    {
        orderPos = GameObject.FindGameObjectWithTag("OrderPos").GetComponent<Transform>();
        cookingSlider = GameObject.FindGameObjectWithTag("CookSlider").GetComponent<Slider>();
        cookingCanvas = GameObject.FindGameObjectWithTag("CookCanvas").GetComponent<Canvas>();
        clientsCanvas = GameObject.Find("GameplayCanvas").GetComponent<Canvas>();

        Time.timeScale = 1f;
        cookingCanvas.enabled = true;
        clientsCanvas.enabled = false;

        prefabOrder.transform.SetParent(orderPos, false);
        prefabOrder.transform.localScale = new Vector3(1f, 1f, 1f);
        prefabOrder.GetComponent<OrdersUIHandler>().enabled = false;
        prefabOrder.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        cookingSlider.value = orderSlider.value;
        cookingSlider.GetComponent<TimeLimitSlider>().maxTime = orderSlider.GetComponent<TimeLimitSlider>().maxTime;
        cookingSlider.GetComponent<TimeLimitSlider>().currentTime = orderSlider.GetComponent<TimeLimitSlider>().currentTime;

        orderSlider.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
