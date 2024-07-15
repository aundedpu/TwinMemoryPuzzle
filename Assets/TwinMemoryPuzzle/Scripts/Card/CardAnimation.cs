using System;
using UnityEngine;

namespace TwinMemoryPuzzle.Scripts.Card
{
    [RequireComponent(typeof(Animator))]
    public class CardAnimation : MonoBehaviour
    {
        [SerializeField] private Animator cardAnimator;

        private void Start()
        {
            cardAnimator = GetComponent<Animator>();
        }
        public void Show()
        {
            cardAnimator.Play("Open");   
        }

        public void Hide()
        {
            cardAnimator.Play("Close");  
        }

        public Animator CardAnimator
        {
            get => cardAnimator;
            set => cardAnimator = value;
        }

    }
}
