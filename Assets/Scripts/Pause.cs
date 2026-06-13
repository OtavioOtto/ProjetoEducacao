using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Button self;
    void Start()
    {
        self = gameObject.GetComponent<Button>();
        self.onClick.AddListener(() => PauseGame());
    }

    private void PauseGame()
    {
        if (Time.timeScale == 0f)
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
        }

        else
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ChangeButton() 
    {
        self.interactable = !self.interactable;
    }
}
