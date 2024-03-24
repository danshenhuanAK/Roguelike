using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSeal : MonoBehaviour
{
    private new SpriteRenderer renderer;

    public float openSpeed;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        renderer.color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            renderer.color = new Color(renderer.color.r - openSpeed * Time.deltaTime, renderer.color.g - openSpeed * Time.deltaTime, renderer.color.b - openSpeed * Time.deltaTime);

            if (renderer.color.r <= Color.black.r && renderer.color.g <= Color.black.g && renderer.color.b <= Color.black.b)
            {
                transform.parent.gameObject.GetComponent<EnemySpawner>().CreateBoss();
                gameObject.SetActive(false);
            }
        }
    }
}
