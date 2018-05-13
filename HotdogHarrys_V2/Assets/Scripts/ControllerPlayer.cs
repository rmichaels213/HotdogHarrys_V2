/*----------------------------------------
Filename:		ControllerPlayer.cs	
Created By:		Ryan Michaels
Created Date:	04/22/2018
Updated Date:	04/22/2018
----------------------------------------*/

using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
	void OnCollisionEnter( Collision col )
	{
		if ( col.gameObject.name == "truckBorder" )
		{
			ControllerTruck.truck.isPlayerMoving = false;
		}
	}
}
