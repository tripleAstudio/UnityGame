﻿using UnityEngine;
using System.Collections;

public enum Direction
{
	Start,
	Right,
	Front,
	Left,
	Back,
};


public class MazeCell
{
	public bool WallRight = false;
	public bool WallFront = false;
	public bool WallLeft = false;
	public bool WallBack = false;
	public bool IsGoal = false;
}
