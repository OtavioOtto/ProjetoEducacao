using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button cookBttn;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(()=>ShowInventory()); ;
    }

    private void ShowInventory() 
    {
        if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
            cookBttn.interactable = true;
        }

        else 
        {
            inventoryPanel.SetActive(true);
            cookBttn.interactable = false;
        }

    }

}
