using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class MainMenuCanvas : MonoBehaviour
{
    public Button m_StartButton;
    public Button m_LoadButton;
    public Button m_OptionsButton;

    public VignetteAndChromaticAberration m_VignetteAndChromaticAberration;

	void Start ()
    {
        m_StartButton.onClick.AddListener(StartNewGame);

        m_VignetteAndChromaticAberration = Camera.main.GetComponent<VignetteAndChromaticAberration>();
        m_VignetteAndChromaticAberration.intensity = 1f;

        StartCoroutine("ShutterController");
	}

    IEnumerator ShutterController()
    {
        while (m_VignetteAndChromaticAberration.intensity > 0.1f)
        {
            yield return new WaitForSeconds(0.075f);
            m_VignetteAndChromaticAberration.intensity -= Time.deltaTime;
        }

        yield return null;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
