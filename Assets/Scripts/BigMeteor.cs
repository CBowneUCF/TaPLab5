using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    private int hitCount = 0;
    private bool isShaking = false;
    public GameObject explosionEffectBig;
    public GameObject playerExplosionBig;
    //public AudioClip playerDieSound;
    public float shakeDuration = 0.5f;    // Hit shaking
    public float shakeMagnitude = 0.1f;   // Magnitude of how much it shakes
    // Start is called before the first frame update
    
   // private AudioSource audioSource;
   // void Start()
   // {
    //    audioSource = GetComponent<AudioSource>(); // Audio sources
   // }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 0.5f);

        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }

        if (hitCount >= 5)
        {
            Instantiate(explosionEffectBig, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
           
            Instantiate(playerExplosionBig, whatIHit.transform.position, Quaternion.identity);

            //if (playerDieSound != null && audioSource != null)
           // {
           //     audioSource.PlayOneShot(playerDieSound);
           // }

            Destroy(whatIHit.gameObject);
        }
        else if (whatIHit.tag == "Laser")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            hitCount++;
            Destroy(whatIHit.gameObject);
            if (!isShaking)
            {
                StartCoroutine(ShakeMeteor());
            }
        }
        
    }
        IEnumerator ShakeMeteor()
    {
        isShaking = true;
        Vector3 originalPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            // Generate a random shake offset
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            // Apply the shake to the meteor's position
            transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Reset the position to original when shake is over
        transform.position = originalPosition;

        isShaking = false;
    }
}
