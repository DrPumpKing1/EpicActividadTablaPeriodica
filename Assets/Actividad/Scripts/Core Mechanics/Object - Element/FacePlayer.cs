using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private IAvatar localAvatar;

    private void Start()
    {
        localAvatar = SpatialBridge.actorService.localActor.avatar;
    }

    private void Update()
    {
        Vector3 facingVector = localAvatar.position - transform.position;
        facingVector.y = 0;
        transform.forward = facingVector.normalized;
    }
}
