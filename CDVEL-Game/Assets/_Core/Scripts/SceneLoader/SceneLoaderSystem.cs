using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public class SceneLoaderSystem : ISceneLoaderSystem
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}