using UnityEngine.SceneManagement;
using UnityEngine;

namespace SceneLoader
{
    public class SceneLoaderSystem : ISceneLoaderSystem
    {
        public void LoadScene(string sceneName)
        {
            Time.timeScale = 1;
            Debug.Log(Time.timeScale);
            SceneManager.LoadScene(sceneName);
        }
    }
}