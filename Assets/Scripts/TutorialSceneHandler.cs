using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialSceneHandler : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Button returnBttn;
    [SerializeField] private Button librasBttn;
    [SerializeField] private Button textBttn;
    [SerializeField] private GameObject options;
    [Header("Libras")]
    [SerializeField] private Button librasCloseBttn;
    [SerializeField] private GameObject video;
    [SerializeField] private VideoPlayer player;
    [Header("Text")]
    [SerializeField] private Button textCloseBttn;
    [SerializeField] private GameObject text;
    void Start()
    {
        returnBttn.onClick.AddListener(() => ReturnToMenu());
        librasCloseBttn.onClick.AddListener(() => CloseLibras());
        textCloseBttn.onClick.AddListener(() => CloseText());
        textBttn.onClick.AddListener(() => ShowText());
        librasBttn.onClick.AddListener(() => ShowLibras());
    }
    
    private void ReturnToMenu() 
    {
        SceneManager.LoadScene(0);
    }
    private void ShowLibras() 
    {
        options.SetActive(false);
        video.SetActive(true);
        player.Play();
    }
    private void ShowText() 
    {
        options.SetActive(false);
        text.SetActive(true);
    }
    private void CloseText()
    {
        options.SetActive(true);
        text.SetActive(false);
    }
    private void CloseLibras() 
    {
        options.SetActive(true);
        video.SetActive(false);
        player.Stop();
        player.time = 0f;
    }
}
