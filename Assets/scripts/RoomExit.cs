using UnityEngine;
using System.Collections;

public class RoomExit : MonoBehaviour {
	
	public delegate void EnteredExitHandler(int side);
	public event EnteredExitHandler onEnteredExit;
	
	private int side;
	public int Side{
		get{
			return side;
		}
		
		set{
			side = value;
		}
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private Room room;
	public Room Room{
		get{ return Room;
		}
		
		set{ room = value;}
	}
	
	private bool fired = false;
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player"))
		{
			if(onEnteredExit != null && !fired)
			{
				fired = true;
	       		onEnteredExit(side);
			}
		}
    }
}

	