using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace meekobytes
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private float _interactionPointRadius = 0.5f;
        [SerializeField] private LayerMask _interactableMask;
        [SerializeField] private InteractionPromptUI _interactionPromptUI;

        [SerializeField] public bool IsInteracting { get; private set; }

        private readonly Collider[] _colliders = new Collider[3];
        [SerializeField] private int _numFound;

        private IInteractable _interactable;

        private void Update()
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

            if (_numFound > 0)
            {
                _interactable = _colliders[0].GetComponent<IInteractable>();

                if (_interactable != null)
                {
                    if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.Setup(_interactable.InteractionPrompt);

                    if (Keyboard.current.eKey.wasPressedThisFrame) StartInteraction(_interactable);
                }
            }
            else
            {
                if(_interactable != null) _interactable = null;
                if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
            }
        }

        void StartInteraction(IInteractable _interactable)
        {
            _interactable.Interact(this, out bool interactSuccessful);
            IsInteracting = true;
        }

        void EndInteraction()
        {
            IsInteracting = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
        }
    }

}