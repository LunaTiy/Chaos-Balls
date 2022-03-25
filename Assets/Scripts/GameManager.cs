using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Transform _goals;
	[SerializeField] private Transform _balls;
	[SerializeField] private Transform _chaoses;
	[SerializeField] private Controller _bumperController;

	private bool _isEndGame = false;
	private bool _isPlaying = false;
	private float _elapsedTime = 0f;

	private void Start()
	{
		DisableObjects();
	}

	private void Update()
	{
		if (_isPlaying) _elapsedTime += Time.deltaTime;

		CheckEndGame();
	}

	private void OnGUI()
	{
		if (!_isPlaying)
		{
			string message;

			if (_isEndGame)
			{
				message = "Click or Press Enter to Play Again";
			}
			else
			{
				message = "Click or Press Enter to Play";
			}

			Rect startButton = new Rect(Screen.width / 2 - 120, Screen.height / 2, 240, 30);

			if (GUI.Button(startButton, message) || Input.GetKeyDown(KeyCode.Return))
			{
				StartGame();
			}
		}

		if (_isEndGame)
		{
			GUI.Box(new Rect(Screen.width / 2 - 65, 185, 130, 40), "Your Time Was");
			GUI.Label(new Rect(Screen.width / 2 - 10, 200, 40, 30), ((int)_elapsedTime).ToString());
		}
		else if (_isPlaying)
		{
			GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Your Time Is");
			GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height - 100, 40, 30), ((int)_elapsedTime).ToString());
		}
	}

	private void CheckEndGame()
	{
		Goal goal = _goals.GetChild(0).GetComponent<Goal>();
		_isEndGame = goal.isSolved;

		for (int i = 0; i < _goals.childCount; i++)
		{
			goal = _goals.GetChild(i).GetComponent<Goal>();
			_isEndGame = _isEndGame && goal.isSolved;

			if (!_isEndGame)
				return;
		}

		EndGame();
	}

	private void StartGame()
	{
		_elapsedTime = 0f;
		_isPlaying = true;
		_isEndGame = false;

		EnableObjects();
	}

	private void EnableObjects()
	{
		_bumperController.enabled = true;
		_bumperController.gameObject.transform.position = new Vector3(0, 1, 0);

		EnableChaoses();
		EnableGoals();
		EnableBalls();
	}

	private void EnableChaoses()
	{
		for (int i = 0; i < _chaoses.childCount; i++)
			_chaoses.GetChild(i).GetComponent<Chaos>().enabled = true;
	}

	private void EnableGoals()
	{
		for(int i = 0; i < _goals.childCount; i++)
		{
			Transform goal = _goals.GetChild(i);
			goal.GetComponent<Goal>().isSolved = false;
			goal.GetComponent<Light>().enabled = true;
		}
	}

	private void EnableBalls()
	{
		for(int i = 0; i < _balls.childCount; i++)
		{
			Transform ball = _balls.GetChild(i);
			ball.gameObject.SetActive(true);
			ball.position = new Vector3(Random.Range(-15, 15), 1.5f, Random.Range(-15, 15));
		}
	}

	private void EndGame()
	{
		_isPlaying = false;

		DisableObjects();
	}

	private void DisableObjects()
	{
		_bumperController.enabled = false;
		_bumperController.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

		for (int i = 0; i < _chaoses.childCount; i++)
		{
			Transform chaos = _chaoses.GetChild(i);

			chaos.GetComponent<Chaos>().enabled = false;
			chaos.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
}
