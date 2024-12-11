using Photon.Pun;
using UnityEngine;

public class CubeSpawner : MonoBehaviourPun
{
    public GameObject cubePrefab;  // Reference to your cube prefab

    void Start()
    {
        // Ensure that we are connected to Photon and the master client is handling the instantiation
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            // Instantiate the cube for all players in the game
            GameObject cube = PhotonNetwork.Instantiate(cubePrefab.name, new Vector3(0, 0, 0), Quaternion.identity);

            // Transfer ownership to the local player (or the player who should control it)
            cube.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }
}
