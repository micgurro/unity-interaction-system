using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace meekobytes
{
    public class Switch : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool _isOn = false;

        [SerializeField]
        private GameObject _toggle;

        private Coroutine AnimationCoroutine;
        private float _dimSpeed;

        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        public string InteractionPrompt => GetInteractionPrompt();

        private string GetInteractionPrompt()
        {
            if (_isOn) return "Turn off";
            return "Turn on";
        }

        public void EndInteraction()
        {
            GetInteractionPrompt();
        }

        public void Interact(Interactor interactor, out bool interactSuccessful)
        {
            if (_isOn)
            {
                TurnOff(_toggle);
                interactSuccessful = true;
            }
            else if (!_isOn)
            {
                TurnOn(_toggle);
                interactSuccessful = true;
            }
            else
            {
                interactSuccessful = false;
            }

        }

        private void TurnOn(GameObject toggle)
        {
            if (!_isOn)
            {
                if (AnimationCoroutine != null)
                {
                    StopCoroutine(AnimationCoroutine);
                }
                else
                {
                    AnimationCoroutine = StartCoroutine(DoTurnOn());
                }
                _isOn = true;
                toggle.SetActive(true);
            }
        }

        private void TurnOff(GameObject toggle)
        {
            if (_isOn)
            {
                if (AnimationCoroutine != null)
                {
                    StopCoroutine(AnimationCoroutine);
                }
                else
                {
                    AnimationCoroutine = StartCoroutine(DoTurnOff());
                }
                _isOn = false;
                toggle.SetActive(false);
            }
            
        }

        private IEnumerator DoTurnOn()
        {
            float time = 0;
            while (time < 1)
            {
                yield return null;
                time += Time.deltaTime * _dimSpeed;
            }
        }

        private IEnumerator DoTurnOff()
        {
            float time = 0;
            while (time < 1)
            {
                yield return null;
                time += Time.deltaTime * _dimSpeed;
            }
        }
    }
}
