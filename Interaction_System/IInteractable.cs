using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace meekobytes
{
    public interface IInteractable
    {
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        public string InteractionPrompt { get; }

        public void Interact(Interactor interactor, out bool interactSuccessful);

        public void EndInteraction();
    }
}
