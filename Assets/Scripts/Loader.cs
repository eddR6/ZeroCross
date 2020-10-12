using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Loader : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation);
    }

}
