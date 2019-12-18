using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Horiguchi.Engine;

namespace Horiguchi
{
    public class YarnController : Singleton<YarnController>
    {
        [SerializeField, Tooltip("")]
        private float maxRollSpeed;
        [SerializeField, Tooltip("")]
        private float minRollSpeed;
        [SerializeField, Tooltip(""), Range(0, 1)]
        private float pullRatio = 1;
        [SerializeField, Tooltip(""), Range(0, 1)]
        private float loosenRatio = 0.5f;

        [SerializeField, Tooltip("")]
        private Transform tapSprite;

        [SerializeField, Tooltip("")]
        private Transform rangeSprite;
        [SerializeField, Tooltip("")]
        private Transform coreSprite;

        // ゲーム描画しているカメラ(座標変換用)
        private Camera camera;
        // fpsの倍率
        private float deltaTimeRatio;

        // 操作円の最大値、最長値
        private float maxRadius, minRadius;
        // world原点からの操作円のずれ
        private Vector3 offset;
        // 操作の原点(操作円の中心点)
        private Vector3 tapPos;
        // 前フレームの円上の点
        private Vector3 oldRangePos;
        private float oldAngle;

        //--------------------
        #region// public Property
        /// <summary>
        /// 操作している座標
        /// </summary>
        public Vector3 RollPosition { get; private set; }

        /// <summary>
        /// 巻いている速度
        /// </summary>
        public float RollingSpeed { get; private set; }

        /// <summary>
        /// 巻いているか
        /// </summary>
        public bool IsRolling { get; private set; }

        /// <summary>
        /// 回転した角度
        /// </summary>
        public float RollValue { get; private set; }

        /// <summary>
        /// 糸の状態
        /// </summary>
        public EYarnState YarnState { get; private set; }

        #endregion


        void Start()
        {
            camera = Camera.main;
            const float dev = 1f / (2 * 2);
            maxRadius = (rangeSprite.localScale.x + rangeSprite.localScale.y) * dev;
            minRadius = (coreSprite.localScale.x + coreSprite.localScale.y) * dev;
            offset = coreSprite.position;
        }

        void Update()
        {
            deltaTimeRatio = Time.deltaTime * 60;
            IsRolling = roll();
            RollValue += RollingSpeed;
            tapSprite.transform.position = RollPosition;
            //Debug.Log(YarnState);
        }

        private bool roll()
        {
            if (Input.GetMouseButton(0))
            {
                // 画面座標からゲーム座標を取ってくる
                tapPos = getTapWorldPoint();

                // 巻く原点からの位置
                Vector3 rangePos = tapPos - offset;
                float length = rangePos.magnitude;
                if (length > minRadius)
                {
                    if (length > maxRadius) length = maxRadius;
                    rangePos.Normalize();
                    rangePos *= length;

                    var angle = Mathf.Atan2(rangePos.y, rangePos.x) * Mathf.Rad2Deg + 180;
                    RollingSpeed = angle - oldAngle;
                    if (RollingSpeed > 350) RollingSpeed -= 360;
                    if (RollingSpeed < -350) RollingSpeed += 360;
                    oldRangePos = rangePos;
                    oldAngle = angle;
                    RollPosition = offset + rangePos;
                }
                float speed = abs(RollingSpeed);// RollingSpeed > 0 ? RollingSpeed : -RollingSpeed;
                bool flag = minRollSpeed < speed && speed < maxRollSpeed;
                if (flag)
                {
                    if (RollingSpeed < 0) YarnState = EYarnState.forwardRolling;
                    else YarnState = EYarnState.reverseRolling;
                }
                else
                {
                    YarnState = EYarnState.hold;
                }
                return flag;
            }
            else
            {
                YarnState = EYarnState.letGo;
                oldRangePos = Vector3.zero;
                RollingSpeed = Mathf.Lerp(RollingSpeed, 0, loosenRatio * deltaTimeRatio);
                return false;
            }
            //return false;
        }

        /// <summary>
        /// 座標取得
        /// </summary>
        /// <returns></returns>
        private Vector3 getTapWorldPoint()
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z -= camera.transform.position.z;
            return camera.ScreenToWorldPoint(screenPos);
        }

        /// <summary>
        /// 絶対値取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private float abs(float val)
        {
            // Mathf.Abs(val);
            return val > 0 ? val : -val;
        }
    }
}
