/*----------------------------------------
Filename:		ControllerCar.cs	
Created By:		Ryan Michaels
Created Date:	04/23/2018
Updated Date:	04/23/2018
----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCar : MonoBehaviour
{
	private bool hasBeenHit_HotDog;
	private bool hasBeenHit_Player;

	public void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.tag == "Player" && !hasBeenHit_Player )
		{
			hasBeenHit_Player = true;
			ControllerTruck.truck.numberOfCollisions++;
			ControllerGameManager.controller.carsHit.value++;
		}
		else if ( collision.gameObject.tag == "HotDog" && !hasBeenHit_HotDog )
		{
			hasBeenHit_HotDog = true;
			ControllerGameManager.controller.hotDogsHit_Person.value++;
		}
	}
}
