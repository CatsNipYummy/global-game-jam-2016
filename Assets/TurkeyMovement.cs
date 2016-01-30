using UnityEngine;
using System.Collections;

public class TurkeyMovement : MonoBehaviour
{
    public TileMap map;
	int TurktileX, TurktileY;
	public GameObject terrainMap;
	Vector2 playerPos;
    // Use this for initialization
    void Start()
    {
		//playerPos = terrainMap.GetComponent<Grid> ().PlayerNodeValue ();
		//Debug.Log ("PlayerNodeValue" + terrainMap.GetComponent<Grid> ().PlayerNodeValue ());
		InvokeRepeating ("MoveTo",3,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
	
		//DictionaryBase


	}
	void MoveTo(){
		playerPos = terrainMap.GetComponent<Grid> ().PlayerNodeValue ();
		TurktileX = (int)(playerPos.x);
		TurktileY = (int)(playerPos.y);
		Debug.Log ("Final value" + TurktileX + "  " + TurktileY);
		map.GeneratePathTo(TurktileX,TurktileY);
	}
}