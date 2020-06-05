using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

namespace Networking.Pun2
{
    //Changes the ownership of an interactable, as PUN uses this "ownership" approach for handling objects in the network
    public class ChangeOwner : MonoBehaviourPun
    {
        OVRGrabbable grabbable;

        private void Start()
        {
            grabbable = GetComponent<PunOVRGrabbable>();
        }

        public void SetNewOwner()
        {
            if (photonView.IsMine)
            {
                grabbable.grabbedBy.transform.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            }
        }
    }
}
