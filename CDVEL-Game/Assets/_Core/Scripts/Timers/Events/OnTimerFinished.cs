using UnityEngine;

namespace Timers
{
    using Players;
    using SceneLoader;
    
    public class OnTimerFinished : MonoBehaviour
    {
        [SerializeField] string sceneName;
        
        private void Awake()
        {
            TimerHandler.Instance.OnTimerFinished += OnTimerFinish;
        }

        private void OnTimerFinish()
        {
            void SaveScore()
            {
                var allScoreArray = FindObjectsByType<PlayerScoreHandler>(FindObjectsSortMode.None);

                foreach (PlayerScoreHandler playerScoreHandler in allScoreArray)
                {
                    PlayerPrefs.SetInt(playerScoreHandler.PlayerTag.ToString(), playerScoreHandler.GetScore());
                }
                
            }
            
            SaveScore();
            SceneLoaderHandler.Instance.OnLoadScene?.Invoke(sceneName);
        }
    }
}