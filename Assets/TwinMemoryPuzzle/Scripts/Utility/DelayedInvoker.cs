using System;
using System.Collections;
using UnityEngine;

namespace TwinMemoryPuzzle.Utility
{
    public class DelayedInvoker : MonoBehaviour
    {
        private static DelayedInvoker instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public static void InvokeAfterDelay(float delay, Action action)
        {
            instance.StartCoroutine(instance.PerformDelayedAction(delay, action));
        }

        private IEnumerator PerformDelayedAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}