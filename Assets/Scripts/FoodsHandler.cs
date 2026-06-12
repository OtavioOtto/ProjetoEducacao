using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FoodsHandler : MonoBehaviour
{
    [SerializeField] private Button cookBttn;
    [SerializeField] private Transform[] positions = new Transform[5];
    [SerializeField] private Transform position;
    [SerializeField] private GameObject orderPos;
    [SerializeField] private Slider timer;
    [SerializeField] private OrdersUIHandler orderUI;
    [SerializeField] private MoneyHandler moneyValue;
    [Header("GameObjects")]
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private GameObject warningTxt;
    [SerializeField] private GameObject emptyTxt;
    [SerializeField] private GameObject explosionFx;
    [Header("Sounds")]
    [SerializeField] private AudioSource create;
    [SerializeField] private AudioSource delete;
    [Header("Crafting System")]
    [SerializeField] private CraftingManager craftingManager;
    [Header("Canvas")]
    [SerializeField] private Canvas clients;
    [SerializeField] private Canvas cooking;
    [Header("Reaction")]
    [SerializeField] private Image correct;
    [SerializeField] private Image incorrect;
    [SerializeField] private Image money;
    [SerializeField] private TMP_Text amount;
    // Simple list to track current ingredients
    private List<Ingredients> currentIngredients = new List<Ingredients>();
    private bool isChoosing;
    private bool isCooking;
    private string orderName;
    private void Start()
    {
        isChoosing = false;
        isCooking = false;
        cookBttn.onClick.AddListener(() => Cook());
        if (craftingManager == null)
            craftingManager = FindFirstObjectByType<CraftingManager>();
    }
    private void Update()
    {
        if (timer.value == 0f)
        {
            isCooking = false;
            currentIngredients.Clear();
            money.enabled = false;
            correct.enabled = false;
            incorrect.enabled = false;
            amount.enabled = false;
            clients.enabled = true;
            cooking.enabled = false;
            Destroy(orderPos.transform.GetChild(0).gameObject);
        }
    }
    public void ChooseFood(IngredientIdentifier ingredientData)
    {
        if (prefabs == null) return;
        if (isChoosing) return;
        if (isCooking) return;
        // Check if we already have 5 ingredients
        if (currentIngredients.Count >= 5)
        {
            StartCoroutine(ShowWarning());
            return;
        }
        isChoosing = true;
        // Get the prefab associated with this ingredient
        GameObject prefab = ingredientData.ingredientPrefab;
        create.Play();
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].childCount == 0)
            {
                GameObject instance = Instantiate(prefab, positions[i]);
                instance.GetComponent<RectTransform>().localScale *= 0.8f;
                // Add identifier to the instantiated object (for destruction tracking)
                IngredientIdentifier identifier = instance.AddComponent<IngredientIdentifier>();
                identifier.ingredientType = ingredientData.ingredientType;
                currentIngredients.Add(ingredientData.ingredientType);
                StartCoroutine(ResetChoosing());
                return;
            }
        }
        isChoosing = false;
    }
    public void Cook()
    {
        if (currentIngredients.Count == 0)
        {
            StartCoroutine(ShowEmpty());
            return;
        }
        StartCoroutine(MoveIngredients());
    }
    private IEnumerator MoveIngredients()
    {
        orderUI = orderPos.GetComponentInChildren<OrdersUIHandler>();
        bool correctCheck = false;
        isCooking = true;
        // Get all ingredient GameObjects to move
        List<Transform> ingredientsToMove = new List<Transform>();
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].childCount > 0)
            {
                ingredientsToMove.Add(positions[i].GetChild(0));
            }
        }
        Vector3[] startPositions = new Vector3[ingredientsToMove.Count];
        for (int i = 0; i < ingredientsToMove.Count; i++)
        {
            startPositions[i] = ingredientsToMove[i].position;
        }
        float duration = 0.5f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            for (int i = 0; i < ingredientsToMove.Count; i++)
            {
                if (ingredientsToMove[i] != null)
                {
                    ingredientsToMove[i].position = Vector3.Lerp(startPositions[i], position.position, t);
                }
            }
            yield return null;
        }
        for (int i = 0; i < ingredientsToMove.Count; i++)
        {
            if (ingredientsToMove[i] != null)
            {
                ingredientsToMove[i].position = position.position;
                ingredientsToMove[i].SetParent(position);
            }
        }
        if (explosionFx != null)
        {
            GameObject explosion = Instantiate(explosionFx, position);
            Destroy(explosion, 1f);
        }
        // CHECK RECIPE USING THE CRAFTING MANAGER
        GameObject craftedResult = null;
        if (craftingManager != null)
        {
            craftedResult = craftingManager.CraftItem(currentIngredients);
            orderName = craftingManager.GetRecipeName(currentIngredients);
        }
        else
        {
            Debug.LogError("CraftingManager not assigned!");
        }
        // Delete all ingredients
        foreach (Transform ingredient in ingredientsToMove)
        {
            if (ingredient != null)
                Destroy(ingredient.gameObject);
        }
        // Handle the result
        if (craftedResult != null)
        {
            correctCheck = true;
            GameObject finalDish = Instantiate(craftedResult, position);
            isCooking = false;
            if (orderName.Equals(orderUI.recipeName))
            {
                if (finalDish.GetComponent<TypeFood>().type == 0)
                {
                    moneyValue.SetMoney(20);
                    currentIngredients.Clear();
                    amount.SetText("+ 20 Pontos");
                    amount.color = Color.green;
                    correct.enabled = true;
                    money.enabled = true;
                    amount.enabled = true;
                    yield return new WaitForSeconds(3f);
                    money.enabled = false;
                    correct.enabled = false;
                    incorrect.enabled = false;
                    amount.enabled = false;
                    clients.enabled = true;
                    cooking.enabled = false;
                    Destroy(finalDish);
                    Destroy(orderPos.transform.GetChild(0).gameObject);
                }

                else if (finalDish.GetComponent<TypeFood>().type == 1)
                {
                    moneyValue.SetMoney(10);
                    currentIngredients.Clear();
                    amount.SetText("+ 10 Pontos");
                    amount.color = Color.green;
                    correct.enabled = true;
                    money.enabled = true;
                    amount.enabled = true;
                    yield return new WaitForSeconds(3f);
                    money.enabled = false;
                    correct.enabled = false;
                    incorrect.enabled = false;
                    amount.enabled = false;
                    clients.enabled = true;
                    cooking.enabled = false;
                    Destroy(finalDish);
                    Destroy(orderPos.transform.GetChild(0).gameObject);
                }

                else if (finalDish.GetComponent<TypeFood>().type == 2)
                {
                    moneyValue.SetMoney(5);
                    currentIngredients.Clear();
                    amount.SetText("+ 5 Pontos");
                    amount.color = Color.green;
                    correct.enabled = true;
                    money.enabled = true;
                    amount.enabled = true;
                    yield return new WaitForSeconds(3f);
                    money.enabled = false;
                    correct.enabled = false;
                    incorrect.enabled = false;
                    amount.enabled = false;
                    clients.enabled = true;
                    cooking.enabled = false;
                    Destroy(finalDish);
                    Destroy(orderPos.transform.GetChild(0).gameObject);
                }
            }

            else
            {
                currentIngredients.Clear();
                amount.SetText("+ 0 Pontos");
                amount.color = Color.red;
                incorrect.enabled = true;
                money.enabled = true;
                amount.enabled = true;
                yield return new WaitForSeconds(3f);
                money.enabled = false;
                correct.enabled = false;
                incorrect.enabled = false;
                amount.enabled = false;
                clients.enabled = true;
                cooking.enabled = false;
                Destroy(finalDish);
                Destroy(orderPos.transform.GetChild(0).gameObject);
            }
        }
        else if(craftedResult == null && !correctCheck)
        {
            moneyValue.SetMoney(-5);
            amount.SetText("-5 Pontos");
            amount.color = Color.red;
            incorrect.enabled = true;
            money.enabled = true;
            amount.enabled = true;
            yield return new WaitForSeconds(3f);
            isCooking = false;
            currentIngredients.Clear();
            money.enabled = false;
            correct.enabled = false;
            incorrect.enabled = false;
            amount.enabled = false;
            clients.enabled = true;
            cooking.enabled = false;
            Destroy(orderPos.transform.GetChild(0).gameObject);
        }
    }
    public void DestroyIngredient(GameObject target)
    {
        IngredientIdentifier identifier = target.GetComponent<IngredientIdentifier>();
        if (identifier != null && currentIngredients.Contains(identifier.ingredientType))
        {
            currentIngredients.Remove(identifier.ingredientType);
        }
        delete.Play();
        Destroy(target);
    }
    private IEnumerator ShowWarning()
    {
        warningTxt.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningTxt.SetActive(false);
    }
    private IEnumerator ShowEmpty()
    {
        emptyTxt.SetActive(true);
        yield return new WaitForSeconds(2f);
        emptyTxt.SetActive(false);
    }
    private IEnumerator ResetChoosing()
    {
        yield return null;
        isChoosing = false;
    }
}