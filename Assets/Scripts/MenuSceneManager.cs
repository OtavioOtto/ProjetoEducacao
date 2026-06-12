using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    private static MenuSceneManager instance;
    private static GameObject musicManager;

    private Button startBttn;
    private Button tutorialBttn;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (gameObject.CompareTag("MusicManager"))
        {
            if (musicManager == null)
            {
                musicManager = gameObject;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "MenuScene" && startBttn == null) 
        {
            startBttn = GameObject.Find("StartBttn").GetComponent<Button>();
            tutorialBttn = GameObject.Find("TutorialBttn").GetComponent<Button>();
            startBttn.onClick.AddListener(() => StartGame());
            tutorialBttn.onClick.AddListener(() => GoToTutorial());
        }
    }

    public void StartGame()
    {
        if (musicManager != null)
        {
            Destroy(musicManager);
            musicManager = null;
        }
        SceneManager.LoadScene(5);
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene(4);
    }
}