using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDirector : System.Object {
	private static SDirector _instance;
	public SceneHandle currentSceneController {get; set;}

	public static SDirector getInstance() {
		if (_instance == null) {
			_instance = new SDirector();
		}
		return _instance;
	}
}