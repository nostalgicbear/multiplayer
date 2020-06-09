using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Networking.Pun2;

[RequireComponent(typeof(PhotonView))]
public class CollectableCoin : MonoBehaviourPun, IPunObservable
{
    PhotonView pv;
    public UnityEvent OnTouched;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError(other.name + " is whats colliding with the coin");
        if (other.gameObject.GetComponentInParent<Photon.Pun.PhotonView>().IsMine) //it was my hand that touched it 
            ObjectTouched(other.gameObject);
    }

    [PunRPC]
    void RPC_IncreaseCoins(int objToIncreaseCoinsOn)
    {;
        PlayerInfoController playerInfoController = PhotonView.Find(objToIncreaseCoinsOn).GetComponent<PlayerInfoController>();
        playerInfoController.coinsCollected += 1;

        Destroy(gameObject);
    }

    public void ObjectTouched(GameObject go)
    {
        int viewID = OculusPlayer.instance.GetComponent<PhotonView>().ViewID;

        
        pv.RPC("RPC_IncreaseCoins", RpcTarget.All, viewID);

        OnTouched.Invoke();
    }
}
