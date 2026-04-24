using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
