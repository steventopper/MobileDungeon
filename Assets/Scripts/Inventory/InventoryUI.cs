using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class InventoryUI : MonoBehaviour {
	public Button close;

	void Start () {
		// Button btn = close.GetComponent<Button>();
		close.onClick.AddListener(Close);
	}

	void Close(){
		Time.timeScale = 1f;
		EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
		SceneManager.LoadScene(engine.OnResume());
	}
}