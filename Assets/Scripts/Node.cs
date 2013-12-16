using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
	public bool Walkable;
	public bool Grounded;
	public Vector2 Position;

	public int hCost;
	public int gCost;

	public int currentList;
	public Node parent;
	public List<Node> JumpToNodes = new List<Node>();
}
