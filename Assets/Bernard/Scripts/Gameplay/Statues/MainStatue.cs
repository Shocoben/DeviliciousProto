using UnityEngine;
using System.Collections;

public class MainStatue : Statue {
	
	public override void onActive ()
	{
		base.onActive();
		//
		InGameNotifications.showNotification("GameOver");
		PlayerManager.cState= PlayerManager.GameStates.gameOver;
		Time.timeScale = 0;
	}
	
}
