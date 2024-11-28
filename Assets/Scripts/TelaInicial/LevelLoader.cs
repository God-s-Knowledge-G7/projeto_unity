using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Button IniciarButton;
    public Button SairButton;

    void Start()
    {
        IniciarButton.onClick.AddListener(() => StartCoroutine(IniciarButtonClick("Fase 1")));
        SairButton.onClick.AddListener(() => SairButtonClick());
    }

    void Update()
    {
        
    }
    // Fun��o para trocar de cena
    IEnumerator IniciarButtonClick(string nomeFase)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nomeFase);
    }
    // Fun��o para fechar o jogo (tanto editor como game)
    void SairButtonClick()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
