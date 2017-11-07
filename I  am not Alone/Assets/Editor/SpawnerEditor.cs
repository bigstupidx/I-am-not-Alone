using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(WaveManager))]
[CanEditMultipleObjects]
public class SpawnerEditor : Editor
{
    bool showPosition = true;
    string status = "Select a GameObject";
    WaveManager waveManager;
    // Use this for initialization
    private void OnEnable ()
    {
        waveManager = (WaveManager)target;
    }

    public void OnInspectorUpdate ()
    {
        this.Repaint();
    }

    public override void OnInspectorGUI ()
    {

    
        GUILayout.Box("Wave count =" + waveManager.wave.Count.ToString());
        for (int i = 0; i < waveManager.wave.Count; i++)
        {
            showPosition = EditorGUILayout.Foldout(showPosition, status);
            if (showPosition)
                if (Selection.activeTransform)
                {
                    status = "Wave ";
                    GUILayout.Box("Wave  =" + i);
                    GUILayout.BeginHorizontal();

                    GUILayout.Label("Day , Night");
                    waveManager.wave[i].Day = EditorGUILayout.FloatField(waveManager.wave[i].Day);
                    waveManager.wave[i].Night = EditorGUILayout.FloatField(waveManager.wave[i].Night);
                    if (GUILayout.Button("x"))
                    {
                        waveManager.wave[i].countZombie.Clear();
                        waveManager.wave[i].ZombiePref.Clear();
                        waveManager.wave.RemoveAt(i);

                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(5);

                    if (GUILayout.Button("add zombiew Wave"))
                    {


                        waveManager.wave[i].countZombie.Add(0);
                        waveManager.wave[i].ZombiePref.Add(null);

                    }


                    for (int l = 0; l < waveManager.wave[i].countZombie.Count; l++)
                    {


                        GUILayout.BeginHorizontal();
                        waveManager.wave[i].countZombie[l] = EditorGUILayout.IntField(waveManager.wave[i].countZombie[l]);
                        waveManager.wave[i].ZombiePref[l] = (GameObject)EditorGUILayout.ObjectField(waveManager.wave[i].ZombiePref[l], typeof(GameObject), true);
                        if (GUILayout.Button("x"))
                        {
                            waveManager.wave[i].countZombie.RemoveAt(l);
                            waveManager.wave[i].ZombiePref.RemoveAt(l);


                        }
                        GUILayout.EndHorizontal();
                    }

                }
            if (!Selection.activeTransform)
            {
                status = "Select a GameObject";
                showPosition = false;
            }




        }
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("add Wave"))
        {

            waveManager.wave.Add(new Wave());

        }


        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}
