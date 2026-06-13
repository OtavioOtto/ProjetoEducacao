using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrdersUIHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler, IBeginDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform spawn;
    [SerializeField] private float targetYPosition = 0f;
    [SerializeField] private float dragThreshold = 10f;
    [SerializeField] private GameObject cookBttn;
    [SerializeField] private Image shadow;
    public string recipeName;

    private RectTransform rectTransform;
    private bool draggable = true;
    private bool isDragging = false;
    private Vector2 dragStartPosition;
    private CanvasGroup canvasGroup;
    private bool isPotentialDrag = true;
    private float remainingTime;
    private Pause pause;

    TimeLimitSlider timer;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        if (timer == null)
            timer = transform.GetComponentInChildren<TimeLimitSlider>();
    }

    private void Start()
    {
        canvas = GameObject.Find("GameplayCanvas").GetComponent<Canvas>();
        spawn = GameObject.Find("OrdersSpawn").GetComponent<Transform>();
        
    }

    private void FixedUpdate()
    {
        remainingTime = timer.GetTimeValue();
        if (remainingTime == 0)
            Destroy(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPosition = eventData.position;
        isDragging = false;
        isPotentialDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!draggable) return;

        // Check if we've moved past the drag threshold
        if (isPotentialDrag && Vector2.Distance(eventData.position, dragStartPosition) > dragThreshold)
        {
            isDragging = true;
            isPotentialDrag = false;
            canvasGroup.alpha = 0.8f;
        }

        if (!isDragging) return;

        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        RectTransform canvasRect = (RectTransform)canvas.transform;
        Vector2 targetPos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, canvas.worldCamera, out targetPos))
        {
            Vector3 worldTarget = canvas.transform.TransformPoint(targetPos);

            // Get boundaries
            Vector3[] objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);

            Vector3[] canvasCorners = new Vector3[4];
            canvasRect.GetWorldCorners(canvasCorners);

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
        canvasGroup.alpha = 1f;

        if (isDragging)
        {
            Vector3 localPos = rectTransform.localPosition;
            localPos.y = targetYPosition;
            rectTransform.localPosition = localPos;
        }

        isDragging = false;
        isPotentialDrag = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(shadow == null)
            shadow = GameObject.Find("Shadow").GetComponent<Image>();

        // Don't process click if it was a drag
        if (isDragging)
        {
            return;
        }

        if (pause == null)
            pause = GameObject.Find("PauseBttn").GetComponent<Pause>();

        if (gameObject.transform.parent.name.Equals("OrdersSpawn"))
        {
            // Move to center for cooking
            Time.timeScale = 0f;
            shadow.enabled = true;
            gameObject.transform.SetParent(canvas.gameObject.transform);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            Vector3 localPos = rectTransform.localPosition;
            localPos.y = targetYPosition;
            localPos.x = 0;
            rectTransform.localPosition = localPos;
            rectTransform.localScale *= 2.5f;
            cookBttn.SetActive(true);
            draggable = false;
            pause.ChangeButton();
        }
        else
        {
            // Return to spawn area
            Time.timeScale = 1f;
            shadow.enabled = false;
            gameObject.transform.SetParent(spawn);
            cookBttn.SetActive(false);
            rectTransform.pivot = new Vector2(0.5f, 1f);
            Vector3 localPos = rectTransform.localPosition;
            localPos.y = targetYPosition;
            localPos.x = 0;
            rectTransform.localPosition = localPos;
            rectTransform.localScale /= 2.5f;
            draggable = true;
            pause.ChangeButton();
        }
    }
}