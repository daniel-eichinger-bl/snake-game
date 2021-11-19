using UnityEngine;
using System.Collections.Generic;
using System;

public class Snke : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;

    public int initalSize;

    private void Start() {
        ResetState();
    }

    private void Update() {

        // Set direction via WASD Keys
        if(Input.GetKeyDown(KeyCode.W)) {
            _direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S)){
            _direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            _direction = Vector2.left;
        } else if(Input.GetKeyDown(KeyCode.D)) {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate() {

        for (int i = _segments.Count -1 ; i > 0; i--)
        {
            _segments[i].position = _segments[i-1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y)+ _direction.y,
            0.0f
        );
    }

    private void Grow() {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count-1].position;
        _segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Food") {
            ScoreManager.instance.AddPoint();
            Grow();
        } else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

    private void ResetState()
    {   
        ScoreManager.instance.Reset();

        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initalSize; i++)
        {
         _segments.Add(Instantiate(segmentPrefab));   
        }

        this.transform.position = Vector3.zero;
    }
}
