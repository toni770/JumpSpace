using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float transitionTime = 1;

    public void ReloadScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        anim.SetTrigger("out");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }
}
