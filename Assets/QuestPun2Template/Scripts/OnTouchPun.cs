using UnityEngine;
using UnityEngine.Events;

namespace Networking.Pun2
{
    public class OnTouchPun : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTouch;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponentInParent<Photon.Pun.PhotonView>().IsMine)
                InvokeOnTouch();
        }

        public void InvokeOnTouch()
        {
            onTouch.Invoke();
        }
    }
}
