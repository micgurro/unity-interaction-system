using UnityEngine;

namespace meekobytes
{
    [CreateAssetMenu(menuName = "NPC System/NPC Actor Data")]
    public class NPCActorData : ScriptableObject
    {
        public int ID = -1;
        public string ActorName;
        [TextArea(4, 4)]
        public string Description;
        public Sprite Sprite;
        public GameObject ActorPrefab;

        public Message[] messages;
        public Actor[] actors;

    }
}