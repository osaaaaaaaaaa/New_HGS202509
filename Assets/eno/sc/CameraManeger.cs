using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraManeger : MonoBehaviour
{
    [SerializeField]
    float defaultSpeed = 5f;
    Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Startmove()
    {
        body.linearVelocity = Vector3.up * defaultSpeed;
    }

    public void StopMove()
    {
        body.linearVelocity = Vector3.zero;
    }

    public void SpeedUp(float addSpeed)
    {
        body.linearVelocity = Vector3.up * (defaultSpeed + addSpeed);
    }
}
