using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kan
{
    public class BGscroller : MonoBehaviour
    {

        private Vector3 startPosition;

        [HideInInspector]
        public float scrollSpeed;
        private float tileSizeX;

        // Start is called before the first frame update
        void Start()
        {
            tileSizeX = transform.localScale.x * 2;
            startPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
            transform.position = startPosition + Vector3.left * newPosition;
        }
    }

}
