using UnityEngine.SceneManagement;
using UnityEngine;

public class gamemanager : MonoBehaviour
{   public float delay = 2f;
    int i = 0;
    
    
    public void EndGame()
    {
        Invoke( "restart", 2);
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        Resources.UnloadUnusedAssets();
    }
}
