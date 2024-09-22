using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public int health;
    public GameObject explosionEffect;
    public float shakeDuration = 0.5f;    // Hit shaking
    public float shakeMagnitude = 0.1f;   // Magnitude of how much it shakes
    public AudioClip destroySFX;

	
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2f);

        if (transform.position.y < Camera.main.transform.position.y -11f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            whatIHit.GetComponent<Player>().DIE();
            Destroy(this.gameObject);
        } 
        else if (whatIHit.tag == "Laser")
        {
            Destroy(whatIHit.gameObject);
            health--;
			if (!isShaking) StartCoroutine(ShakeMeteor());
			if (health <= 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().meteorCount++;
                GetComponent<CinemachineImpulseSource>().GenerateImpulse(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * shakeMagnitude);
                
                Destroy(gameObject);
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
				AudioSource.PlayClipAtPoint(destroySFX, transform.position);
			}
        }
    }

	private bool isShaking = false;
	IEnumerator ShakeMeteor()
	{
		isShaking = true;
		Vector3 originalPosition = transform.position;

		float elapsedTime = 0f;
		while (elapsedTime < shakeDuration)
		{
			// Generate a random shake offset
			float offsetX = Random.Range(-1f, 1f) * 0.1f;
			float offsetY = Random.Range(-1f, 1f) * 0.1f;

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
