using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace meekobytes
{
    public class ChairInteraction : MonoBehaviour, IInteractable
    {
        private string _prompt;
        private bool _isActive = false;

        [SerializeField] private GameObject _computerCamera;
        private MeshCollider _meshCollider;

        [SerializeField] private Transform _sitPosition;

        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        public string InteractionPrompt => _prompt;

        public void Awake()
        {
            _meshCollider = this.GetComponent<MeshCollider>();
            _meshCollider.isTrigger = false;

        }

        public void EndInteraction()
        {
            _meshCollider.isTrigger = false;
            _computerCamera.SetActive(false);
            this.transform.Translate(0, 0, .5f);
            _isActive = false;
        }

        public void Interact(Interactor interactor, out bool interactSuccessful)
        {
            if (!_isActive)
            {
                interactor.TryGetComponent<Animator>(out Animator player);
                this.transform.Translate(0, 0, -.5f);
                player.transform.position = _sitPosition.position;
                _meshCollider.isTrigger = true;
                _computerCamera.SetActive(true);
                _isActive = true;
                interactSuccessful = true;
            }
            else
            {
                EndInteraction();
                interactSuccessful = true ;
            }
        }
    }
}
