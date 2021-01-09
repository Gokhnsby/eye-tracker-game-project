using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {		//controlling the spawning of the game objects, score and game over. It responsible for game situation

	public GameObject truck;		//truck game object 
	public GameObject coin;			//coin game object
	public Vector3 spawnValues;		//that controll the position, rotation of the objects
	public int truck_Count;				//the count of the truck object
	public float truck_Wait_Gap_Spawn;		//the gap between appears of the trucks and another
	public float start_Wait;				// the gap to starting the game
	public float stage_Wait;				// gat to create another group of truck objects as a level of game
	public int Coin_Number;				// the coin number that collect in the game for score
	private int score;					// score

	public TextMesh score_txt;		// score text mesh
	public TextMesh restart_txt;		//restart text mesh
	public TextMesh gamemover_txt;		// game over text mesh

	private bool gameover;			// boolean that control the game over
	private bool restart;			//boolean that check the restart

	ChangeSpeed cs;			//if we want to change speed, controll this value
	//Mover a = new Mover();

	void Start ()			//at the begin of the game 
	{
		gameover = false;				//game over and restart bool are false, 
		restart = false;
		restart_txt.text = "";			//text meshs are null
		gamemover_txt.text = "";	
		score = 0;						//and score is zero


		UpdateScore ();						//call update score each coin is collected
		StartCoroutine( SpawnWaves ());		// call methot that conrolling the trucks and their position
		StartCoroutine (SpawnCoin ());		// call methot that conrolling the trucks and their position
//		StartCoroutine (OptionalSpawn());
	}
	void Update(){					// this non functional with eye tracker components, but without it, game is restarted when the 'R' is pressed
		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel("Scene2");
			}
		}
	}

	IEnumerator SpawnWaves ()			// spawner of the truck function
	{
		yield return new WaitForSeconds(start_Wait);		// wait the start gap at the begin
		while(true){								// infinite loop that create the trucks 		
		for(int i = 0; i< truck_Count; i++){		//the stage controller loop, in the stage how many trucks are created

		Vector3 spawnPosition1 = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);		//each time new vector that has random x point, is created for each truck object.
		//Vector3 spawnPosition2 = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);


			Quaternion spawnRotation = Quaternion.identity;			//rotation of the created truck objects, as we identify it
		Instantiate (truck, spawnPosition1, spawnRotation);			// make copies of the truck and put it certain positions and rotation  
		//Instantiate (coin, spawnPosition2, spawnRotation);
		yield return new WaitForSeconds(truck_Wait_Gap_Spawn);			//truck wait gap, each truck is waiting for another one

			}
			yield return new WaitForSeconds(stage_Wait);			//stage wait, group of trucks wait another group of trucks

		}
	}
	IEnumerator SpawnCoin(){		// spawner of the coin function
		while (true) {							// infinite loop that create the coins	
		
			yield return new WaitForSeconds(start_Wait+ 1);		
		for (int i = 0; i< Coin_Number ; i++) {
				Vector3 spawnPosition2 = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z); //each time new vector that has random x point, is created for each coin object.
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (coin, spawnPosition2, spawnRotation);
			yield return new WaitForSeconds(4);
		}
			yield return new WaitForSeconds(stage_Wait);

		}
	}

	/*IEnumerator OptionalSpawn(){				//optional
		yield return new WaitForSeconds(1);
		while (true) {
			yield return new WaitForSeconds(1);

			Vector3 spawnPosition1 = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Vector3 spawnPosition2 = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

			if(spawnPosition1.x == spawnPosition2.x || Mathf.Abs(spawnPosition1.x - spawnPosition2.x) < 1){
				Random.Range(-spawnValues.x, spawnValues.x);
				Random.Range (-spawnValues.x, spawnValues.x);
			}
			Quaternion spawnRotation1 = Quaternion.identity;
			Quaternion spawnRotation2 = Quaternion.identity;
			Instantiate (truck, spawnPosition1, spawnRotation1);
			//yield return new WaitForSeconds(1);
			Instantiate (coin, spawnPosition2, spawnRotation2);
		
		}
		yield return new WaitForSeconds(stage_Wait);
	}*/
	public void AddScore (int scoreValue){		//add score method, update each time score when the coim is collected
		score += scoreValue;
		UpdateScore ();
	
	}

	void UpdateScore(){						//update the score text mesh, and display it on the screen
		score_txt.text = "Score:" + score;

	}
	public void GameOver(){			//update the game over controller boolean and text mesh.

		gamemover_txt.text = "GAME OVER";
		gameover = true;

	}



}
