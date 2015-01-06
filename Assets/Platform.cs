using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public Sprite[] platformSprites;
	public float platformMinSpacing = 0f;
	public float platformMaxSpacing = 0f;
	
	private Camera cam;
	private float[] spriteWidth;
	// private bool hasARightBuddy = false;
	private Transform lastPlatform;
	private int lastPlatformIndex = 0;
	private float baseHeight;


	void Awake() {

		cam = Camera.main;
		lastPlatform = GameObject.Find("Platform").transform;
	}

	// Use this for initialization
	void Start () {

		spriteWidth = new float[platformSprites.Length];

		baseHeight = lastPlatform.position.y;

		for(int i = 0; i < platformSprites.Length; i++) {

			if(platformSprites[i] != null) {
				spriteWidth[i] = platformSprites[i].bounds.size.x;;
			}
			else {
				Debug.Log("Whoa there! Get that sprite in bro!");
			}
		}

		if(platformMinSpacing == 0 && platformMaxSpacing == 0) {

			platformMinSpacing = 999999f;
			platformMaxSpacing = 0f;

			for(int i = 0; i < spriteWidth.Length; i++) {

				if(spriteWidth[i] > platformMaxSpacing) {
					platformMaxSpacing = spriteWidth[i];
				}
				
				if(spriteWidth[i] < platformMinSpacing) {
					platformMinSpacing = spriteWidth[i];
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		//if (hasARightBuddy == false) {
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

			if((cam.transform.position.x + camHorizontalExtend) > (lastPlatform.position.x + platformMaxSpacing)) {
				MakeNewBuddy();
			}
		//}
	}

	void MakeNewBuddy() {

		Vector3 newPosition = new Vector3 (lastPlatform.position.x + spriteWidth[lastPlatformIndex]/2 + Random.Range(platformMinSpacing, platformMaxSpacing*2), baseHeight + Random.Range(-1, 1), lastPlatform.position.z);
		Transform newBuddy = Instantiate (lastPlatform, newPosition, lastPlatform.rotation) as Transform;

		newBuddy.parent = lastPlatform.parent;

		lastPlatformIndex = Random.Range(0, 3);
		newBuddy.GetComponent<SpriteRenderer>().sprite = platformSprites[lastPlatformIndex];

		lastPlatform = newBuddy;
	}
}
