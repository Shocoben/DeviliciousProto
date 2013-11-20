using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public enum GameStates
	{
		gameOver
		,playing
	}
	
	public static GameStates cState = GameStates.playing;
}
