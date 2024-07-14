using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwinMemoryPuzzle.Scripts.Utility
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Animator fadeAnimator;
        //Fade time
        private static float fadeTime = 2f;

        private void Start()
        {
            DontDestroyOnLoad(this);
            LoadSceneWithFade(1);
        }
        public void LoadSceneWithFade(int sceneIndex)
        {
            StartCoroutine(FadeAndLoad(sceneIndex));
        }
        private IEnumerator FadeAndLoad(int sceneIndex)
        {
            // Start Fade out
            fadeAnimator.Play("FadeIn");
        
            // Wait for fade to complete
            yield return new WaitForSeconds(fadeTime);

            // Start loading the scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            // Wait until the async scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            // Fade in after the scene is loaded
            fadeAnimator.Play("FadeOut"); 
        }
    }
}
