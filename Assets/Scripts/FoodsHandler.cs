using UnityEngine;
using UnityEngine.EventSystems;

public class FoodsHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform position;
    [SerializeField] private GameObject prefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        ChooseFood();
    }

    public void ChooseFood()
    {
        if (prefab == null || position == null) return;

        GameObject instance = Instantiate(prefab, position);
        instance.GetComponent<RectTransform>().sizeDelta *= 2;
    }
}