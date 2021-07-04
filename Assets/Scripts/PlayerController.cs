using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    public PlayerController(PlayerView view)
    {
        playerView = view;
        playerModel = new PlayerModel();
        LoadSprites();
        view.OnScoreUpdate = scoreUpdate;
        playerView.UpdatePlayerStartPos(playerModel, playerView);
    }
    public void scoreUpdate(int score)
    {
        GameManagerScript.instance.increaseScore(score);
    }
    public void LoadSprites()
    {
        playerModel.birdSprites = Resources.LoadAll<Sprite>(("BirdSprites"));
    }
  
    public void BirdWingAnimation()
    {
        playerView.BirdWingAnim(playerModel);
    }
   
    public void CheckEverytime()
    {
        playerView.playerPrepareAnim(playerModel);
        playerView.PlayerMovement(playerModel.birdNormalRotation, playerModel.birdDownRotation, playerModel.birdUpRotation, playerModel.birdUpForce);
    }
    public void UpdatePlayerStartPos()
    {
        if (GameManagerScript.instance.CurrentGameState == GameManagerScript.GameState.Prepare)
        {
            if (playerModel.isUpdatePlayerStartPos)
            {
                playerView.transform.position = playerModel.playerStartPos;
            }
            playerModel.isUpdatePlayerStartPos = false;
        }
        else
        {
            playerModel.isUpdatePlayerStartPos = true;
        }

    }
}
