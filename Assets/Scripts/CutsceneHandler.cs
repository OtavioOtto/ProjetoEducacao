using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneHandler : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(EndCutscene());
    }

    public void Skip() 
    {
        StopCoroutine(EndCutscene());
        SceneManager.LoadScene(1);
    }

    private IEnumerator EndCutscene() 
    {
        yield return new WaitForSeconds(17.9f);
        SceneManager.LoadScene(1);
    }
}
