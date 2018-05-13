using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPerson : MonoBehaviour
{
	private bool hasBeenHit;

	public void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.tag == "HotDog" && !hasBeenHit )
		{
			hasBeenHit = true;
			ControllerTruck.truck.foodSold++;
		}
	}
}
