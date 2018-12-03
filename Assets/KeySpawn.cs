using UnityEngine;

public class KeySpawn : MonoBehaviour
{

    [SerializeField]
    public GameObject whiteKeyGO;
    public GameObject blackKeyGO;
    [SerializeField]

    // Use this for initialization
    void Start()
    {
        //The amount of white keys
        float whiteKeys = 52f;

        for (int i = 0; i < whiteKeys; i++)
        {
            //Finds the camera width and height in pixels.
            float cameraHeight = 2f * Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * Screen.width / Screen.height;

            //Gets the screen width, takes into account the key width being 1/66 of the screen. Then the "i" will move it
            //relative to other white keys.
            SpriteRenderer whiteKeyRender = (SpriteRenderer)whiteKeyGO.GetComponent<SpriteRenderer>();
            float scalerNum = 6 / 125f;
            //**************************************************************************************************************************
            //We need to figure out the appropriate scalerNum. the sprite is 100 ppu.
            //**************************************************************************************************************************
            Vector3 whiteScaler = new Vector3(cameraWidth * scalerNum, (cameraWidth * scalerNum), 0);
            whiteKeyRender.transform.localScale = whiteScaler;
            var whiteKeySize = (cameraWidth / whiteKeys);

            //Converts the pixel unit to World Units
            float PixelsPerUnit = whiteKeyRender.sprite.pixelsPerUnit;

            //The spacing between keys.
            float x = (i * whiteKeySize) - (cameraWidth / 2f) + (.5f * whiteKeySize);
            float y = -(cameraHeight / 2f) + (whiteKeySize * 3.425f); //3.425 is found by the white key pixel ratio divided by 2
            Instantiate(whiteKeyGO, new Vector3(x, y, 10), Quaternion.identity);

            //*************************************************************************
            //Working now with black keys
            if (i % 7 != 1 && i % 7 != 4 && i != 51) //added && i != 51 to prevent extra black key from spawning at the end
            {
                SpriteRenderer blackKeyRender = (SpriteRenderer)blackKeyGO.GetComponent<SpriteRenderer>();
                Vector3 blackScaler = new Vector3(cameraWidth * scalerNum * .714266666f, (cameraWidth * scalerNum) * .714266666f, 0);
                blackKeyRender.transform.localScale = blackScaler;
                float blackXPos = x + whiteKeySize * .5f;
                float blackYPos = y + whiteKeySize * 1.173f;
                Instantiate(blackKeyGO, new Vector3(blackXPos, blackYPos, 9), Quaternion.identity);
            }
        }
    }
}
