using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {

	void OnClick()
    {
        Application.LoadLevel("ProtoShow");
		Time.timeScale = 1;
		PlayerManager.cState = PlayerManager.GameStates.playing;
		InGameNotifications.clear();
    }
}

