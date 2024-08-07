using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    private Vector2 direction;



    // Start is called before the first frame update
    void Start()
    {
        // player의 스프라이트를 받아오기 위해 Find 함수 사용 
        GameObject otherObject = GameObject.Find("Player");
        SpriteRenderer playerRenderer = otherObject.GetComponent<SpriteRenderer>();

        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.UpArrow))  
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.right;
        }

        Invoke("DestroyBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
        // 총알을 설정된 방향으로 이동
        transform.Translate(direction * speed * Time.deltaTime);

        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, distance, isLayer);
        if (ray.collider != null)
        {
            {
                Debug.Log(ray.collider.name + " 명중!");
            }
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
