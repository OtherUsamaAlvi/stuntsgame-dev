using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AdnanScripts
{
    public class EditorTools : Editor
    {

        #region Copy Transform
        static Transform copiedTranform;
        [MenuItem("AdnanTools/Copy Transform %T")]
        private static void CopyTransform()
        {
            copiedTranform = Selection.activeTransform;
            Debug.Log("Transform Copied");
        }

        [MenuItem("AdnanTools/Paste Transform %#T")]
        private static void PasteTransform()
        {
            //Selection.activeTransform.position = copiedTranform.position;
            //Selection.activeTransform.rotation = copiedTranform.rotation;
            Transform[] selectedTransforms = Selection.transforms;
            int count = selectedTransforms.Length;

            for (int i = 0; i < count; i++)
            {
                selectedTransforms[i].localPosition = copiedTranform.localPosition;
                selectedTransforms[i].localRotation = copiedTranform.localRotation;
                //selectedTransforms[i].localScale = copiedTranform.localScale;
            }
            Debug.Log("Transform Pasted");
        }

        [MenuItem("AdnanTools/Paste Transform with Scale %#&T")]
        private static void PasteTransformWithScale()
        {
            //Selection.activeTransform.position = copiedTranform.position;
            //Selection.activeTransform.rotation = copiedTranform.rotation;
            Transform[] selectedTransforms = Selection.transforms;
            int count = selectedTransforms.Length;

            for (int i = 0; i < count; i++)
            {
                selectedTransforms[i].localPosition = copiedTranform.localPosition;
                selectedTransforms[i].localRotation = copiedTranform.localRotation;
                selectedTransforms[i].localScale = copiedTranform.localScale;
            }
            Debug.Log("Transform Pasted");
        }
        #endregion



        #region Snap to Ground
        [MenuItem("AdnanTools/Snap To Underneath Collider #G")]
        private static void SnapGosToUnderneathCollider()
        {
            Transform[] selectedTransforms = Selection.transforms;
            int count = selectedTransforms.Length;

            for (int i = 0; i < count; i++)
            {
                Vector3 pos = selectedTransforms[i].position;
                selectedTransforms[i].gameObject.SetActive(false);
                pos.y += 500;
                Ray ray = new Ray(pos, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    selectedTransforms[i].position = hit.point;
                }
                selectedTransforms[i].gameObject.SetActive(true);
            }
        }

        static LayerMask groundLayer = LayerMask.GetMask("Ground");
        [MenuItem("AdnanTools/Snap To Ground Layer &#G")]
        private static void SnapGosToGroundLayer()
        {
            Transform[] selectedTransforms = Selection.transforms;
            int count = selectedTransforms.Length;

            for (int i = 0; i < count; i++)
            {
                Vector3 pos = selectedTransforms[i].position;
                selectedTransforms[i].gameObject.SetActive(false);
                pos.y += 500;
                Ray ray = new Ray(pos, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, groundLayer))
                {
                    selectedTransforms[i].position = hit.point;
                }
                selectedTransforms[i].gameObject.SetActive(true);
            }
        }
        private static void SnapSingleGoToGround(GameObject go)
        {
            Vector3 pos = go.transform.position;
            go.SetActive(false);
            pos.y += 500;
            Ray ray = new Ray(pos, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                go.transform.position = hit.point;
            }
            go.SetActive(true);
        }
        #endregion



        #region Copy-Paste Components
        static GameObject copiedGo;
        static Component[] copiedComponents, componentsToPasteTo;
        [MenuItem("AdnanTools/Copy Components &#C")]
        private static void CopyComponents()
        {
            copiedGo = Selection.activeGameObject;
            copiedComponents = Selection.activeGameObject.GetComponents<Component>();
            //UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponents);
            if (Debug.isDebugBuild) Debug.Log("Copy Components from: " + Selection.activeGameObject);
        }

        [MenuItem("AdnanTools/Paste Components &#V")]
        private static void PasteComponents()
        {
            //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(Selection.activeGameObject);
            for (int i = 0; i < copiedComponents.Length; i++)
            {
                if (copiedComponents[i].GetType() != typeof(Transform))
                {
                    UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponents[i]);
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(Selection.activeGameObject);
                }
            }
            if (Debug.isDebugBuild) Debug.Log("Paste Components to: " + Selection.activeGameObject);
        }

        [MenuItem("AdnanTools/Paste Component Values %&#V")]
        private static void PasteComponentValues()
        {
            componentsToPasteTo = Selection.activeGameObject.GetComponents<Component>();
            //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(Selection.activeGameObject);
            for (int i = 0; i < copiedComponents.Length; i++)
            {
                //if (copiedComponents[i] == Selection.activeGameObject.GetComponent<Component>())
                //if (copiedComponents[i] == componentsToPasteTo[i])
                if (copiedComponents[i].GetType() != typeof(Transform))
                {
                    UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponents[i]);
                    //UnityEditorInternal.ComponentUtility.PasteComponentValues(Selection.activeGameObject.GetComponent<Component>());
                    UnityEditorInternal.ComponentUtility.PasteComponentValues(componentsToPasteTo[i]);
                }
            }
            if (Debug.isDebugBuild) Debug.Log("Paste Component Values to: " + Selection.activeGameObject);
        }
        #endregion

    }
}
