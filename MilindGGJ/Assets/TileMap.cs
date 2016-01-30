using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour {

	public GameObject selectedUnit;

    public GameObject tilePrefab;

	int[,] tiles;
	Node[,] graph;


	public int mapSizeX = 10;
	public int mapSizeY = 10;

	void Start() {
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;
        tiles = new int[mapSizeX, mapSizeY];
        GenerateMapAndShit();
		GenerateShortestPathGraph();
		GenerateGridAndShit();
       
	}

	void GenerateMapAndShit() {
		int x,y;
		
		for(x=0; x < mapSizeX; x++) {
			for(y=0; y < mapSizeX; y++) {
				tiles[x,y] = 0;
			}
		}

	}

	public class Node {
		public List<Node> neighbours;
		public int x;
		public int y;

		public Node() {
			neighbours = new List<Node>();
		}

		public float DistanceTo(Node n) {
			if(n == null) {
				Debug.LogError("WTF?");
			}

			return Vector2.Distance(
					new Vector2(x, y),
					new Vector2(n.x, n.y)
				);
		}
	}


	void GenerateShortestPathGraph() {
		
		graph = new Node[mapSizeX,mapSizeY];

		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
				graph[x,y] = new Node();
				graph[x,y].x = x;
				graph[x,y].y = y;
			}
		}

		
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {

                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }
				if(y > 0)
					graph[x,y].neighbours.Add( graph[x, y-1] );
				if(y < mapSizeY-1)
					graph[x,y].neighbours.Add( graph[x, y+1] );
			}
		}
	}

	void GenerateGridAndShit() {
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
                    GameObject go = (GameObject)Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.tileX = x;
                    ct.tileY = y;
                    ct.map = this;
                
			}
		}
	}

	public Vector3 TileCoordToWorldCoord(int x, int y) {
		return new Vector3(x, y, 0);
	}

	public void GeneratePathTo(int x, int y) {

        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().currentPath = null;

		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

	
		List<Node> unvisited = new List<Node>();
		
		Node source = graph[
		                    selectedUnit.GetComponent<Unit>().tileX, 
		                    selectedUnit.GetComponent<Unit>().tileY
		                    ];
		
		Node target = graph[
		                    x, 
		                    y
		                    ];

        selectedUnit.GetComponent<Unit>().SendTargetCoordinates(target);
		
		dist[source] = 0;
		prev[source] = null;

		
		foreach(Node v in graph) {
			if(v != source) {
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}

			unvisited.Add(v);
		}

		while(unvisited.Count > 0) {
			
			Node u = null;

			foreach(Node possibleU in unvisited) {
				if(u == null || dist[possibleU] < dist[u]) {
					u = possibleU;
				}
			}

			if(u == target) {
				break;	
			}

			unvisited.Remove(u);

			foreach(Node v in u.neighbours) {
				float alt = dist[u] + u.DistanceTo(v);
				if( alt < dist[v] ) {
					dist[v] = alt;
					prev[v] = u;
				}
			}
		}


		if(prev[target] == null) {
			return;
		}

		List<Node> currentPath = new List<Node>();

		Node curr = target;

		
		while(curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}

		

		currentPath.Reverse();

        foreach(Node v in currentPath)
        {
            Debug.Log(v.x + " " + v.y);
        }

		selectedUnit.GetComponent<Unit>().UpdateCurrentPath(currentPath);
	}

}
