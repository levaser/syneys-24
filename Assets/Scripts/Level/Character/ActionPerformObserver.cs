using UnityEngine;

namespace Game.Character
{
    public class ActionPerformObserver : MonoBehaviour
    {
        public event System.Action<DefaultAction> ActionPerformed;

        public DefaultAction _actionConfig;

        public void Initialize(DefaultAction actionConfig)
        {
            _actionConfig = actionConfig;
        }

        void OnDrop()
        {
            // можешь менять метод как хочешь, но invoke события должен вызываться при выкидывании
            PerformAction();
        }

        private void PerformAction()
        {
            if (_actionConfig == null)
                throw new System.ArgumentNullException(nameof(_actionConfig), "Parameter '_actionConfig' cannot be null");
            ActionPerformed?.Invoke(_actionConfig);
            Destroy(transform.gameObject);
        }
    }
}