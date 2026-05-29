using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientSceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject prefabOrder;
    [SerializeField] private TimeLimitSlider timer;
    [SerializeField] private Slider timerSlider;
    [Header("Ingredients")]
    [SerializeField] private GameObject food1;
    [SerializeField] private GameObject food2;
    [SerializeField] private GameObject food3;
    [SerializeField] private GameObject food4;
    [SerializeField] private GameObject food5;
    OrderInfo info;
    public void GoCook() 
    {
        int sizeList = 0;
        if (food1.transform.childCount != 0)
            sizeList++;

        if (food2.transform.childCount != 0)
            sizeList++;

        if (food3.transform.childCount != 0)
            sizeList++;

        if (food4.transform.childCount != 0)
            sizeList++;

        if (food5.transform.childCount != 0)
            sizeList++;

        string[] ingredients = new string[sizeList];

        if (food1.transform.childCount != 0)
            ingredients[0] = food1.transform.GetChild(0).name;

        if (food2.transform.childCount != 0)
            ingredients[1] = food2.transform.GetChild(0).name;

        if (food3.transform.childCount != 0)
            ingredients[2] = food3.transform.GetChild(0).name;

        if (food4.transform.childCount != 0)
            ingredients[3] = food4.transform.GetChild(0).name;

        if (food5.transform.childCount != 0)
            ingredients[4] = food5.transform.GetChild(0).name;

        GameObject orderInfo = Instantiate(prefabOrder);
        DontDestroyOnLoad(orderInfo);
        info = orderInfo.GetComponent<OrderInfo>();
        info.SetTime(timer.maxTime, timerSlider.value);
        info.SetDish("");
        info.SetClient(0);
        info.SetIngridients(ingredients);
        SceneManager.LoadScene(2);
    }
}
