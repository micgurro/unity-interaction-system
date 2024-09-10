using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace meekobytes
{
    public class DoorTrigger : MonoBehaviour
    {
        [SerializeField]
        private Door _door;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
            {
                if (_door.IsOpen)
                {
                    _door.Open(other.transform.position);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
            {
                if (_door.IsOpen)
                {
                    _door.Close();
                }
            }
        }
    }
}
