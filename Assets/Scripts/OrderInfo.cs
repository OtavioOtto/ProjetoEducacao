using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class OrderInfo : MonoBehaviour
{
    [SerializeField] private int client = -1;
    [SerializeField] private string[] ingredients;
    [SerializeField] private string dish = "";
    [SerializeField] private float timeLeft;
    [SerializeField] private float totalTime;

    public int GetClient() { return client; }
    public string[] GetIngridients() { return ingredients; }
    public string GetDish() { return dish; }
    public float GetTime() { return totalTime; }
    public float GetCurrentTime() { return timeLeft; }

    public void SetClient(int clientId) { client = clientId; }
    public void SetIngridients(string[] ingredientsList) { ingredients = ingredientsList; }
    public void SetDish(string food) { dish = food; }
    public void SetTime(float time, float currentTime) { totalTime = time; timeLeft = currentTime; }
}
