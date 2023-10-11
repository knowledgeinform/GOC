using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftScene : MonoBehaviour
{
    public int shift;
    public void ShifttoScene()
    {
        SceneManager.LoadScene(shift);
    }
}
