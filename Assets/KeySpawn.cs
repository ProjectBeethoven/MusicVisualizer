using UnityEngine;

public class KeySpawn : MonoBehaviour {

    [SerializeField]
    public GameObject key;
    [SerializeField]


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 66; i++)
        {
            //Finds the camera width and height in pixels.
            float cameraWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
            float cameraHeight = 2f * Camera.main.orthographicSize;

            //Gets the screen width, takes into account the key width being 1/66 of the screen. Then the "i" will move it
            //relative to other white keys.
            SpriteRenderer keyRender = (SpriteRenderer)key.GetComponent<SpriteRenderer>();
            float scalerNum =  3696/100000f ; 
            //**************************************************************************************************************************
            //We need to figure out the appropriate scalerNum. the sprite is 100 ppu.
            //**************************************************************************************************************************
            Vector3 scaler = new Vector3(cameraWidth * scalerNum, cameraWidth * scalerNum, 0);
            keyRender.transform.localScale = scaler;
            Vector3 keySize = keyRender.bounds.size;

            //Converts the pixel unit to World Units
            float PixelsPerUnit = keyRender.sprite.pixelsPerUnit;


            keySize.x = keySize.x * PixelsPerUnit;
            keySize.y = keySize.y * PixelsPerUnit;


            float x = (1.5f*i * keySize.x)*(cameraWidth / 66) + .2f*keySize.x;
            float y = (.5f * keySize.y - .3f*keySize.y);
            Instantiate(key, Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10)), Quaternion.identity);

        }
    }
}
