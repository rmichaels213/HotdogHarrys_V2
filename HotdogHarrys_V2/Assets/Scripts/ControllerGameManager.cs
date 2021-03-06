﻿/*----------------------------------------
Filename:		ControllerGameManager.cs	
Created By:		Ryan Michaels
Created Date:	04/21/2018
Updated Date:	04/21/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ControllerGameManager : MonoBehaviour
{
	public static ControllerGameManager controller;

	public IntVariable score;
	public IntVariable carsHit;
	public IntVariable hotDogsShot;
	public IntVariable hotDogsHit_Car;
	public IntVariable hotDogsHit_Person;
	public FloatVariable damages;

	public Canvas canvasGameOver;

	public bool isGameOver;
	public bool isPaused;
	public bool isWaitingOnGameOverScreen;
	public float currentTime;
	public float maxTimeAllowed = 20f;

	/// <summary>
	/// Runs one time at game creation.
	/// </summary>
	public void Awake()
	{
		if ( controller == null )
		{
			controller = this;
			DontDestroyOnLoad( controller );
		}
		else if ( this != controller )
		{
			Destroy( this );
		}

		if ( canvasGameOver != null )
		{
			canvasGameOver.gameObject.SetActive( false );
		}

		// Reset ga,e variables
		score.value = 0;
		carsHit.value = 0;
		hotDogsShot.value = 0;
		hotDogsHit_Car.value = 0;
		hotDogsHit_Person.value = 0;
		damages.value = 0.0f;

	}

	/// <summary>
	/// Raycast to see if we are clicking something
	/// </summary>
	public Collider2D CastRay()
	{
		Collider2D result = null;

		RaycastHit2D rayCast = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero );

		if ( rayCast.collider != null )
		{
			//Debug.Log( "Hey, we click on " + rayCast.collider.name );
			result = rayCast.collider;
		}

		return result;
	}

	/// <summary>
	/// Runs every frame.
	/// </summary>
	public void Update()
	{
		if ( SceneManager.GetActiveScene().name == "MainGame" ) {
			if ( !isPaused && !isWaitingOnGameOverScreen )
			{
				if ( isGameOver )
				{
					// Load the game over UI
					canvasGameOver.gameObject.SetActive( true );
					isWaitingOnGameOverScreen = true;

					TextMeshProUGUI[] text = canvasGameOver.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
					foreach ( TextMeshProUGUI t in text )
					{
						if ( t.name == "Stats" )
						{
							// Fill in the stats
							t.text = "Time Left : " + Mathf.FloorToInt( maxTimeAllowed - currentTime ) + " sec\n" +
								"Collisions: " + ControllerTruck.truck.numberOfCollisions + "\n" +
								"People fed: " + hotDogsHit_Person.value + "\n" +
								"Hot dogs shot: " + hotDogsShot.value + "\n" +
								"Accuracy: " + Math.Round( ( (float)hotDogsHit_Person.value / (float)hotDogsShot.value ) * 100, 2 ) + "%";
						}
						else if ( t.name == "Score" )
						{
							// Fill in score
							t.text = "Score: " + Mathf.FloorToInt( ( Mathf.FloorToInt( maxTimeAllowed - currentTime ) * hotDogsHit_Person.value ) - ( ( currentTime * ControllerTruck.truck.numberOfCollisions ) / 10 ) );
						}
					}

					return;
				}

				if ( currentTime >= maxTimeAllowed )
				{
					// Game is over, you lost!?
					isGameOver = true;
					Debug.Log( "Times up!" );
					return;
				}

				// Start the timer@
				currentTime = currentTime + ( 1 * Time.fixedDeltaTime );

				if ( Input.GetMouseButtonDown( 0 ) )
				{
					if ( !PointerOverUI() )
					{
						Collider2D collider = CastRay();
						ControllerTruck.truck.Shoot();
						//if ( collider != null && collider.name == "truck" )
						//{
						//	// We are clicking the truck, move the player
						//	Debug.Log( "Clicked the truck." );
						//	Vector2 target = Camera.main.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0f ) );
						//	//ControllerTruck.truck.MovePlayer( target );
						//}
						//else
						//{
						//	// We are probably trying to shoot something! Shoot in that direction, if we have hot dogs, of course.
						//	ControllerTruck.truck.Shoot();
						//}
					}
				}
			}
		}
	}

	/// <summary>
	/// Check if we are clicking a UI element
	/// </summary>
	/// <returns></returns>
	public bool PointerOverUI()
	{
		return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
	}
}
