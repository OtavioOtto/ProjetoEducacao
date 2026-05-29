using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Image shadow;
    [SerializeField] private GameObject panel;
    private Button self;
    void Start()
    {
        self = gameObject.GetComponent<Button>();
        self.onClick.AddListener(() => PauseGame());
    }

    private void Update()
    {
        if (shadow.enabled)
            self.interactable = false;

        if (!shadow.enabled && !self.interactable)
            self.interactable = true;
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
}
