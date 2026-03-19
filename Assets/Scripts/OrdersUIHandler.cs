using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrdersUIHandler : MonoBehaviour, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform spawn;
    [SerializeField] private float targetYPosition = 0f;
    private RectTransform rectTransform;
    private bool draggable = true;
    private bool isDragging = false;

    public void DragHandler(BaseEventData data)
    {
        if (draggable)
        {
            isDragging = true;
            PointerEventData pointerData = (PointerEventData)data;

            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            Vector3[] objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);

            RectTransform canvasRect = (RectTransform)canvas.transform;
            Vector3[] canvasCorners = new Vector3[4];
            canvasRect.GetWorldCorners(canvasCorners);

            Vector2 targetPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, pointerData.position, canvas.worldCamera, out targetPos);

            Vector3 worldTarget = canvas.transform.TransformPoint(targetPos);

            float width = objectCorners[2].x - objectCorners[0].x;
            float height = objectCorners[2].y - objectCorners[0].y;

            float minWorldX = canvasCorners[0].x + (width * rectTransform.pivot.x);
            float maxWorldX = canvasCorners[2].x - (width * (1 - rectTransform.pivot.x));
            float minWorldY = canvasCorners[0].y + (height * rectTransform.pivot.y);
            float maxWorldY = canvasCorners[2].y - (height * (1 - rectTransform.pivot.y));

            worldTarget.x = Mathf.Clamp(worldTarget.x, minWorldX, maxWorldX);
            worldTarget.y = Mathf.Clamp(worldTarget.y, minWorldY, maxWorldY);

            transform.position = worldTarget;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        Vector3 localPos = rectTransform.localPosition;
        localPos.y = targetYPosition;
        rectTransform.localPosition = localPos;

        Invoke(nameof(ResetDragFlag), 0.1f);
    }

    private void ResetDragFlag()
    {
        isDragging = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDragging)
            return;

        if (gameObject.transform.parent.name.Equals("OrdersSpawn"))
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            gameObject.transform.SetParent(canvas.gameObject.transform);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            Vector3 localPos = rectTransform.localPosition;
            localPos.y = targetYPosition;
            localPos.x = 0;
            rectTransform.localPosition = localPos;
            rectTransform.localScale *= 2.5f;
            draggable = false;
        }
        else
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            gameObject.transform.SetParent(spawn);
            rectTransform.pivot = new Vector2(0.5f, 1f);
            Vector3 localPos = rectTransform.localPosition;
            localPos.y = targetYPosition;
            localPos.x = 0;
            rectTransform.localPosition = localPos;
            rectTransform.localScale /= 2.5f;
            draggable = true;
        }
    }
}