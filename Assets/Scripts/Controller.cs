using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _angularSpeed = 5f;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveLogic();
    }

    private void MoveLogic()
	{
        float mX = Input.GetAxis("Horizontal") * _angularSpeed;
        float mY = Input.GetAxis("Vertical") * _speed;

        Vector3 movement = transform.forward * mY + transform.right * mX;

        _rb.AddForce(movement);
		_rb.angularVelocity = new Vector3(0, mX * _angularSpeed, 0);
	}
}
