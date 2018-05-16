/*----------------------------------------
Filename:		ControllerCarGenerator.cs	
Created By:		Ryan Michaels
Created Date:	04/23/2018
Updated Date:	04/23/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCarGenerator : MonoBehaviour
{
	public const float ROAD_X_POSITION_LEFT = -2F;
	public const float ROAD_X_POSITION_LEFT_CURB = -5.25F;
	public const float ROAD_X_POSITION_RIGHT = 2F;
	public const float ROAD_X_POSITION_RIGHT_CURB = 5.25F;

	public GameObject carPrefab1;
	public GameObject carPrefab2;
	public GameObject carPrefab3;

	public int minNumberOfCars = 0;
	public int maxNumberOfCars = 5;
	public int slowestSpeed = 10;
	public int fastestSpeed = 50;


	private GameObject[] carPrefabs;
	private int actualNumberOfCarPrefabs;
	private List<GameObject> cars;
	private int lastRoadPopulated;
	private bool isFirstRoadPopulated;

	private void PopulateCurrentRoad()
	{
		int numberOfCarsToCreate = (int)Random.Range( minNumberOfCars, maxNumberOfCars );
		Debug.Log( "Creating " + numberOfCarsToCreate + " cars." );

		while ( numberOfCarsToCreate > 0 )
		{
			numberOfCarsToCreate--;

			int randomCar = (int)Random.Range( 0.0f, actualNumberOfCarPrefabs );
			int randomLocation = (int)Random.Range( 0.0f, 4.0f );
			int randomPosition = (int)Random.Range( -14.0f, 14.0f );

			float randomColorR = Random.Range( 0.0f, 1.0f );
			float randomColorG = Random.Range( 0.0f, 1.0f );
			float randomColorB = Random.Range( 0.0f, 1.0f );

			Vector2 velocity = new Vector2( 0f, 0f );
			float locationToUse;
			switch (randomLocation)
			{
				case 0:
					locationToUse = ROAD_X_POSITION_LEFT;
					velocity = new Vector2( 0f, -Random.Range( slowestSpeed, fastestSpeed ) );
					break;
				case 1:
					locationToUse = ROAD_X_POSITION_LEFT_CURB;
					break;
				case 2:
					locationToUse = ROAD_X_POSITION_RIGHT;
					velocity = new Vector2( 0f, Random.Range( slowestSpeed, fastestSpeed ) );
					break;
				case 3:
				default:
					locationToUse = ROAD_X_POSITION_RIGHT_CURB;
					break;
			}

			Debug.Log( randomCar + " " + randomLocation + " " + randomPosition );
			Debug.Log( "Creating " + carPrefabs[randomCar].gameObject.name + " at " + randomLocation + " with an offset of " + randomPosition +
				" and colors (" + randomColorR + "," + randomColorG + "," + randomColorB + ")." );

			GameObject newCar = Instantiate( carPrefabs[randomCar], new Vector3( locationToUse, ( ( ControllerRoad.road.roads.Count - 1 ) * ControllerRoad.road.offset ) + randomPosition, 0f ), Quaternion.identity );
			newCar.GetComponent<Rigidbody2D>().velocity = velocity;
			cars.Add( newCar );

			// Update car color
			if ( carPrefabs[randomCar].gameObject.name == "carPrefab")
			{
				Debug.Log( "Found prefab!" );
				SpriteRenderer[] sprites = newCar.GetComponentsInChildren<SpriteRenderer>();

				foreach ( SpriteRenderer sr in sprites )
				{
					Debug.Log( "Found sprite: " + sr.gameObject.name );
					if ( sr.gameObject.name == "carFrame")
					{
						sr.gameObject.GetComponent<SpriteRenderer>().color = new Color( randomColorR, randomColorG, randomColorB, 1.0f );
					}
				}
			}
			else
			{
				newCar.GetComponent<SpriteRenderer>().color = new Color( randomColorR, randomColorG, randomColorB, 1.0f );
			}
		}		
	}

	/// <summary>
	/// 
	/// </summary>
	void Start()
	{
		cars = new List<GameObject>();

		lastRoadPopulated = 0;

		carPrefabs = new GameObject[10];

		carPrefabs[0] = carPrefab3;
		actualNumberOfCarPrefabs++;

		PopulateCurrentRoad();
	}

	/// <summary>
	/// 
	/// </summary>
	void Update()
	{
		if ( ControllerRoad.road.currentRoad == 0 && !isFirstRoadPopulated )
		{
			// If this is the beginning, populate that first section of road
			PopulateCurrentRoad();
			isFirstRoadPopulated = true;
		}

		if ( ControllerRoad.road.currentRoad > lastRoadPopulated )
		{
			PopulateCurrentRoad();
			lastRoadPopulated = ControllerRoad.road.currentRoad;
		}
	}
}
