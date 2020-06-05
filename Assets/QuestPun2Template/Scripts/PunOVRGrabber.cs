using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking.Pun2
{
    [RequireComponent (typeof (PhotonView))]
    public class PunOVRGrabber : OVRGrabber
    {
        PhotonView pv;
        protected override void Awake()
        {
            base.Awake();
            pv = GetComponent<PhotonView>();
        }
        public override void FixedUpdate()
        {
            if(pv.IsMine)
                base.FixedUpdate();
        }
    }
}
