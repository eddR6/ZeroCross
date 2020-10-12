//DEMO PLAYER FOR TESTING THE PHOTON NETWORK
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerController :MonoBehaviourPunCallbacks//,IPunObservable
{
    public float speed;
    public PhotonView pv;

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float stray = Input.GetAxis("Horizontal");
        transform.position = (new Vector3(speed * stray * Time.deltaTime, transform.position.y, 0)) + transform.position;
        //pv.TransferOwnership(2);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        if (stream.IsReading)
        {
            Vector3 pos = (Vector3)stream.ReceiveNext();
            ToNetworkPosition(pos);
        }
    }

    private void ToNetworkPosition(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
    }
}
