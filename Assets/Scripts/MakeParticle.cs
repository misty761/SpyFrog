using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeParticle : MonoBehaviour
{
    public GameObject particle;
    EnemyBehavior enemy;
    public float intervalParticle = 1f;
    public float particleOffsetX = 0.2f;
    public float particleOffsetY = 0.15f;
    float timeParticle;
    Vector2 position;

    private void Start()
    {
        enemy = GetComponent<EnemyBehavior>();
        timeParticle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        timeParticle += Time.deltaTime;
        if (timeParticle > intervalParticle)
        {
            timeParticle = 0f;

            if (enemy.isLookRight)
            {
                position = new Vector2(transform.position.x - particleOffsetX, transform.position.y + particleOffsetY);
            }
            else
            {
                position = new Vector2(transform.position.x + particleOffsetX, transform.position.y + particleOffsetY);
            }
            Instantiate(particle, position, Quaternion.Euler(Vector2.zero));

        }
        
    }
}
