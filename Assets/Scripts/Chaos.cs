using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaos : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;

	private Rigidbody _rb;
	private Vector3 oldPosition;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		transform.Rotate(0, Random.Range(0, 360), 0);
	}

	private void FixedUpdate()
	{
		_rb.AddForce(transform.forward * _speed);
	}
}
