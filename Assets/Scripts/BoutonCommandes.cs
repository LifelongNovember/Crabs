using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonCommandes : MonoBehaviour
{
    public void onPressed()
    {
        SceneManager.LoadScene("Commandes");
    }
}
