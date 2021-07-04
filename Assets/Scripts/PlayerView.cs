using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rigidBody;

    public UnityAction<int> OnScoreUpdate;
    public void UpdatePlayerStartPos(PlayerModel playerModel, PlayerView playerView)
    {
       playerModel.playerStartPos = playerView.transform.position;
    }
    public void BirdWingAnim(PlayerModel playerModel)
    {
        if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.MainGame || GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.Prepare)
        {
            playerModel.BirdWingAnimTime += Time.deltaTime;
            if (playerModel.BirdWingAnimTime >=0.1f)
            {
                if (playerModel.wingControl)
                {
                    spriteRenderer.sprite = playerModel.birdSprites[playerModel.birdCount];
                    playerModel.birdCount++; // 0, 1, 2, 3
                    if (playerModel.birdCount == playerModel.birdSprites.Length)
                    {
                        playerModel.birdCount--;
                        playerModel.wingControl = false;
                    }
                }
                else
                {
                    playerModel.birdCount--;
                    spriteRenderer.sprite = playerModel.birdSprites[playerModel.birdCount];
                    if (playerModel.birdCount == 0)
                    {
                        playerModel.birdCount++;
                        playerModel.wingControl = true;
                    }
                }
                playerModel.BirdWingAnimTime = 0;
            }
        }
    }
    public void PlayerMovement(Vector3 birdNormalRot,Vector3 birdDownRot, Vector3 birdUpRot, Vector2 birdUpForce)
    {
        if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.Prepare)
        {
            rigidBody.gravityScale = 0f;
            rigidBody.velocity = Vector2.zero;
            transform.eulerAngles = birdNormalRot;

        }
        else if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.MainGame)
        {
            rigidBody.gravityScale = 1f;
            if (Input.GetMouseButtonDown(0))
            {
                SoundManager.instance.PlaySound(SoundManager.instance.birdWingSound, 1f);
                rigidBody.velocity = Vector2.zero;
                rigidBody.AddForce(birdUpForce); 
            }
            if (rigidBody.velocity.y > 0)
            {
                transform.eulerAngles = birdUpRot;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(birdDownRot), .1f);
            }
        }
        else if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.FinishGame)
        {
            rigidBody.gravityScale = 1f;
            transform.eulerAngles = birdDownRot;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "score" && GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.MainGame)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.coinSound, 1f);
            OnScoreUpdate?.Invoke(1);
        }
        else if(collision.gameObject.tag == "pipe")
        {
            if (GameManagerScript.instance.CurrentGameState != GameManagerScript.GameState.FinishGame)
            {
                SoundManager.instance.PlaySound(SoundManager.instance.deathSound, 1f);
            }
            GameManagerScript.instance.CurrentGameState = GameManagerScript.GameState.FinishGame;
        }
        else if (collision.gameObject.tag == "ground")
        {
            if (GameManagerScript.instance.CurrentGameState != GameManagerScript.GameState.FinishGame)
            {
                SoundManager.instance.PlaySound(SoundManager.instance.deathSound, 1f);
            }
            GameManagerScript.instance.CurrentGameState = GameManagerScript.GameState.FinishGame;
        }
    }
    public void playerPrepareAnim(PlayerModel playerModel)
    {
        if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.Prepare)
        {
            if (playerModel.isPlayerPrepareAnim)
            {
                birYPosAnim(playerModel.playerStartPos.y);
                playerModel.isPlayerPrepareAnim = false;
            }
        }
        else
        {
            DOTween.Kill(this.transform);
            playerModel.isPlayerPrepareAnim = true;
        }
    }
    private void birYPosAnim(float posY)
    {
        if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.Prepare)
        {
            transform.DOMoveY(posY + 0.5f, 1).OnComplete(() => transform.DOMoveY(posY, 1).OnStepComplete(() => birYPosAnim(posY)));
        }
    }
}
