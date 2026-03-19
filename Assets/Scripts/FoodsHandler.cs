using UnityEngine;

public class FoodsHandler : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] Transform position;
    [SerializeField] GameObject prefab;
    public void ChooseFood()
    {
        
        GameObject instance = Instantiate(prefab,position);
        instance.transform.SetParent(position);
        if (rectTransform == null)
            rectTransform = instance.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0,0,0);
    }
}
