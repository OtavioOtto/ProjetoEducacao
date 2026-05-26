using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FoodsHandler : MonoBehaviour
{
    [SerializeField] private Button cookBttn;
    [SerializeField] private Transform [] positions = new Transform[5];
    [SerializeField] private Transform position;
    [Header("GameObjects")]
    [SerializeField] private GameObject [] prefabs;
    [SerializeField] private GameObject warningTxt;
    [SerializeField] private GameObject emptyTxt;
    [SerializeField] private GameObject explosionFx;

    private bool isChoosing;
    private bool isCooking;

    private void Start()
    {
        isChoosing = false;
        isCooking = false;
         cookBttn.onClick.AddListener(() => Cook());
    }

    public void ChooseFood(int food)
    {
        if (prefabs == null) return;

        if (isChoosing) return;
        if (isCooking) return;
        isChoosing = true;

        GameObject instance = null;

        GameObject prefab = ChoosePrefab(food);

        for (int i = 0; i < positions.Length; i++)
        { 
            if (positions[i].childCount == 0)
            {
                instance = Instantiate(prefab, positions[i]);
                instance.GetComponent<RectTransform>().sizeDelta *= 1.2f;
                StartCoroutine(ResetChoosing());
                return;
            }
        }
        if(instance == null)
        {
            StartCoroutine(ShowWarning());
        }
        isChoosing = false;
    }

    private void Cook() 
    {
        int ingredientQuant = 0;

        for(int i = 0; i < prefabs.Length; i++) 
        {
            if (positions[i].childCount != 0)
                ingredientQuant++;
        }

        if (ingredientQuant == 0)
        {
            StartCoroutine(ShowEmpty());
            return;
        }

        StartCoroutine(MoveIngredients());


    }

    private GameObject ChoosePrefab(int food) 
    {
        return prefabs[food];
    }

    private IEnumerator MoveIngredients()
    {
        isCooking = true;
        Transform[] ingredients = new Transform[5];
        int index = 0;

        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].childCount > 0)
            {
                ingredients[index] = positions[i].GetChild(0);
                index++;
            }
        }

        Vector3[] startPositions = new Vector3[5];

        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                startPositions[i] = ingredients[i].position;
            }
        }

        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i] != null)
                {
                    ingredients[i].position = Vector3.Lerp(startPositions[i], position.position, t);
                }
            }

            yield return null;
        }

        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                ingredients[i].position = position.position;
                ingredients[i].SetParent(position);
            }
        }

        if (explosionFx != null)
        {
            GameObject explosion = Instantiate(explosionFx, position);
            Destroy(explosion, 1f);
        }

        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                Destroy(ingredients[i].gameObject);
            }
        }

        for (int i = 0; i < positions.Length; i++)
        {
            foreach (Transform child in positions[i])
            {
                Destroy(child.gameObject);
            }
        }
        isCooking = false;
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