using UnityEngine;

namespace Game.Character
{
    public class ActionPerformObserver : MonoBehaviour
    {
        public event System.Action ActionPerformed;

        void OnDrop() // можешь менять метод как хочешь, но invoke события должен вызываться при выкидывании
        {
            ActionPerformed?.Invoke();
            Destroy(transform.gameObject);
        }
    }
}