namespace SceneLoader
{
    using Singletons;
    
    public class SceneLoaderHandler : MonoSingleton<SceneLoaderHandler>
    {
        private ISceneLoaderSystem _sceneLoaderSystem;
        
        protected override void Awake()
        {
            base.Awake();
            _sceneLoaderSystem = new SceneLoaderSystem();
        }

        public void LoadScene(string sceneName)
        {
            _sceneLoaderSystem.LoadScene(sceneName);
        }
    }
}