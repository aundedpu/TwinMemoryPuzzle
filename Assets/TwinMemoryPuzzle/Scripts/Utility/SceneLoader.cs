using System;
using System.Collections;
using TwinMemoryPuzzle.Scripts.Constant;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwinMemoryPuzzle.Scripts.Utility
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Animator fadeAnimator;
        //Fade time
        private static float fadeTime = 2f;
        public static SceneLoader instance;
        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            LoadSceneWithFade(GlobalConstant.INDEX_SAVELOAD_SCENE);
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
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

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
