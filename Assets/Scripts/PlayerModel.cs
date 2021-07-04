using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public Sprite[] birdSprites;

    public bool wingControl = true;
    public int birdCount = 0;
    public float BirdWingAnimTime = 0f;
    public Vector3 playerStartPos;
    public bool isUpdatePlayerStartPos = true;
    public Vector3 birdUpRotation = new Vector3(0,0,45), birdDownRotation = new Vector3(0,0, -45), birdNormalRotation = new Vector3(0,0, 0);
    public Vector2 birdUpForce = new Vector2(0, 180);

    public bool isPlayerPrepareAnim = true;

}
