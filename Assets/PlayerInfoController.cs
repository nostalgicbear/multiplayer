using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;

public class PlayerInfoController : MonoBehaviourPun
{

    [SerializeField]
    private TextMeshProUGUI nameText;



    public int teamNumber;
    public int coinsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {

            return;
        }

        SetName();
    }

    private void SetName()
    {
        nameText.text = photonView.Owner.NickName;
        teamNumber = PhotonNetwork.PlayerList.Length;
    }
}
