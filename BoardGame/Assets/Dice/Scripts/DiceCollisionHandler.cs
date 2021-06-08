using System;
using UnityEngine;

namespace Dice.Scripts
{
    public class DiceCollisionHandler : MonoBehaviour
    {
        public Action<GameObject> TriggerStay { get; set; }

        private void OnTriggerStay(Collider other)
        {
            TriggerStay?.Invoke(other.gameObject);
        }
    }
}
