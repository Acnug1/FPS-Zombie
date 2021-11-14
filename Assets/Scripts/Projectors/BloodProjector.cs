using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projector))]

public class BloodProjector : MonoBehaviour
{
    [SerializeField] private float _destroyTime;
    [SerializeField] [Range(0, 0.1f)] private float _stepFadeProjector;

    private void Start()
    {
        GetComponent<Projector>().material = new Material(GetComponent<Projector>().material); // необходимо создать отдельный экземпляр материала для каждого проектора, для изменения альфы каждого проектора по отдельности, а не у общего материала в ассете

        transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), transform.forward) * transform.rotation; // кватернион поворота объекта, умноженный на случайный угол от 0 до 360, повернутый вдоль оси Z

        Invoke(nameof(DestroyProjector), _destroyTime);
    }

    private void DestroyProjector()
    {
        StartCoroutine(FadeProjector());
    }

    private IEnumerator FadeProjector()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_stepFadeProjector);

        for (float _alpha = 1; _alpha > 0; _alpha -= 0.01f)
        {
            GetComponent<Projector>().material.color = new Color(0, 0, 0, _alpha);
            yield return waitForSeconds;
        }

        Destroy(gameObject);
    }
}
