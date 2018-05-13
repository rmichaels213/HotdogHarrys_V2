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
	private bool hasBeenHit;

	public void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.tag == "Player" && !hasBeenHit )
		{
			hasBeenHit = true;
			ControllerTruck.truck.numberOfCollisions++;
		}
	}
}
