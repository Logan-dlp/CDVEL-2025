using System;

namespace SceneLoader
{
    using Singletons;
    
    public class SceneLoaderHandler : PersistentMonoSingleton<SceneLoaderHandler>
    {
        public Action<string> OnLoadScene; 
        
        private ISceneLoaderSystem _sceneLoaderSystem;
        
        protected override void Awake()
        {
            base.Awake();
            _sceneLoaderSystem = new SceneLoaderSystem();

            OnLoadScene += LoadScene;
        }

        private void LoadScene(string sceneName)
        {
            _sceneLoaderSystem.LoadScene(sceneName);
        }
    }
}