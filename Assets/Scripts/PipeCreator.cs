using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreator : MonoBehaviour
{
    [SerializeField] GameObject pipe;
    GameObject[] pipes;
    [SerializeField] private int pipesNumber;
    [SerializeField] private float pipeSpeed;

    int pipeCount = 0;
    bool isMainGame = false;
    void Start()
    {
        pipes = new GameObject[pipesNumber];
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = Instantiate(pipe, Vector2.zero, Quaternion.identity);
            pipes[i].gameObject.SetActive(false);
        }
        InvokeRepeating("checkPipePos", 0f,2f);
    }

    void Update()
    {
        if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.MainGame)
        {
            isMainGame = true;
        }
        else if(GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.Prepare)
        {
            isMainGame = false;
            for (int i = 0; i < pipes.Length; i++)
            {
                pipes[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                pipes[i].gameObject.SetActive(false);
            }
        }
        else
        {
            isMainGame = false;
           
            for (int i = 0; i < pipes.Length; i++)
            {
                pipes[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
    float timeCount = 0f;
    void checkPipePos()
    {
       if(isMainGame)
        {
            float randomY = Random.Range(-0.6f, 1f);
            pipes[pipeCount].transform.position = new Vector2(5, randomY);
            pipes[pipeCount].gameObject.SetActive(true);
            pipes[pipeCount].GetComponent<Rigidbody2D>().velocity = new Vector2(pipeSpeed, 0);
            pipeCount++;
            if (pipeCount>= pipes.Length)
            {
                pipeCount = 0;
            }
        }
    }
}
