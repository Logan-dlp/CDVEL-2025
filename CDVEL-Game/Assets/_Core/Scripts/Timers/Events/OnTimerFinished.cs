using UnityEngine;

namespace Timers
{
    using SceneLoader;
    using Players;
    
    public class OnTimerFinished : MonoBehaviour
    {
        [SerializeField] string sceneName;
        [SerializeField] private SaveTotalScore saveTotalScore;

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

            saveTotalScore.SaveScoreTotal();
            SaveScore();
            SceneLoaderHandler.Instance.LoadScene(sceneName);
        }
    }
}