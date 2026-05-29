using System.Collections;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ClientSpawnHandler : MonoBehaviour
{
    [SerializeField] private int currentDay = 1;
    [Header("Objects")]
    [SerializeField] private GameObject[] clients = new GameObject[6];
    [SerializeField] private GameObject order;
    [SerializeField] private GameObject textBG;
    [SerializeField] private TMP_Text dialogue;
    [Header("Transforms")]
    [SerializeField] private Transform orderPos;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform finalPosition;
    [Header("Scrtips")]
    [SerializeField] private OrdersHandler handler;
    [Header("Mapping")]
    [SerializeField] private IngredientMapping ingredientMapping;
    [Header("SFX")]
    [SerializeField] private AudioSource soundEffect;

    private bool isSpawning = false;

    void Start() 
    {
        StartSpawningForCurrentDay();
    }

    void StartSpawningForCurrentDay()
    {
        if (isSpawning) return; 

        if (currentDay == 1)
        {
            InvokeRepeating(nameof(SpawnClientDay1), 0f, 10f); 
        }
        else if (currentDay == 2)
        {
            InvokeRepeating(nameof(SpawnClientDay2), 5f, 20f);
        }
        else if (currentDay == 3)
        {
            InvokeRepeating(nameof(SpawnClientDay3), 3f, 15f);
        }
        else
        {
            Debug.LogError("Dia atual fora do limite");
            return;
        }

        isSpawning = true;
    }

    private void SpawnClientDay1() { SpawnClient(1); }
    private void SpawnClientDay2() { SpawnClient(2); }
    private void SpawnClientDay3() { SpawnClient(3); }

    private void SpawnClient(int chanceOfSpawn)
    {
        int spawn = 0;
        if (chanceOfSpawn == 1)
        {
            spawn = Random.Range(1, 11);
        }
        else if (chanceOfSpawn == 2)
        {
            spawn = Random.Range(1, 10);
        }
        else if (chanceOfSpawn == 3)
        {
            spawn = Random.Range(1, 9);
        }
        else
        {
            Debug.LogError("Chance fora do limite");
            return;
        }

        if (spawn <= 5)
        {
            int which = Random.Range(0, clients.Length);
            StartCoroutine(SpawnNPCAndOrder(which));
            
        }
    }

    private IEnumerator SpawnNPCAndOrder(int which) 
    {
        soundEffect.Play();
        GameObject client = Instantiate(clients[which], spawnPosition.position, Quaternion.identity, spawnPosition);

        yield return null;

        float elapsedTime = 0f;
        float moveDuration = 1.7f;

        Vector3 startPos = client.transform.position;
        Vector3 endPos = finalPosition.position;
        

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            client.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        textBG.SetActive(true);
        dialogue.text = ChooseText();

        client.transform.position = endPos;

        GameObject newOrder = Instantiate(order, orderPos.position, Quaternion.identity, orderPos);
        string[] ingredients = handler.ReturnIngredients();
        Transform imagesParent = newOrder.transform.GetChild(1);
        for(int i = 0; i < ingredients.Length; i++) 
        {
            GameObject ingredientPrefab = ingredientMapping.GetPrefab(ingredients[i]);
            if (ingredientPrefab != null)
            {
                for(int j = 0; j < imagesParent.childCount; j++) 
                {
                    if (imagesParent.GetChild(j).childCount == 0)
                    {
                        GameObject instance = Instantiate(ingredientPrefab, imagesParent.GetChild(j));
                        instance.GetComponent<RectTransform>().sizeDelta /= 1.2f;
                        break;
                    }
                    
                }
                
            }
        }
        

        isSpawning = false;
        StartCoroutine(DestroyNPC(client));
    }

    private IEnumerator DestroyNPC(GameObject target) 
    {
        yield return new WaitForSeconds(7.0f);

        float elapsedTime = 0f;
        float moveDuration = 1.4f;

        textBG.SetActive(false);

        Vector3 startPos = target.transform.position;
        Vector3 endPos = spawnPosition.position;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            target.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        Destroy(target);
    }

    private string ChooseText() 
    {
        string text = "";
        int random = Random.Range(1, 4);

        switch (random)
        {
            case 1:
                text = "Ola! Tudo bem? Eu gostaria de fazer meu pedido!";
                break;
            case 2:
                text = "Oii! Como vai? Esse vai ser meu pedido!";
                break;
            case 3:
                text = "Oie! Como voce esta? Vou querer isso!";
                break;
        }
        return text;
    }
}