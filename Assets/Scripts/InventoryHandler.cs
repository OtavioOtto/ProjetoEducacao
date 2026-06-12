using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    [SerializeField] private GameObject group1;
    [SerializeField] private GameObject group2;
    [SerializeField] private GameObject group3;

    private void Update()
    {
        if (group1.activeSelf)
            left.interactable = false;

        if (group3.activeSelf)
            right.interactable = false;

        if (group2.activeSelf && (!left.interactable || !right.interactable)) 
        {
            left.interactable = true;
            right.interactable = true;
        }
    }

    public void SwitchGroup(int side) 
    {
        if (group1.activeSelf) 
        {
            group1.SetActive(false);
            group2.SetActive(true);
        }

        else if (group3.activeSelf)
        {
            group3.SetActive(false);
            group2.SetActive(true);
        }

        else if(group2.activeSelf && side == 0) 
        {
            group2.SetActive(false);
            group1.SetActive(true);
        }

        else if (group2.activeSelf && side == 1)
        {
            group2.SetActive(false);
            group3.SetActive(true);
        }
    }
}
