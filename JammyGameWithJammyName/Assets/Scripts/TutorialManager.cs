using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private float _exitTime;

    private void Start()
    {
        StartCoroutine(ExitScene());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator ExitScene()
    {
        yield return new WaitForSeconds(_exitTime);
        SceneManager.LoadScene(1);
    }
}
