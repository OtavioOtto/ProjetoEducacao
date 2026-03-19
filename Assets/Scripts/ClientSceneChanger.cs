using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientSceneChanger : MonoBehaviour
{
    public void GoCook() 
    {
        SceneManager.LoadScene(2);
    }
}
