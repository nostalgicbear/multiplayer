using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColorManager : MonoBehaviourPun, IPunObservable
{
    public Material mat;
    public MeshRenderer mesh;
    public List<Rigidbody> boxes;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Photon.Pun.PhotonView>().IsMine && other.transform.parent.name.Contains("Hand"))
        {
            Debug.LogError("aaaa");
            PhotonView pv =GetComponent<PhotonView>();
                pv.RPC("TriggerMaterialChange", RpcTarget.AllBuffered);

                //Vector3 forceDirection = new Vector3(Random.Range(-5, 5),
                //    5,
                //    Random.Range(-5, 5)
                //    );

               // pv.RPC("TriggerBoxJump", RpcTarget.AllBuffered, forceDirection);
            }
        
    }

    [PunRPC]
    void TriggerMaterialChange()
    {
        Debug.LogError("bbbb");

        mesh.material = mat;
    }

    [PunRPC]
    void TriggerBoxJump(Vector3 dir)
    {
        foreach (Rigidbody rb in boxes)
        {
            rb.AddForce(dir, ForceMode.Impulse);
        }
    }
}
