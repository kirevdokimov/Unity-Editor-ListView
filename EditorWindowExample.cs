using UnityEditor;
using UnityEngine;

namespace ListView {
	public class EditorWindowExample : EditorWindow {
		
		private static EditorWindowExample window;
		static ListView<PersonItem> listView;

		private SimpleListViewDelegate _delegate;
		
		[MenuItem("Window/EditorWindowExample")]
		public static void OpenWindow(){
			if (window != null) window.Close();
			GetWindow<EditorWindowExample>("EditorWindowExample").Show();
		}

		void OnEnable(){
			window = this; // set singleton.
			_delegate = new SimpleListViewDelegate();
			listView = new ListView<PersonItem>(_delegate);
			listView.Refresh();
		}

		// Flag to refresh list if dataSource changed
		private bool refreshFlag;

		private void OnGUI(){

			ButtonsGUI();

			if (refreshFlag){
				listView?.Refresh();
				refreshFlag = false;
			}

			var controlRect = EditorGUILayout.GetControlRect(
				GUILayout.ExpandHeight(true), 
				GUILayout.ExpandWidth(true));
			
			listView?.OnGUI(controlRect);
		}

		private void ButtonsGUI(){
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Add")){
				_delegate.Add();
				refreshFlag = true;
			}
			
			if (GUILayout.Button("Remove")){
				_delegate.Remove();
				refreshFlag = true;
			}
			
			if (GUILayout.Button("Refresh")){
				refreshFlag = true;
			}
			GUILayout.EndHorizontal();
		}
	}
}
