using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public int NextLevel
	{
		get{ return (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;}
	}
	public void GotoLevel(int index)
	{
		SceneManager.LoadScene(index);
	}
	public void GotoLevel(string name)
	{
		SceneManager.LoadScene(name);
	}
	public void GotoNextLevel()
	{
		int next = NextLevel;
		Debug.Log ("LevelChanger: Loading Next Level { " + next + " }");
		SceneManager.LoadScene(next);
	}
}
