using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroid : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.name);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    



}
