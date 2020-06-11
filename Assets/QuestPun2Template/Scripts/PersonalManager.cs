using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//
//For handling local objects and sending data over the network
//
namespace Networking.Pun2
{
    public class PersonalManager : MonoBehaviourPun
    {
        public bool useCustomHeads = false;
        [SerializeField] GameObject headPrefab;
        [SerializeField] GameObject handRPrefab;
        [SerializeField] GameObject handLPrefab;
        [SerializeField] GameObject ovrCameraRig;
        [SerializeField] Transform[] spawnPoints;

        public string headToUse;

        PhotonView headPhotonView;

        //Tools
        List<GameObject> toolsR;
        List<GameObject> toolsL;
        int currentToolR;
        int currentToolL;

        public PhotonView ReturnHeadPhotonView()
        {
            return headPhotonView;
        }

        private void Awake()
        {
            //For accelerating testing///////
            if (!PhotonNetwork.NetworkingClient.IsConnected)
            {
                SceneManager.LoadScene("Photon2Lobby");
                return;
            }
            /////////////////////////////////

            toolsR = new List<GameObject>();
            toolsL = new List<GameObject>();

            if(PhotonNetwork.LocalPlayer.ActorNumber <= spawnPoints.Length)
            {
                ovrCameraRig.transform.position = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position;
                ovrCameraRig.transform.rotation = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.rotation;
            }
        }

        private void Start()
        {
            //Instantiate Head
            GameObject obj = (PhotonNetwork.Instantiate(headToUse, OculusPlayer.instance.head.transform.position, OculusPlayer.instance.head.transform.rotation, 0));

            //GameObject obj = (PhotonNetwork.Instantiate(headPrefab.name, OculusPlayer.instance.head.transform.position, OculusPlayer.instance.head.transform.rotation, 0));
            // obj.GetComponent<SetColor>().SetColorRPC(PhotonNetwork.LocalPlayer.ActorNumber);
            OculusPlayer.instance.playerInfoController = obj.GetComponent<PlayerInfoController>();
            OculusPlayer.instance.personalManager = this;
            headPhotonView = obj.GetComponent<PhotonView>();

            //Instantiate right Tools
            obj = (PhotonNetwork.Instantiate(handRPrefab.name, OculusPlayer.instance.rightHand.transform.position, OculusPlayer.instance.rightHand.transform.rotation, 0));
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                toolsR.Add(obj.transform.GetChild(i).gameObject);
                obj.transform.GetChild(i).GetComponent<SetColor>().SetColorRPC(PhotonNetwork.LocalPlayer.ActorNumber);
                if(i > 0)
                    toolsR[1].transform.parent.GetComponent<PhotonView>().RPC("DisableTool", RpcTarget.AllBuffered, 1);
            }

            //Instantiate left Tools
            obj = (PhotonNetwork.Instantiate(handLPrefab.name, OculusPlayer.instance.leftHand.transform.position, OculusPlayer.instance.leftHand.transform.rotation, 0));
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                toolsL.Add(obj.transform.GetChild(i).gameObject);
                obj.transform.GetChild(i).GetComponent<SetColor>().SetColorRPC(PhotonNetwork.LocalPlayer.ActorNumber);
                if (i > 0)
                    toolsL[1].transform.parent.GetComponent<PhotonView>().RPC("DisableTool", RpcTarget.AllBuffered, 1);
            }
        }

        private void Update()
        {
            if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick))
                SwitchToolL();

            if (OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick))
                SwitchToolR();
        }

        void SwitchToolR()
        {
            toolsR[currentToolR].transform.parent.GetComponent<PhotonView>().RPC("DisableTool", RpcTarget.AllBuffered, currentToolR);
            currentToolR++;
            if (currentToolR > toolsR.Count - 1)
                currentToolR = 0;
            toolsR[currentToolR].transform.parent.GetComponent<PhotonView>().RPC("EnableTool", RpcTarget.AllBuffered, currentToolR);
        }

        void SwitchToolL()
        {
            toolsL[currentToolL].transform.parent.GetComponent<PhotonView>().RPC("DisableTool", RpcTarget.AllBuffered, currentToolL);
            currentToolL++;
            if (currentToolL > toolsL.Count - 1)
                currentToolL = 0;
            toolsL[currentToolL].transform.parent.GetComponent<PhotonView>().RPC("EnableTool", RpcTarget.AllBuffered, currentToolL);
        }
    }
}
