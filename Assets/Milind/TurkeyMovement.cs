using UnityEngine;
using System.Collections;

public class TurkeyMovement : MonoBehaviour
{
    public TileMap map;
    int tileX, tileY;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) && (int)transform.position.y > 0)
        {
            Vector3 start = transform.position;
            Vector3 end = new Vector3(transform.position.x, (float)((int)(transform.position.y) - 1), transform.position.z);
            transform.position = end;
            map.GeneratePathTo((int)transform.position.x, (int)transform.position.y);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && (int)transform.position.y<map.mapSizeY-1)
        {
            transform.position = new Vector3(transform.position.x, (int)transform.position.y + 1, transform.position.z);
            map.GeneratePathTo((int)transform.position.x, (int)transform.position.y);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && (int)transform.position.x>0 )
        {
            transform.position = new Vector3((int)transform.position.x-1, transform.position.y, transform.position.z);
            map.GeneratePathTo((int)transform.position.x, (int)transform.position.y);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && (int)transform.position.x < map.mapSizeX-1)
        {
            transform.position = new Vector3((int)transform.position.x+1, transform.position.y, transform.position.z);
            map.GeneratePathTo((int)transform.position.x, (int)transform.position.y);
        }
    }
}
