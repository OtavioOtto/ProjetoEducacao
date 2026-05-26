using UnityEngine;
using UnityEngine.EventSystems;

public class FoodButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private FoodsHandler handler;
    [SerializeField] private int foodNumber;

    public void OnPointerClick(PointerEventData eventData)
    {
        handler.ChooseFood(foodNumber);
    }
}
