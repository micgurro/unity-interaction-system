using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace meekobytes
{
    public class CharacterInteractionManager : MonoBehaviour
    {
        public static CharacterInteractionManager Instance;
        public static UnityAction<SaveData> OnLoadGame;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
                SaveLoad.OnLoadGame += LoadActiveCharacters;
            }

        }

        private void LoadActiveCharacters(SaveData saveData)
        {
            RefreshCharacterInteractionsInScene(saveData);
        }

        private void RefreshCharacterInteractionsInScene(SaveData saveData)
        {
            foreach (var id in saveData.characterInteractionsList)
            {
            }
        }
    }

}