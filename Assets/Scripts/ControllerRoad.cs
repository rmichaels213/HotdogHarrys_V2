/*----------------------------------------
Filename:		ControllerRoad.cs	
Created By:		Ryan Michaels
Created Date:	04/22/2018
Updated Date:	04/22/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRoad : MonoBehaviour
{
	public static ControllerRoad road;

	public GameObject roadPrefabBlock;
	public GameObject roadPrefab1;
	public GameObject roadEnd;

	public int currentRoad;
	public int maxNumberOfRoads = 10;
	public bool noMoreRoads;
	public float offset = 28f;
	public List<GameObject> roads;

	private void AddNewRoad()
	{
		GameObject newRoad = Instantiate( roadPrefab1, new Vector3( 0f, roads.Count * offset, 0f ), Quaternion.identity );
		roads.Add( newRoad );
	}

	private void AddEndRoad()
	{
		GameObject newRoad = Instantiate( roadEnd, new Vector3( 0f, roads.Count * offset, 0f ), Quaternion.identity );
		roads.Add( newRoad );
	}

	/// <summary>
	/// 
	/// </summary>
	public void Awake()
	{
		road = this;
	}

	/// <summary>
	/// Reset the roads back to the beginning
	/// </summary>
	public void Reset()
	{
		foreach( GameObject g in roads )
		{
			Destroy( g );
		}

		currentRoad = 0;
		noMoreRoads = false;
		roads = new List<GameObject>
		{
			gameObject
		};

		// Add blocking road at bottom
		Instantiate( roadPrefabBlock, new Vector3( 0f, -offset, 0f ), Quaternion.identity );

		// Add a new road to start
		AddNewRoad();
	}

	/// <summary>
	/// Runs when object is created.
	/// </summary>
	public void Start()
	{
		currentRoad = 0;
		noMoreRoads = false;
		roads = new List<GameObject>
		{
			gameObject
		};

		// Add blocking road at bottom
		Instantiate( roadPrefabBlock, new Vector3( 0f, -offset, 0f ), Quaternion.identity );

		// Add a new road to start
		AddNewRoad();
	}

	/// <summary>
	/// Runs every frame.
	/// </summary>
	public void Update()
	{
		if ( !noMoreRoads )
		{
			if ( currentRoad >= maxNumberOfRoads )
			{
				// We are at the end! Add the last road prefab
				AddEndRoad();
				noMoreRoads = true;
			}

			if ( ControllerTruck.truck.transform.position.y > ( roads[currentRoad].gameObject.transform.position.y + ( offset / 2 ) ) )
			{
				Debug.Log( "Moving current road up one." );
				currentRoad++;
			}

			if ( currentRoad > 0 )
			{
				if ( ControllerTruck.truck.transform.position.y < ( roads[currentRoad - 1].gameObject.transform.position.y + ( offset / 2 ) ) )
				{
					Debug.Log( "Moving current road down one." );
					currentRoad--;
				}
			}

			while ( ( roads.Count - 1 ) <= currentRoad )
			{
				AddNewRoad();
			}
		}
	}
}
