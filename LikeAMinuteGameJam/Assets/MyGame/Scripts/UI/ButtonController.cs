using UnityEngine;
using UnityEngine.UI;

public class ButtonControllerUI : MonoBehaviour
{
	[Header("FullScreen")]
	public Toggle toggle;
	public static bool isFullscreen;
	

	[Header("Audio")]
	public Slider slider;


	void Start()
	{
		#region FullScreen
		isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
		Screen.fullScreen = isFullscreen;

		toggle.isOn = Screen.fullScreen;

		toggle.onValueChanged.AddListener(delegate {
			ToggleFullScreen(toggle.isOn);
		});
		#endregion
		#region Audio

		float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
		slider.value = savedVolume;
		AudioListener.volume = savedVolume;
		Debug.Log("Esta mudando o valor inicial: " + savedVolume);

		slider.onValueChanged.AddListener(delegate {
			UpdateAudioVolume(slider.value);
		});

		slider.value = AudioListener.volume;
		#endregion
	}

	void UpdateAudioVolume(float volume)
	{
		
		AudioListener.volume = volume;
		PlayerPrefs.SetFloat("Volume", volume);
		Debug.Log("Esta mudando os valores: " + volume);
	}

	// Função para ativar/desativar o modo tela cheia
	void ToggleFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
		PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
		PlayerPrefs.Save();
	}
}
