/*----------------------------------------
Filename:		ControllerHotDog.cs	
Created By:		Ryan Michaels
Created Date:	04/22/2018
Updated Date:	04/22/2018
----------------------------------------*/

using UnityEngine;

public class ControllerHotDog : MonoBehaviour
{
	private float speed;

	/// <summary>
	/// Run on collision
	/// </summary>
	/// <param name="collision"></param>
	void OnCollisionEnter2D( Collision2D collision )
	{
		//TODO: Doesn't seem to ignore Player inside truck collisions?
		if ( collision.gameObject.tag == "Player" )
		{
			Debug.Log( "Ignoring collision between hot dog and " + collision.gameObject.name );
			Physics2D.IgnoreCollision( GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>() );
		}
	}

	/// <summary>
	/// Runs once when created.
	/// </summary>
	public void Start()
	{
		speed = Random.Range( -360f, 360f );
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		
		GetComponent<Rigidbody2D>().rotation += speed * Time.deltaTime;
	}
}
