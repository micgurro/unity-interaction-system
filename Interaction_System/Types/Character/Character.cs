using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace meekobytes
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(UniqueID))]
    public class Character : MonoBehaviour, IInteractable, IDialogTrigger
    {
        [SerializeField] private string _prompt;

        public string InteractionPrompt => _prompt;
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }
        public Message[] messages { get; set; }
        public Actor[] actors { get; set; }

        [SerializeField] private NPCActorData npcActorData;

        private string id;

        public void Awake()
        {
            id = GetComponent<UniqueID>().ID;
            _prompt = $"Talk to '{npcActorData.ActorName}'.";
            messages = npcActorData.messages;
            actors = npcActorData.actors;

        }

        public void Interact(Interactor interactor, out bool interactSuccessful)
        {
            if(npcActorData.messages != null)
            {
                DialogueManager.Instance.OpenDialogue(messages, actors);
                interactSuccessful = true;
            }
            else
            {
                interactSuccessful = false;
            }
        }

        public void EndInteraction()
        {
            // Lock mouse
            // Free up character
        }
    }
    [System.Serializable]
    public struct NPCActorSaveData
    {
        public NPCActorData NPCActorData;
        public Vector3 Position;
        public Quaternion Rotation;

        public NPCActorSaveData(NPCActorData _data, Vector3 _position, Quaternion _rotation)
        {
            NPCActorData = _data;
            Position = _position;
            Rotation = _rotation;
        }
    }
}