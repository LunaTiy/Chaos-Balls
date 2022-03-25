using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
	[SerializeField] private float _speed = 5f;
	[SerializeField] private float _angularSpeed = 10f;
	[SerializeField] LayerMask masksObstacles;

	private Vector3 _position;


	private void Start()
	{
		_position = _target.transform.InverseTransformPoint(transform.position);
	}

	private void FixedUpdate()
	{
		Follow();
		Look();
		CheckObstacles();
	}

	private void Follow()
	{
		Quaternion oldRotation = _target.rotation;
		_target.rotation = Quaternion.Euler(0, oldRotation.eulerAngles.y, 0);
		Vector3 currentPosition = _target.transform.TransformPoint(_position);
		_target.rotation = oldRotation;

		transform.position = Vector3.Lerp(transform.position, currentPosition, _speed);
	}

	private void Look()
	{
		Quaternion currentRotation = Quaternion.LookRotation(_target.position - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, _angularSpeed);
	}

	private void CheckObstacles()
	{
		RaycastHit hit;

		if(Physics.Raycast(_target.position, transform.position - _target.position, out hit,
			Vector3.Distance(transform.position, _target.position), masksObstacles))
		{
			transform.position = hit.point;
			transform.LookAt(_target.position);
		}
	}
}
