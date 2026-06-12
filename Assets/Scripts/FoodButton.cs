using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private FoodsHandler handler;
    [SerializeField] private IngredientIdentifier ingredientData;

    public void OnPointerClick(PointerEventData eventData)
    {
        string parent = transform.parent.name;
        if (!(parent == "Pos1" || parent == "Pos2" || parent == "Pos3" || parent == "Pos4" || parent == "Pos5"))
        {
            handler.ChooseFood(ingredientData);
        }

        if(parent == "Pos1" || parent == "Pos2" || parent == "Pos3" || parent == "Pos4" || parent == "Pos5")
        {
            if (handler == null)
                handler = GameObject.Find("CookingHandler").GetComponent<FoodsHandler>();
            handler.DestroyIngredient(gameObject);
        }
    }
}
