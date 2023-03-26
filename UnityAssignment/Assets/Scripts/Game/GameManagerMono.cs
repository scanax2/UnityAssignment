using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMono : MonoBehaviour
{
    public void ResetScene()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
