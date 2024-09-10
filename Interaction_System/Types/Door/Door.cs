using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace meekobytes
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _prompt;

        public string InteractionPrompt => GetInteractionPrompt();
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        [SerializeField]
        private bool _isOpen = false;
        [SerializeField]
        private bool _isRotatingDoor = true;
        [SerializeField]
        private float _rotationSpeed = 1f;
        [Header("Rotation Configs")]
        [SerializeField]
        private float _rotationAmount = 90f;
        [SerializeField]
        private float _forwardDirection = 0;

        [Header("Sliding Configs")]
        [SerializeField]
        private Vector3 _slideDirection = Vector3.back;
        private float _slideAmount = 1.9f;
        private float _slideSpeed = 1f;

        private Vector3 _startRotation;
        private Vector3 _startPosition;
        private Vector3 _forward;

        private Coroutine AnimationCoroutine;

        public bool IsOpen { get { return _isOpen; } set { _isOpen = value; } }

        private void Awake()
        {
            _startRotation = transform.rotation.eulerAngles;
            _forward = transform.right;
        }

        public void Open(Vector3 playerPosition)
        {
            if (!_isOpen)
            {
                if (AnimationCoroutine != null)
                {
                    StopCoroutine(AnimationCoroutine);
                }

                if (_isRotatingDoor)
                {
                    float dot = Vector3.Dot(_forward, playerPosition - transform.position.normalized);
                    AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
                }
                else
                {
                    AnimationCoroutine = StartCoroutine(DoSlidingOpen());
                }
            }
        }

        public string GetInteractionPrompt()
        {
            if (_isOpen) return "Close door.";
            return "Open door.";
        }

        public void EndInteraction()
        {
            GetInteractionPrompt();
        }



        public void Interact(Interactor interactor, out bool interactSuccessful)
        {
            if (_isOpen)
            {
                Close();
                interactSuccessful = true;
            }
            else if (!_isOpen)
            {
                Open(interactor.transform.position);
                interactSuccessful = true;
            }
            else
            {
                interactSuccessful = false;
            }
        }

        private IEnumerator DoSlidingOpen()
        {
            Vector3 endPosition = _startPosition + _slideAmount * _slideDirection;
            Vector3 startPosition = transform.position;

            float time = 0;
            _isOpen = true;
            while (time < 1)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, time);
                yield return null;
                time += Time.deltaTime * _slideSpeed;
            }
        }

        private IEnumerator DoRotationOpen(float forwardAmount)
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation;

            if (forwardAmount >= _forwardDirection)
            {
                endRotation = Quaternion.Euler(new Vector3(0, startRotation.y - _rotationAmount, 0));
            }
            else
            {
                endRotation = Quaternion.Euler(new Vector3(0, startRotation.y + _rotationAmount, 0));
            }

            _isOpen = true;

            float time = 0;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;
                time += Time.deltaTime * _rotationSpeed;
            }
        }

        public void Close()
        {
            if (_isOpen)
            {
                if (AnimationCoroutine != null)
                {
                    StopCoroutine(AnimationCoroutine);
                }

                if (_isRotatingDoor)
                {
                    AnimationCoroutine = StartCoroutine(DoRotationClose());
                }
                else
                {
                    AnimationCoroutine = StartCoroutine(DoSlidingClose());
                }
            }
        }

        private IEnumerator DoRotationClose()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(_startRotation);

            _isOpen = false;

            float time = 0;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;
                time += Time.deltaTime * _rotationSpeed;
            }
        }

        private IEnumerator DoSlidingClose()
        {
            Vector3 endPosition = _startPosition;
            Vector3 startPosition = transform.position;
            float time = 0;

            _isOpen = false;

            while (time < 1)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, time);
                yield return null;
                time += Time.deltaTime * _slideSpeed;
            }
        }
    }
}
