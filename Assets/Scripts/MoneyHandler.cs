using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private int globalMoney;
    [SerializeField] private int currentDay;
    void Start()
    {
        globalMoney = 0;
        currentDay = 1;
    }

    public int GetMoney() 
    {
        return globalMoney;
    }

    public void SetMoney(int change)
    {
        globalMoney += change;
    }

    public int GetDay()
    {
        return currentDay;
    }

    public void ChangeDay()
    {
        globalMoney += 1;
    }
}
