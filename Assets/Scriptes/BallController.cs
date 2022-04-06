using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [Serializable]
    public class Location
    {
        public Transform Transform;
        public ShowCanvasItemController CanvasUI;
        public string SceneName;
        public Sprite CoverImg;
    }
    public class BallController : MonoBehaviour
    {
        private float rotationX;
        private float rotationY;
        private float scaleRotationX = 5f;
        private float scaleRotationY = 5f;

        private float positionX;
        private float positionY;
        private float positionZ;
        private float scalePosiontX = 0.1f;
        private float scalePosiontY = 0.1f;
        private float scalePosiontZ = 1f;
        public Camera MainCamera;
        public RectTransform canvasRectTransform;
        public List<Location> Locations = new List<Location>();
        public Transform ChrisGO;
        public GameObject ChrisUI;
        public Transform FaceCamPostion;
        public Image CoverImage;

        public int CurrentIndex = -1;

        private void Start()
        {
            for (var i = 0; i < Locations.Count; i++)
            {
                Locations[i].Transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                Locations[i].CanvasUI.OnClickHandler += OnClickHandler;
                Locations[i].CanvasUI.SetIndex(i);
            }

            
        }

        private void OnDestroy()
        {
            foreach (var location in Locations)
            {
                location.CanvasUI.OnClickHandler -= OnClickHandler;
            }
        }

        private void OnClickHandler(int index)
        {
            if (isRotateFacing)
            {
                return;
            }
            CurrentIndex = index;
            StartCoroutine(RotateFace(Locations[index], true));
        }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                positionX = Input.GetAxis("Mouse X") * scalePosiontX;
                positionY = Input.GetAxis("Mouse Y") * scalePosiontY;
                positionZ = -Input.GetAxis("Mouse ScrollWheel") * scalePosiontZ;
                transform.position += new Vector3(positionX, positionY, positionZ);
            }
            else if (Input.GetMouseButton(0))
            {
                if (isRotateFacing)
                {
                    return;
                }
                rotationX = -Input.GetAxis("Mouse X") * scaleRotationX;
                rotationY = Input.GetAxis("Mouse Y") * scaleRotationY;
                transform.Rotate(rotationY, rotationX, 0f, Space.World);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rotationY = scaleRotationY * 0.1f;
                transform.Rotate(rotationY, 0f, 0f, Space.World);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rotationY = scaleRotationY * -0.1f;
                transform.Rotate(rotationY, 0f, 0f, Space.World);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotationX = scaleRotationX * 0.1f;
                transform.Rotate(0f, rotationX, 0f, Space.World);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotationX = scaleRotationX * -0.1f;
                transform.Rotate(0f, rotationX, 0f, Space.World);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (isRotateFacing)
                {
                    return;
                }
                CurrentIndex--;
                if (CurrentIndex < 0)
                {
                    CurrentIndex = Locations.Count - 1;
                }
                StartCoroutine(RotateFace(Locations[CurrentIndex]));
                
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (isRotateFacing)
                {
                    return;
                }
                CurrentIndex++;
                if (CurrentIndex > Locations.Count -1)
                {
                    CurrentIndex = 0;
                }
                StartCoroutine(RotateFace(Locations[CurrentIndex]));
                
            }

            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     
            //
            //     // float angle = GetAngle(FaceCamPostion.position, Locations[0].Transform.position);
            //     // transform.rotation = Quaternion.Euler(targetDir);
            //     
            //     StartCoroutine(nameof(RotateFace));
            // }

            
        }

        private void FixedUpdate()
        {
            ModifyCanvas();
        }

        public bool isRotateFacing = false;
        
        IEnumerator RotateFace(Location location, bool jump = false)
        {
            isRotateFacing = true;
            while (true)
            {
                Vector2 targetDir = FaceCamPostion.position - location.Transform.position;
                Debug.Log(targetDir);
                if ((targetDir - Vector2.zero).sqrMagnitude < 0.01f)
                {
                    break;
                }
                transform.Rotate(targetDir.y, -targetDir.x, 0f, Space.World);
                yield return new WaitForEndOfFrame();
                
            }
            
            if (jump)
            {
                Debug.Log(location.SceneName);
                SceneChangeHelper.PreChangeSceneAsync(location.SceneName).Coroutine();
                MainCamera.transform.DOMoveZ(-4.22f, 1).onComplete = () =>
                {
                    CoverImage.sprite = location.CoverImg;
                    CoverImage.rectTransform.localScale = Vector3.one;
                    CoverImage.rectTransform.DOScale(new Vector3(2, 2, 0), 1f).onComplete = () =>
                    {
                        SceneChangeHelper.ChangeSceneAsync().Coroutine();
                        isRotateFacing = false;
                    };
                };
                
            }
            else
            {
                isRotateFacing = false;
            }
        }


        private void ModifyCanvas()
        {
            foreach (var location in Locations)
            {
                CheckCanvas(location.Transform, location.CanvasUI.gameObject);    
            }
            // CheckCanvas(Locations[3].Transform, Locations[3].CanvasUI.gameObject);
            
        }

        
        private void CheckCanvas(Transform target, GameObject showCanvas)
        {
            Physics.SyncTransforms();
            RaycastHit hitInfo;
            Physics.Raycast(MainCamera.transform.position, (target.position - MainCamera.transform.position).normalized,
                out hitInfo);

            if (hitInfo.collider == null)
            {
                return;
            }
            // Debug.DrawLine(MainCamera.transform.position, (target.position - MainCamera.transform.position).normalized, Color.red);
            if (hitInfo.collider.gameObject == gameObject)
            {
                // Debug.Log($"{hitInfo.collider}");
                
                showCanvas.transform.localScale = Vector3.zero;
            }
            else
            {
                showCanvas.transform.localScale = Vector3.one;
                showCanvas.transform.localPosition = WorldToUgui(target.position);
                
            }
            // Debug.Log($"CheckCanvas:{hitInfo.collider.gameObject}");
        }

        public Vector2 WorldToUgui(Vector3 position)
        {
            Vector2 screenPoint = MainCamera.WorldToScreenPoint(position); //世界坐标转换为屏幕坐标
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            screenPoint -= screenSize / 2; //将屏幕坐标变换为以屏幕中心为原点
            Vector2 anchorPos = screenPoint / screenSize * canvasRectTransform.sizeDelta; //缩放得到UGUI坐标
            return anchorPos;
        }

        private bool IsInView(Transform tran)
        {
            Transform camTransform = MainCamera.transform;
            Vector2 viewPos = MainCamera.WorldToViewportPoint(tran.position);
            Vector3 dir = (tran.position - camTransform.position).normalized;
            float dot = Vector3.Dot(camTransform.forward, dir); //判断物体是否在相机前面

            if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
                return true;
            else
            {
                //Log.e("x_{0}, y_{1}",viewPos.x,viewPos.y);
                return false;
            }
        }
    }
}