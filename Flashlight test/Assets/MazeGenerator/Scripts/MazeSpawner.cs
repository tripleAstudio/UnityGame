using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour 
{
	public bool FullRandom = false;
	public int RandomSeed = 12356123;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public GameObject CubeGenerate = null;

	private BasicMazeGenerator mMazeGenerator = null;

	void Start () 
	{
		if (!FullRandom) 
		{
			Random.seed = RandomSeed;
		}
		mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
		mMazeGenerator.GenerateMaze ();
		

		//инициализация стен
		for (int row = 0; row < Rows; row++) 
		{
			for(int column = 0; column < Columns; column++)
			{
				float x = column * CellWidth + CubeGenerate.transform.localPosition.x;
				float y = CubeGenerate.transform.localPosition.y;
				float z = row * CellHeight + CubeGenerate.transform.localPosition.z;
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor,new Vector3(x,y,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;
				if(cell.WallRight)
				{
						tmp = Instantiate(Wall,new Vector3(x+CellWidth/2,y,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
						tmp.transform.parent = transform;
				}
				if(cell.WallFront)
				{
					if(row != 0 && column != 0)
					{
						tmp = Instantiate(Wall,new Vector3(x,y,z+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
						tmp.transform.parent = transform;
					}
				}
				if(cell.WallLeft)
				{
					tmp = Instantiate(Wall,new Vector3(x-CellWidth/2,y,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
					tmp.transform.parent = transform;
				}
				if(cell.WallBack)
				{
					tmp = Instantiate(Wall,new Vector3(x,y,z-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
					tmp.transform.parent = transform;
				}
			}
		}

		//бокавая грань
		if(Pillar != null)
		{
			for (int row = 0; row < Rows+1; row++) 
			{
				for (int column = 0; column < Columns+1; column++)
				 {
					// добавление расстония между обьектами
					float x = column * CellWidth + CubeGenerate.transform.localPosition.x;
					float y = CubeGenerate.transform.localPosition.y;
					float z = row * CellHeight + CubeGenerate.transform.localPosition.z;
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,y,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}
	}
}
