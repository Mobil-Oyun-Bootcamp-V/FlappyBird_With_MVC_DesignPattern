using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private GameObject background1, bakcground2;
    [SerializeField] Rigidbody2D rigidBody1, rigidBody2;

    float backgroundLength = 0;
    void Start()
    {
        backgroundLength = background1.GetComponent<BoxCollider2D>().size.x;
    }
    void Update()
    {
        if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.Prepare)
        {
            rigidBody1.velocity = Vector2.zero;
            rigidBody2.velocity = Vector2.zero;
        }
        else if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.MainGame)
        {
            rigidBody1.velocity = new Vector2(-1.5f, 0);
            rigidBody2.velocity = new Vector2(-1.5f, 0);
            if (background1.transform.position.x <= -backgroundLength)
            {
                background1.transform.position += new Vector3(backgroundLength * 2, 0, 0);
            }
            if (bakcground2.transform.position.x <= -backgroundLength)
            {
                bakcground2.transform.position += new Vector3(backgroundLength * 2, 0, 0);
            }
        }
        else if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.FinishGame)
        {
            rigidBody1.velocity = Vector2.zero;
            rigidBody2.velocity = Vector2.zero;
        }
        
    }
}
