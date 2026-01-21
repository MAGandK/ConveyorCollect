using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Test : MonoBehaviour
{
    // [SerializeField] private Transform _transform;
    // private Coroutine _rotate;
    //
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         _rotate = StartCoroutine(CorTest(transform));
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         if (_rotate != null)
    //         {
    //             StopCoroutine(_rotate);
    //         }
    //     }
    // }
    //
    // private IEnumerator CorTest(Transform transform)
    // {
    //     while (true)
    //     {
    //         transform.Rotate(Vector3.up * Time.deltaTime * 100, Space.World);
    //         yield return null;
    //     }
    // }
    [SerializeField] private List<Transform> _cubs;

    private void Awake()
    {

            StartCoroutine(AnimCubesCor());
    }

    private IEnumerator AnimCubesCor()
    {
        int startCubsCount = _cubs.Count;
        List<YieldInstruction> cubeAnim = new List<YieldInstruction>();

        for (int i = 0; i < startCubsCount; i++)
        {
            Transform cube = _cubs[Random.Range(0, _cubs.Count)];
            _cubs.Remove(cube);

            Coroutine animation = StartCoroutine(AnimateCor(cube));
            cubeAnim.Add(animation);

            yield return new WaitForSeconds(Random.Range(0, 0.75f));
        }

        foreach (var instruction in cubeAnim)
        {
            yield return instruction;
        }

        Debug.Log("Все схлопнулись");
    }

    private IEnumerator AnimateCor(Transform cube)
    {
        float progress = 0;
        float timeAnimation = 1.25f;

        Vector3 startScale = cube.localScale;

        while (progress < timeAnimation)
        {
            progress += Time.deltaTime;
            cube.localScale = Vector3.Lerp(startScale, Vector3.zero, progress / timeAnimation);
            yield return null;
        }
    }
}
