using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(CurveGenerator))]
public class CurveGeneratorEditor : Editor {
	override public void OnInspectorGUI() {
		CurveGenerator cg = (CurveGenerator)target;
		
		if(cg.amplitudes == null) {
			cg.amplitudes = new List<float>();
		}
		if(cg.frequencies == null) {
			cg.frequencies = new List<float>();
		}
		if(cg.phases == null) {
			cg.phases = new List<float>();
		}
		if(cg.weights == null) {
			cg.weights = new List<float>();
		}
		
		int curves = Mathf.Max(1,EditorGUILayout.IntField("Curves: ",cg.mNumberOfCurves));
		if(curves != cg.mNumberOfCurves) {
			while(cg.mNumberOfCurves < curves) {
				cg.amplitudes.Add(1);
				cg.frequencies.Add(1);
				cg.phases.Add(0);
				cg.weights.Add(1);
				cg.mNumberOfCurves++;
			}
			while(cg.mNumberOfCurves > curves) {
				cg.amplitudes.RemoveAt(cg.amplitudes.Count-1);
				cg.frequencies.RemoveAt(cg.frequencies.Count-1);
				cg.phases.RemoveAt(cg.phases.Count-1);
				cg.weights.RemoveAt(cg.weights.Count-1);
				cg.mNumberOfCurves--;
			}
		}
		for(int i = 0; i < cg.mNumberOfCurves; i++) {
			GUILayout.Label("");
			cg.amplitudes[i] = 	Mathf.Max(0,EditorGUILayout.FloatField("amp: ",cg.amplitudes[i]));
			cg.frequencies[i] = Mathf.Max(0,EditorGUILayout.FloatField("frq: ",cg.frequencies[i]));
			cg.phases[i] = 		Mathf.Max(0,EditorGUILayout.FloatField("phs: ",cg.phases[i]));
			cg.weights[i] = 	Mathf.Max(0,EditorGUILayout.FloatField("wgh: ",cg.weights[i]));
		}
	}
}
