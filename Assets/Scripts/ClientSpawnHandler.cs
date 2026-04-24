using System.Collections;
using TMPro;
using UnityEngine;

public class ClientSpawnHandler : MonoBehaviour
{
    [SerializeField] private int currentDay = 1;
    [Header("Objects")]
    [SerializeField] private GameObject[] clients = new GameObject[6];
    [SerializeField] private GameObject order;
    [Header("Transforms")]
    [SerializeField] private Transform orderPos;
    [SerializeField] private Transform spawnPosition;
    [Header("Scrtips")]
    [SerializeField] private OrdersHandler handler;

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
            InvokeRepeating(nameof(SpawnClientDay1), 20f, 30f); 
        }
        else if (currentDay == 2)
        {
            InvokeRepeating(nameof(SpawnClientDay2), 10f, 30f);
        }
        else if (currentDay == 3)
        {
            InvokeRepeating(nameof(SpawnClientDay3), 5f, 30f);
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
            GameObject client = Instantiate(clients[which], spawnPosition.position, Quaternion.identity, spawnPosition);
            GameObject newOrder = Instantiate(order, orderPos.position, Quaternion.identity, orderPos);
            string[] ingredients = handler.ReturnIngredients();
            newOrder.transform.GetChild(1).GetComponent<TMP_Text>().text = string.Join(", ", ingredients);
            isSpawning = false;
            StartCoroutine(DestroyNPC(client));
        }
    }

    private IEnumerator DestroyNPC(GameObject target) 
    {
        yield return new WaitForSeconds(7.0f);
        Destroy(target);
    }
}