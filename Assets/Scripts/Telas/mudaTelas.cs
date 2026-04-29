using UnityEngine;
using UnityEngine.SceneManagement;

public class mudaTelas : MonoBehaviour
{
    public void TrocarCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
