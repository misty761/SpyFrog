using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject enemy;
    public GameObject heart;

    public void SpawnItem()
    {
        float random = Random.Range(0f, 1f);
        float probabilityLifeUp = 0.5f;
        if (random < probabilityLifeUp)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.audioSpawnItem, 0.2f);
            Vector2 position = new Vector2(transform.position.x, transform.position.y + 0.3f);
            GameObject goHeart = Instantiate(heart, position, Quaternion.Euler(Vector2.zero));
            Rigidbody2D rb = goHeart.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 50f));
        }
        else
        {
            SoundManager.instance.PlaySound(SoundManager.instance.audioNegative, 1f);
            Instantiate(enemy, transform.position, Quaternion.Euler(Vector2.zero));
        } 
    }
}
