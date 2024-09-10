using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject countdownText;
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    private float musicVolume = 0.5f;
    private float sfxVolume = 0.5f;

    void Start() {

        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

       musicSlider.value = musicVolume;
       sfxSlider.value = sfxVolume;

        SetMusicVolume();
        SetSFXVolume();
    }

    public void PlayGame() {

        StartCoroutine(StartGameWithCountdown());
    }
    IEnumerator StartGameWithCountdown() {
        countdownText.SetActive(true);
        int countdown = 3;
        while (countdown > 0) {
            countdownText.GetComponent<Text>().text = countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
        }
        countdownText.GetComponent<Text>().text = "Go!";
        yield return new WaitForSeconds(1);
        countdownText.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void OpenOptions() {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseOptions() {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void QuitGame() {
        Application.Quit();
        Debug.Log("Game is exitting");
    }

    public void SetMusicVolume() {
        musicAudioSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicAudioSource.volume);
    }
    public void SetSFXVolume() {
        sfxAudioSource.volume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", sfxAudioSource.volume);
    }




}