using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shooting : NetworkBehaviour
{
    public Transform _firePoint;
    public GameObject _FireBallPrefab;
    public float _FireBallSpeed = 10f;

    [ServerRpc]
    public void ShootServerRpc()
    {
        GameObject FireBall = Instantiate(_FireBallPrefab,_firePoint.position, _firePoint.rotation);

        FireBall.GetComponent<NetworkObject>().Spawn();

        Quaternion playerRotation = transform.rotation;
        
        FireBall.transform.rotation = playerRotation;
        
        Rigidbody2D rb = FireBall.GetComponent<Rigidbody2D>();
        rb.AddForce(_firePoint.up * _FireBallSpeed, ForceMode2D.Impulse);
        
    }
}
