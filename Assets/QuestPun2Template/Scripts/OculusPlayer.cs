﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//For easily accesing local head and hand anchors
//
namespace Networking.Pun2
{
    public class OculusPlayer : MonoBehaviour
    {
        public GameObject head;
        public GameObject rightHand;
        public GameObject leftHand;
        public PlayerInfoController playerInfoController;
        public PersonalManager personalManager;

        public static OculusPlayer instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}
