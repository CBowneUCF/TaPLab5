using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject laserPrefab;
    public float moveSpeed;
    public float turnSpeed;
    public Transform laserParent;
    public GameObject deathVFX;
    public AudioClip deathSFX;

    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 6f;
    private bool canShoot = true;
    private PlayerActions input;
    Vector2 screenSize;
    Plane XYPlane = new(Vector2.up, Vector2.right);

    // Start is called before the first frame update
    void Awake()
    {
        input = new();
        input.Enable();
        input.Gameplay.Shoot.performed += _ => Shooting();

        ChangeCamSize();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //Shooting();
    }

    void Movement()
    {
        transform.Translate(input.Gameplay.Movement.ReadValue<Vector2>() * Time.deltaTime * moveSpeed, Space.World);
        transform.Rotate(Vector3.back * Time.deltaTime * turnSpeed * input.Gameplay.Turn.ReadValue<float>() * 10f);

        /*

        bool offscreenW = false;
        bool offscreenH = false;

        if (transform.position.x > screenSize.x + 1 || transform.position.x <= -screenSize.x - 1) offscreenW = true;
        if (transform.position.y > screenSize.y + 1 || transform.position.y <= -screenSize.y - 1) offscreenH = true;

        transform.position = new Vector3(
            offscreenW ? -transform.position.x : transform.position.x, 
            offscreenH ? -transform.position.y : transform.position.y, 
            0);

         */


    }

    void Shooting()
    {
        
            Instantiate(laserPrefab, transform.position, transform.rotation, parent: laserParent);
            //canShoot = false;
            //StartCoroutine("Cooldown");
        
    }

    public void DIE()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
        input.Gameplay.Restart.performed += _ => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
    }

    void ChangeCamSize()
    {
        screenSize = Camera.main.ViewportToWorldPoint(new(1, 1, -Camera.main.transform.position.z));
    }


    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
