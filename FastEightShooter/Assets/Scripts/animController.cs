using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class animController : MonoBehaviour {

    public GameObject spaceship;
    float y = 0;
    public Animator anim;

    public static bool IsAnimationStarted = false;
    public static int SceneIndex = 0;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        if (IsAnimationStarted)
        {
            y = y + 0.9f;
            spaceship.transform.position = new Vector3(spaceship.transform.position.x, y);
        }

        if(IsAnimationStarted && y > 60)
        {
            IsAnimationStarted = false;
            SceneManager.LoadScene(SceneIndex);
            var gameSessions = FindObjectOfType<GameSession>();
            if(gameSessions != null)
                gameSessions.ResetGame();
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            //anim.SetTrigger("Active");
            //anim.Play("NewAnimation");
        }
	}
}
