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
        private Transform circle;
        [SerializeField, Tooltip("")]
        private Transform rangeSprite;
        [SerializeField, Tooltip("")]
        private Transform coreSprite;
        [SerializeField, Tooltip("")]
        private LineRenderer lr;
        [SerializeField, Tooltip("")]
        private Transform yarn;
        [SerializeField, Tooltip("")]
        private Transform target;
        [SerializeField, Tooltip("ゲーム描画しているカメラ(座標変換用)")]
        private Camera camera;

        // fpsの倍率
        private float deltaTimeRatio;

        // 操作円の最大値、最長値
        private float maxRadius, minRadius;
        // world原点からの操作円のずれ
        private Vector3 offset;
        private Vector3 tapOffset;
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

        /// <summary>
        /// 角度
        /// </summary>
        public float Angle { get; private set; }

        /// <summary>
        /// 凧のトランスフォーム
        /// </summary>
        public Transform KiteTransform
        {
            get { return target; }
            set { target = value; }
        }

        #endregion


        void Start()
        {
            if (camera == null) camera = Camera.main;
            lr.positionCount = 2;
            tapSprite.position = yarn.position;
            set(yarn.position);
        }

        private void set(Vector3 point)
        {
            const float dev = 1f / (2 * 2);
            maxRadius = (circle.localScale.x + circle.localScale.y) * dev;
            minRadius = (coreSprite.localScale.x + coreSprite.localScale.y) * dev;
            RollPosition = point;
            circle.position = point;
            coreSprite.position = point;
            offset = point;
            tapOffset = circle.position;
            lr.SetPosition(0, yarn.position);
        }

        void Update()
        {
            deltaTimeRatio = Time.deltaTime * 60;
            //IsRolling = roll();
            IsRolling = roll2();
            RollValue += RollingSpeed;
            tapSprite.transform.position = RollPosition;
            //Debug.Log(YarnState);
            //Debug.Log(RollingSpeed);
            lr.SetPosition(lr.positionCount - 1, KiteTransform.position);
        }

        #region
        private bool roll()
        {
            // 持っているなら
            if (Input.GetMouseButton(0))
            {
                // 画面座標からゲーム座標を取ってくる
                tapPos = getTapWorldPoint();

                // 巻く原点からの位置
                Vector3 rangePos = tapPos - offset;
                float length = rangePos.magnitude;
                // 内側範囲外なら
                if (length > minRadius)
                {
                    {// 外側範囲外なら
                        if (length > maxRadius) length = maxRadius;
                        rangePos.Normalize();
                        rangePos *= length;
                    }

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
            else // 持っていないなら
            {
                YarnState = EYarnState.letGo;
                oldRangePos = Vector3.zero;
                RollPosition = offset;
                RollingSpeed = Mathf.Lerp(RollingSpeed, 0, loosenRatio * deltaTimeRatio);
                return false;
            }
            //return false;
        }
        #endregion

        private bool roll2()
        {
            #region
            //bool flag = Input.GetMouseButton(0);
            //if (flag)
            //{
            //    // 画面座標からゲーム座標を取ってくる
            //    tapPos = getTapWorldPoint();
            //    // 巻く原点からの位置
            //    Vector3 rangePos = tapPos - tapOffset;
            //    flag = (rangePos.sqrMagnitude > minRadius * minRadius);
            //}
            //if (flag)
            //{
            //    // 巻く原点からの位置
            //    Vector3 rangePos = tapPos - tapOffset;
            //    const float pi2 = 2 * Mathf.PI;
            //    var radAngle = Mathf.Atan2(rangePos.y, rangePos.x) + pi2;
            //    radAngle %= pi2;
            //    var degAngle = radAngle * Mathf.Rad2Deg;
            //    var dir = Quaternion.Euler(0, 0, degAngle) * Vector3.right;
            //    RollPosition = tapOffset + dir * maxRadius;
            //    if (oldAngle != 0)
            //    {
            //        RollingSpeed = oldAngle - degAngle;
            //        if (RollingSpeed > 350) RollingSpeed -= 360;
            //        if (RollingSpeed < -350) RollingSpeed += 360;
            //    }
            //    oldAngle = degAngle;
            //}
            //else
            //{
            //    oldAngle = 0;
            //    oldRangePos = Vector3.zero;
            //    RollPosition = tapOffset;
            //    RollingSpeed = Mathf.Lerp(RollingSpeed, 0, loosenRatio * deltaTimeRatio);
            //    return false;
            //}
            #endregion
            // 画面座標からゲーム座標を取ってくる
            tapPos = getTapWorldPoint();
            if (Input.GetMouseButtonDown(0)) set(tapPos);
            if (Input.GetMouseButton(0))
            {
                // 巻く原点からの位置
                Vector3 rangePos = tapPos - tapOffset;
                if (rangePos.sqrMagnitude < minRadius * minRadius) { YarnState = EYarnState.hold; }
                else
                {
                    const float pi2 = 2 * Mathf.PI;
                    var radAngle = Mathf.Atan2(rangePos.y, rangePos.x) + pi2;
                    radAngle %= pi2;
                    var degAngle = radAngle * Mathf.Rad2Deg;
                    //Debug.Log(degAngle);
                    //bool a = (oldAngle < 360 && 360 < degAngle) || (degAngle < 360 && 360 < oldAngle);
                    //if (a)
                    if (abs(oldAngle - degAngle) > 330)
                    {
                        if (oldAngle > degAngle) degAngle += 360;
                        else if (oldAngle < degAngle) oldAngle += 360;
                    }
                    degAngle = Mathf.Lerp(oldAngle, degAngle, pullRatio);
                    var dir = Quaternion.Euler(0, 0, degAngle) * Vector3.right;
                    RollPosition = tapOffset + dir * maxRadius;
                    //if (oldAngle != 0)
                    {
                        RollingSpeed = oldAngle - degAngle;
                        if (RollingSpeed > 350) RollingSpeed -= 360;
                        if (RollingSpeed < -350) RollingSpeed += 360;

                        if (RollingSpeed < 0) YarnState = EYarnState.forwardRolling;
                        else if (RollingSpeed > 0) YarnState = EYarnState.reverseRolling;
                        else YarnState = EYarnState.hold;
                    }
                    oldAngle = degAngle % 360;
                }
            }
            else
            {
                YarnState = EYarnState.letGo;
            }
            if (YarnState == EYarnState.hold || YarnState == EYarnState.letGo)
            {
                //oldAngle = 0;
                //RollPosition = tapOffset;
                oldRangePos = Vector3.zero;
                RollingSpeed = Mathf.Max(Mathf.Lerp(RollingSpeed, 0, loosenRatio * deltaTimeRatio), minRollSpeed);
                return false;
            }
            return true;
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
