using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TwinMemoryPuzzle.Scripts.UI
{
    public class ScoreAnimatorUpdate : MonoBehaviour
    {
        public float animationTime = 2f;  // How long the animation takes

        // Call this function with startScore and endScore
        public void AnimateScore(int startScore, int endScore,Text scoreText)
        {
            StartCoroutine(AnimateScoreCoroutine(startScore, endScore, scoreText));
        }

        private IEnumerator AnimateScoreCoroutine(int startScore, int endScore,Text scoreText)
        {
            float elapsedTime = 0;
        
            // Loop for the animation time
            while (elapsedTime < animationTime)
            {
                // Calculate the current score
                float currentScore = Mathf.Lerp(startScore, endScore, elapsedTime / animationTime);
                scoreText.text = Mathf.RoundToInt(currentScore).ToString();

                elapsedTime += Time.deltaTime;
                yield return null;  // Wait until next frame
            }

            // Ensure the final score is exactly endScore
            scoreText.text = endScore.ToString();
        }
    }
}
