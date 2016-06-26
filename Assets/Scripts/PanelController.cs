using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class PanelController : MonoBehaviour {

	private static bool muteTheSound = false;
	private static float volValue = 0.8f;

	public bool atStart;
	public bool pause;
	private bool musicOn = true;

	public bool panelIsOpen = false;
	public Animator panelAnim;

	public GameObject startPanel;
	public GameObject settingsPanel;
	public GameObject gamePanel;
	public GameObject menuPanel;
	public GameObject gameOverPanel;

	public AudioSource audioPlayer;
	public Slider volSlider;
	public  Image soundOn;
	public  Image soundOff;

	private string currentscene;
	
	public float speed;

	private GameManager gameManager;


	// Use this for initialization
	void Start () {
		atStart = true;
		pause = false;
		SetSoundSetting();

		if (SceneManager.GetActiveScene().name == "MainMenu")
		{

		}

		else
		{
			gameOverPanel.SetActive(false);
			gameManager = GameObject.FindGameObjectWithTag("player").GetComponent<GameManager>();
		}

	   // gameManager = GameObject.FindGameObjectWithTag("player").GetComponent<GameManager>();

		currentscene = SceneManager.GetActiveScene().name;

		if (SceneManager.GetActiveScene().name == "Level01")
		{
			startPanel.SetActive(false);
			settingsPanel.SetActive(false);
			gamePanel.SetActive(true);
		}

		//gamePanel.SetActive(false);
		//menuPanel.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{

		}

		else
			if(gameManager.gameOver)
				if(gameOverPanel.gameObject.activeInHierarchy == false)
				{
					gameOverPanel.SetActive(true);
				}

		if (Input.GetKeyDown("i"))
			soundOn.enabled = false;

		if (Input.GetKeyDown("k"))
			soundOn.enabled = true;

		
		//SceneManager.GetActiveScene().name ------------> Get the scene name
	}

	public void StartButton()
	{
	   
		atStart = false;
		volValue = volSlider.value;


		SceneManager.LoadScene(1);
	}

	public void OpenMenu()
	{
		//pause = true;
		if (panelIsOpen == false)
		{
			panelAnim.Play("MenuP_Open");
			panelIsOpen = true;
		}
	}

	public void CloseMenu()
	{

		Time.timeScale = 1;
		if (panelIsOpen)
		{
			panelAnim.Play("MenuP_Close");
			panelIsOpen = false;
		}
	}

	public void OpenSettings()
	{
		settingsPanel.SetActive(true);
	}

	public void CloseSettings()
	{
		settingsPanel.SetActive(false);
	}

	public void QuitApp()
	{
		Application.Quit();
	}

	public void SoundButton()
	{
		
		if (musicOn)
		{
			muteTheSound = true;
			soundOn.enabled = false;
			soundOff.enabled = true;
			audioPlayer.mute = true;
			volSlider.interactable = false;
			musicOn = false;
			print("music off");
		}

		else
		{
			soundOn.enabled = true;
			soundOff.enabled = false;
			audioPlayer.mute = false;
			volSlider.interactable = true;
			musicOn = true;
			print("music off");
		}
	}

	private void SetSoundSetting()
	{
		volSlider.value = volValue;

		if(muteTheSound)
		{
			soundOn.enabled = false;
			soundOff.enabled = true;
			audioPlayer.mute = true;
			volSlider.interactable = false;
			musicOn = false;
		}
	}

	public void BackToMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(currentscene);
	}
}
