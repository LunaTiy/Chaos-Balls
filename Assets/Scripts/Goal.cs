using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isSolved = false;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == tag)
		{
			isSolved = true;
			GetComponent<Light>().enabled = false;
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);
		}
	}
}
