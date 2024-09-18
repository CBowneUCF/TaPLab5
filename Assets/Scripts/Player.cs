using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject laserPrefab;
    public float moveSpeed;
    public float turnSpeed;


    private float speed = 6f;
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 6f;
    private bool canShoot = true;
    private PlayerActions input;


    // Start is called before the first frame update
    void Awake()
    {
        input = new();
        input.Enable();
        input.Gameplay.Shoot.performed += _ => Shooting();
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




       


        //transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
        
    }

    void Shooting()
    {
        
            Instantiate(laserPrefab, transform.position, transform.rotation);
            //canShoot = false;
            //StartCoroutine("Cooldown");
        
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
