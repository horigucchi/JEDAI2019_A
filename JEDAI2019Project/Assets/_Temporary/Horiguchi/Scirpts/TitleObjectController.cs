using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Horiguchi.Engine {

    public class TitleObjectController : Singleton<TitleObjectController>
    {
        [SerializeField, Range(0, 1)]
        private float flyRatio = 0.2f;
        [SerializeField]
        private Transform kite;
        [SerializeField]
        private Transform titleKite;
        [SerializeField]
        private Transform titleObject;
        [SerializeField]
        private SpriteRenderer titleSpriteRenderer;
        [SerializeField]
        private Transform kiteAnchor;


        [SerializeField]
        private Vector3 targetPos;

        [SerializeField]
        private GameObject[] hideObjects;

        [SerializeField]
        private GameObject[] showObjects;

        [SerializeField]
        private Image img;

        private bool isLoop = false;

        void Start()
        {
            titleStart();
            //StartCoroutine(titleFlow());
            ObjectsActiveAll(false);
            TitleObjectsActiveAll(true);
            
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) StartTitleFlow();
        }

        public void StartTitleFlow()
        {
            //StartCoroutine(titleFlow());
            titleStart();
        }

        public void ObjectsActiveAll(bool isActive)
        {
            foreach (var item in hideObjects)
            {
                item.SetActive(isActive);
            }
        }

        public void TitleObjectsActiveAll(bool isActive)
        {
            foreach (var item in showObjects)
            {
                item.SetActive(isActive);
            }

        }

        private void titleStart()
        {
            StartCoroutine(titleFlow());
            StartCoroutine(rollToStart());
        }

        private IEnumerator rollToStart()
        {
            float t = 0;
            Color c = Color.white;
            var imgPos = img.transform.position;
            while (true)
            {
                t += Time.deltaTime;
                var pp = Mathf.PingPong(t, 1);
                //c.a = pp;
                //img.color = c;

                //var scale = Vector3.one * pp;
                //scale.z = 1;
                //img.transform.localScale = scale;

                img.transform.position = Vector3.Lerp(imgPos, imgPos + Vector3.right * 80, pp);

                if (false) break;
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator titleFlow()
        {
            GameManager.Instance.PauseStage();
            titleObject.gameObject.SetActive(true);
            var startKitePos = kite.position;
            kite.position = targetPos;
            kite.gameObject.SetActive(false);
            var startTitlePos = titleObject.position;
            //YarnController.Instance.KiteTransform = titleKite;
            YarnController.Instance.KiteTransform = kiteAnchor;


            // まわしてスタート
            while (true)
            {
                if (YarnController.Instance.RollValue > 30) break;
                yield return new WaitForFixedUpdate();
            }

            titleKite.parent = titleObject;
            Color c = Color.white;

            // 左上にTitleLogo上昇
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * flyRatio;
                c.a = 1 - t * 2f;
                titleSpriteRenderer.color = c;
                titleObject.position = Vector3.Lerp(startTitlePos, targetPos, t);
                if (titleObject.position.x < -10) break;
                if (titleObject.position.x > 9) break;
                //yield return new WaitForFixedUpdate();
                var oldRollValue = YarnController.Instance.RollValue;
                yield return new WaitUntil(() => YarnController.Instance.RollValue > oldRollValue);
            }

            TitleObjectsActiveAll(false);
            titleObject.gameObject.SetActive(false);
            kite.gameObject.SetActive(true);
            YarnController.Instance.KiteTransform = kite;
            // kiteを初期値に戻す
            t = 0;
            while (t < 0.5f)
            {
                t += Time.deltaTime * flyRatio;
                kite.position = Vector3.Lerp(targetPos, startKitePos, t);
                yield return new WaitForFixedUpdate();
            }

            ObjectsActiveAll(true);
            titleObject.position = targetPos;
            titleObject.gameObject.SetActive(false);
            //GameManager.Instance.ResumeStage();
            GameManager.Instance.StartStage(1);


        }

    }
}
