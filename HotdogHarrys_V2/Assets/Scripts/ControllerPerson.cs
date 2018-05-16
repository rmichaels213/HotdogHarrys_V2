using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPerson : MonoBehaviour
{
	private bool hasBeenHit;
	public GameObject hitBubble;

	public void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.tag == "HotDog" && !hasBeenHit )
		{
			hasBeenHit = true;
			ControllerGameManager.controller.hotDogsHit_Person.value++;

			GameObject hit = Instantiate( hitBubble, transform.position, Quaternion.identity );
			hit.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
			Destroy( hit, 2f );
		}
	}
}
