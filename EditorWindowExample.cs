using UnityEditor;
using UnityEngine;

namespace ListView {
	public class EditorWindowExample : EditorWindow {
		
		private static EditorWindowExample window;
		static ListView<SimpleItem> listView;
		
		[MenuItem("Window/EditorWindowExample")]
		public static void OpenWindow(){
			if (window != null) window.Close();
			GetWindow<EditorWindowExample>("EditorWindowExample").Show();
		}

		void OnEnable(){
			window = this; // set singleton.
			var source = new SimpleListViewDataSource();
			listView = new ListView<SimpleItem>(source);
			listView.Refresh();
		}

		private void OnGUI(){
			
			if (GUILayout.Button("Refresh")){
				Debug.Log($"Refresh matter? {listView != null}");
				listView?.Refresh();
			}

			var controlRect = EditorGUILayout.GetControlRect(
				GUILayout.ExpandHeight(true), 
				GUILayout.ExpandWidth(true));
			
			listView?.OnGUI(controlRect);
		}
	}
}
