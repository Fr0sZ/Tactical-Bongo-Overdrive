using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

	const int NodeWidth = 140;
	const int NodeHeight = 60;

	const int FindPosX = 96;
	const int FindPosY = 5;

	public Node[,] m_walkNodes = new Node[NodeWidth,NodeHeight];
	public Vector2 m_nodeOffset;

	private List<Node> m_groundedNodes;

	private List<GameObject> m_players;

	private Player			m_playerScript;
	private PlayerMovement 	m_playerMovementScript;

	// Use this for initialization
	void Start () 
	{
		m_playerScript = GetComponent<Player>();
		m_playerMovementScript = GetComponent<PlayerMovement>();

		LayerMask layermask = 1 << 8 | 1 << 9;
		layermask = ~layermask;

		//Find walkable nodes
		for(int i = 0; i < NodeWidth; i++)
		{
			for(int j = 0; j < NodeHeight; j++)
			{
				m_walkNodes[i,j] = new Node();
				m_walkNodes[i,j].Position =  new Vector2(i,j);

				Vector2 pos = m_nodeOffset + new Vector2(i,j);
				if(!Physics2D.OverlapArea(m_nodeOffset + new Vector2(i,j)/2, m_nodeOffset + new Vector2(i+1,j+1)/2, layermask))
					m_walkNodes[i,j].Walkable = true;
			}
		}

		List<Node> groundedNodes = new List<Node>();

		//Find grounded nodes
		for(int i = 0; i < NodeWidth; i++)
		{
			for(int j = 0; j < NodeHeight; j++)
			{
				if(j-1 > 0 && m_walkNodes[i,j-1].Walkable == false && m_walkNodes[i,j-2].Walkable == false && m_walkNodes[i,j].Walkable == true)
				{
					m_walkNodes[i,j].Grounded = true;
					groundedNodes.Add(m_walkNodes[i,j]);
				}
			}
		}

		//Find which nodes can jump to who
		foreach(Node node in groundedNodes)
		{
			foreach(Node jumpNode in groundedNodes)
			{
				if(node.Position == jumpNode.Position)
					continue;

				if(node.Position.y > jumpNode.Position.y)
					continue;

				int xToCheck = (int)Mathf.Abs(node.Position.x - jumpNode.Position.x);
				int yToCheck = (int)Mathf.Abs(node.Position.y - jumpNode.Position.y);

				if(xToCheck > 16 || yToCheck > 10 || xToCheck < 3)
					continue;
			
				bool canJumpHere = true;

				for(int i = 0; i <= yToCheck; i++)
				{
					if(node.Position.y + i >= NodeHeight || node.Position.y + i < 0)
					{
						canJumpHere = false;
						break;
					}

					if(!m_walkNodes[(int)node.Position.x, (int)node.Position.y + i].Walkable)
					{
						canJumpHere = false;
						break;
					}
				}

				if(!canJumpHere)
					continue;

				int x = (int)Mathf.Sign(jumpNode.Position.x - node.Position.x);
				for(int i = 0; i < xToCheck; i++)
				{
					if(node.Position.x + i * x >= NodeWidth || node.Position.y + yToCheck >= NodeHeight)
					{
						canJumpHere = false;
						break;
					}

					Node checkNode = m_walkNodes[(int)node.Position.x + i * x , (int)node.Position.y + yToCheck];
					if(!checkNode.Walkable || checkNode.Grounded)
					{
						canJumpHere = false;
						break;
					}
				}

				if(!canJumpHere)
					continue;

				node.JumpToNodes.Add(jumpNode);
				jumpNode.JumpToNodes.Add(node);
			}
		}
		m_groundedNodes = groundedNodes;

		FindPath(ClosetGround(((Vector2)transform.position - m_nodeOffset)*2) , new Vector2(FindPosX,FindPosY));
	}
	
	Vector2 ClosetGround(Vector2 pos)
	{
		int startX = (int)pos.x;
		int startY = (int)pos.y;

		Vector2 highestPoint = Vector2.zero;

		while(startY >= NodeHeight)
			startY--;

		while(startY >= 0)
		{
			if(startX < 0 || startX >= NodeWidth)
				break;

			if(m_walkNodes[startX, startY].Grounded)
			{
				highestPoint = new Vector2(startX, startY);
				break;
			}

			startY--;
		}

		startX = (int)pos.x + 1;
		startY = (int)pos.y;
		
		while(startY >= NodeHeight)
			startY--;
		
		while(startY >= 0)
		{
			if(startX < 0 || startX >= NodeWidth)
			break;

			if(m_walkNodes[startX, startY].Grounded && startY > highestPoint.y)
			{
				highestPoint = new Vector2(startX, startY);
				break;
			}
			
			startY--;
		}

		startX = (int)pos.x - 1;
		startY = (int)pos.y;
		
		while(startY >= NodeHeight)
			startY--;
		
		while(startY >= 0)
		{
			if(startX < 0 || startX >= NodeWidth)
				break;

			if(m_walkNodes[startX, startY].Grounded && startY > highestPoint.y)
			{
				highestPoint = new Vector2(startX, startY);
				break;
			}
			
			startY--;
		}

		return highestPoint;
	}

	List<Node> m_openList 	= new List<Node>();
	List<Node> m_closedList = new List<Node>();

	void FindPath(Vector2 startPos, Vector2 endPos)
	{
		int endX = (int)endPos.x;
		int endY = (int)endPos.y;

		int startX = (int)startPos.x;
		int startY = (int)startPos.y;

		if(startX < 0 || startX >= NodeWidth || startY < 0 || startY >= NodeHeight)
			return;

		if(!m_walkNodes[endX,endY].Walkable)
		{
			Debug.LogError("not walkable");
			return;
		}

		m_openList.Clear();
		m_closedList.Clear();

		for(int i = 0; i < NodeWidth; i++)
		{
			for(int j = 0; j < NodeHeight; j++)
			{
				Node node = m_walkNodes[i,j];
				node.currentList = 0;
				node.gCost = 0;
				node.parent = null;
			}
		}

		Debug.Log(startX + " " + startY);
		foreach(Node node in m_groundedNodes)
		{
			node.hCost = (int)Vector2.Distance(node.Position, endPos);
		}

		Node currentNode = m_walkNodes[startX, startY];
		//Node currentNode = m_walkNodes[Mathf.RoundToInt(transform.position.x + m_nodeOffset.x), Mathf.RoundToInt(transform.position.y + m_nodeOffset.y)];

	
		m_openList.Add(currentNode);

		while(m_openList.Count > 0)
		{
			currentNode = FindLowestFCostNode();

			for(int i = -1; i < 2; i++)
			{
				if(i == 0)
					continue;

				int nodeX = (int)currentNode.Position.x + i;
				int nodeY = (int)currentNode.Position.y;

				if(nodeX < 0 || nodeX >= NodeWidth)
					continue;

				Node checkNode = m_walkNodes[nodeX, nodeY];

				if(!checkNode.Grounded)
					continue;

				if(checkNode.currentList == 0)
				{
					m_openList.Add(checkNode);
					checkNode.currentList = 1;
					checkNode.parent = currentNode;
					checkNode.gCost = currentNode.gCost + 10;
				}
				else if(checkNode.currentList == 1)
				{
					if(currentNode.gCost + 10 < checkNode.gCost)
					{
						checkNode.parent = currentNode;
						checkNode.gCost = currentNode.gCost + 10;
					}
				}
			}

			foreach(Node jumpNode in currentNode.JumpToNodes)
			{
				int dist =  Mathf.RoundToInt(Vector2.Distance(currentNode.Position, jumpNode.Position) * 11);

				if(jumpNode.currentList == 0)
				{
					m_openList.Add(jumpNode);
					jumpNode.currentList = 1;
					jumpNode.parent = currentNode;
					jumpNode.gCost = currentNode.gCost +  dist;
				}
				else if(jumpNode.currentList == 1)
				{
					if(currentNode.gCost + dist < jumpNode.gCost)
					{
						jumpNode.parent = currentNode;
						jumpNode.gCost = currentNode.gCost + dist;
					}
				}
			}


			m_closedList.Add(currentNode);
			currentNode.currentList = 2;
			m_openList.Remove(currentNode);

			if(currentNode.Position == endPos)
			{
				Debug.Log("REAL BREAK");
				break;
			}
		}
		List<Vector2> path = GetPath(startX,startY, endX, endY);
		if(path != null)
		{
			currentState = 0;
			m_path =  path;
		}
		else
			Debug.Log("is null");
	}
	public List<Vector2> m_path = new List<Vector2>();

	Node FindLowestFCostNode()
	{
		Node currentNode = null;
		int currentLowestFCost = 999999;
		foreach(Node node in m_openList)
		{
			int fCost = node.gCost + node.hCost;
			if(fCost < currentLowestFCost)
			{
				currentNode = node;
				currentLowestFCost = fCost;
			}
		}
		return currentNode;
	}

	List<Vector2> GetPath(int startPosX, int startPosY, int endPosX, int endPosY)
	{
		Node currentNode = m_walkNodes[endPosX, endPosY];
		
		List<Vector2> path = new List<Vector2>();
		Debug.Log(startPosX + " " + startPosY + " " + endPosX + " " + endPosY);
		while(currentNode.Position.x != startPosX || currentNode.Position.y != startPosY)
		{
			Debug.Log(currentNode.Position.ToString());
			path.Add(new Vector2(currentNode.Position.x, currentNode.Position.y)/2 + m_nodeOffset);
			
			if(currentNode.parent == null)
			{
				Debug.Log("null parent");
				break;
			}	
			currentNode = currentNode.parent;
		}
		//path.Add(new Vector2(currentNode.x, currentNode. y) + new Vector2(m_position.x,m_position.z));
		
		path.Reverse();
		
		if(currentNode.Position.x == startPosX && currentNode.Position.y == startPosY)
		{
			
		}
		else
		{
			Debug.LogWarning("Can't find path. " + startPosX + " " + startPosY);
			return null;
		}
		
		return path;
	}

	private int currentState;

	private float checkTimer = 1;
	public Transform target;
	// Update is called once per frame
	void Update () 
	{
		checkTimer -= Time.deltaTime;
		if(checkTimer <= 0)
		{
			//FindPath(ClosetGround(((Vector2)transform.position - m_nodeOffset)*2) , new Vector2(FindPosX,FindPosY));
			FindPath(ClosetGround(((Vector2)transform.position - m_nodeOffset)*2) , ClosetGround(((Vector2)target.position - m_nodeOffset)*2));
			checkTimer = 0.5f;
		}

		if(currentState < m_path.Count && Mathf.Abs(m_path[currentState].x - transform.position.x) < 0.75f && Mathf.Abs(m_path[currentState].y - transform.position.y) < 1 && m_playerMovementScript.m_grounded)
			currentState++;

		if(m_path != null && currentState < m_path.Count)
		{
			float heightDiff = Mathf.Abs(m_path[currentState].y - transform.position.y);
			float widthDiff = Mathf.Abs(m_path[currentState].x - transform.position.x);

			if(widthDiff > 0.2f)
			{
				if(m_path[currentState].x > transform.position.x)
					m_playerMovementScript.SetPlayerDir(new Vector2(1,0));
				else
					m_playerMovementScript.SetPlayerDir(new Vector2(-1,0));
			}
			else
				m_playerMovementScript.SetPlayerDir(new Vector2(0,0));

			if(m_path[currentState].y > transform.position.y)
			{
				m_playerMovementScript.TryToJump();
			
				if(heightDiff > widthDiff)
				{
					m_playerMovementScript.SetPlayerDir(new Vector2(0,0));
				}
			}
		}
		else
			m_playerMovementScript.SetPlayerDir(new Vector2(0,0));
	}
		/*
		m_players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
		
		m_players.Remove(gameObject);
		
		float closestDist = 999999;
		GameObject closestPlayer = null;
		foreach(GameObject player in m_players)
		{
			float dist = Vector2.Distance(player.transform.position, transform.position);
			if(dist < closestDist)
			{
				closestDist = dist;
				closestPlayer = player;
			}
		}
		
		if(closestPlayer.transform.position.x - 2 > transform.position.x)
			m_playerMovementScript.SetPlayerDir(new Vector2(1,0));
		else if(closestPlayer.transform.position.x + 2 < transform.position.x)
			m_playerMovementScript.SetPlayerDir(new Vector2(-1,0));
		else
			m_playerMovementScript.SetPlayerDir(new Vector2(0,0));
		
		Vector2 collSize = GetComponent<BoxCollider2D>().size;
		
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y)  + Mathf.Sign(rigidbody2D.velocity.x) * new Vector2(collSize.x/2 + 0.01f, 0) , new Vector2(Mathf.Sign(rigidbody2D.velocity.x),0), 200);
		if(hit != null)
		{
			if(hit.collider.tag == "Player")
				m_playerScript.Fire();
		}
	}*/
	
	public Vector3 m_debugCube;
	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawSphere(m_debugCube/2 + new Vector3(m_nodeOffset.x, m_nodeOffset.y, 0), 0.25f);
		Gizmos.DrawSphere(m_nodeOffset, 0.5f);
		
		if(!Application.isPlaying)
			return;	
		
		for(int i = 0; i < NodeWidth; i++)
		{
			for(int j = 0; j < NodeHeight; j++)
			{
				if(m_walkNodes[i,j].Walkable && !m_walkNodes[i,j].Grounded)
					continue; //Gizmos.color = Color.white;
				else
					Gizmos.color = Color.red;
				
				if(m_walkNodes[i,j].Grounded)
					Gizmos.color = Color.blue;
				Gizmos.DrawSphere(new Vector2(i,j)/2 + m_nodeOffset, 0.25f);
			}
		}
		
		foreach(Node node in m_groundedNodes)
		{	
			Gizmos.color = Color.black;
			if(node.parent != null)
					Gizmos.DrawLine(node.Position/2 + m_nodeOffset, node.parent.Position/2 + m_nodeOffset);
			/*foreach(Node jumpNode in node.JumpToNodes)
			{
				Gizmos.DrawLine(node.Position/2 + m_nodeOffset, jumpNode.Position/2 + m_nodeOffset);
			}*/
		}
		
		foreach(Vector2 pos in m_path)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(new Vector3(pos.x, pos.y, -6), 0.25f);
		}
	}

}
