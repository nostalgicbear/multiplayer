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
        if (other.gameObject.GetComponentInParent<Photon.Pun.PhotonView>().IsMine)
            ObjectTouched(other.gameObject);
    }

    [PunRPC]
    void RPC_IncreaseCoins()
    {;
        Debug.LogError("About to increase coins collected");
        OculusPlayer.instance.playerInfoController.coinsCollected += 1;
        Debug.LogError("About to destroy coin object");

        Destroy(gameObject);
    }

    public void ObjectTouched(GameObject go)
    {
        //int viewID = go.gameObject.GetComponentInParent<Photon.Pun.PhotonView>().ViewID;


        pv.RPC("RPC_IncreaseCoins", RpcTarget.All);

        OnTouched.Invoke();
    }
}
