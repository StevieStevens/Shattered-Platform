using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RestartLevel : MonoBehaviour {

	public void Restart(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("Main");
	}
}
