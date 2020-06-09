using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PhotonTimer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI roundTimer;
    public double totalRoundTime = 50;
    public double currentRoundTime;

    // Start is called before the first frame update
    void Start()
    {

        Debug.LogError("People in room is " + PhotonNetwork.CurrentRoom.PlayerCount);
        if(!PhotonNetwork.LocalPlayer.IsMasterClient)
        {

        }

        currentRoundTime = totalRoundTime;


    }

    // Update is called once per frame
    void Update()
    {
        


        DecreaseTime();
        roundTimer.text = currentRoundTime.ToString();
    }

    private void DecreaseTime()
    {
        currentRoundTime -= Time.deltaTime;
    }

    [PunRPC]
    public void SetCurrentTime(double currentTimeFromServer)
    {
        currentRoundTime = currentTimeFromServer;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.LogError("RRR");
    }

}
