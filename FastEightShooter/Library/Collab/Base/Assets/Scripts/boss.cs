using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform player;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Color normalColor;
    Color hitColor;

    [SerializeField] float padding = 1f;
    [SerializeField] int health = 3000;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;

        Renderer rend = GetComponent<Renderer>();
        normalColor = rend.material.color;
        hitColor = Color.red;
        SetUpMoveBoundaries();
    }

    // Function to process 2D collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
        StartCoroutine(Flasher(normalColor, hitColor));
    }

    IEnumerator Flasher(Color normColor, Color hitColor)
    {




        for (int i = 0; i < 2; i++)
        {
            GetComponent<Renderer>().material.color = hitColor;
            yield return new WaitForSeconds(.1f);
            GetComponent<Renderer>().material.color = normColor;
            yield return new WaitForSeconds(.1f);
        }
    }

    // Function called when 2D collision is detected
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    // Function called to destroy player when health falls below zero
    private void Die()
    {

        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, 1f);
        FindObjectOfType<Level>().LoadGameWon();

    }

    IEnumerator WaitAndDestroy(float delay, GameObject explosionanim)
    {
        yield return new WaitForSeconds(delay);
        Destroy(explosionanim);
    }

    public int GetHealth()
    {
        return health;
    }


    void Update () {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {

                transform.position = this.transform.position;

            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }
        }

        if (timeBtwShots <= 0){

            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;

        }
        else {

            timeBtwShots -= Time.deltaTime;
        }

	}

    // Set boundaries for boss movement. 
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}



