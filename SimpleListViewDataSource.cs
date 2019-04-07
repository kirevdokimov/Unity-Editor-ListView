using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ListView {
	public class SimpleListViewDataSource : IListViewDataSource<SimpleItem> {
		public MultiColumnHeader Header => new MultiColumnHeader(new MultiColumnHeaderState(new[]{
			new MultiColumnHeaderState.Column{headerContent = new GUIContent("Id"), width = 5},
			new MultiColumnHeaderState.Column{headerContent = new GUIContent("First name"), width = 20},
			new MultiColumnHeaderState.Column{headerContent = new GUIContent("Last name"), width = 20},
			new MultiColumnHeaderState.Column{headerContent = new GUIContent("Age"), width = 10},
		}));

		public List<TreeViewItem> GetData(){
			return raw.Cast<TreeViewItem>().ToList();
		}

		public List<TreeViewItem> GetSortedData(int columnIndex, bool isAscending)
			=> GetSortedData0(columnIndex, isAscending).Cast<TreeViewItem>().ToList();

		private IEnumerable<SimpleItem> GetSortedData0(int columnIndex, bool isAscending){
			switch (columnIndex){
				case 0:
					return isAscending
						? raw.OrderBy(item => item.Id)
						: raw.OrderByDescending(item => item.Id);
				case 1:
					return isAscending
						? raw.OrderBy(item => item.Firstname)
						: raw.OrderByDescending(item => item.Firstname);
				case 2:
					return isAscending
						? raw.OrderBy(item => item.Lastname)
						: raw.OrderByDescending(item => item.Lastname);
				case 3:
					return isAscending
						? raw.OrderBy(item => item.Age)
						: raw.OrderByDescending(item => item.Age);
				default:
					throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, null);
			}
		}

		private readonly List<SimpleItem> raw = new List<SimpleItem>{
			new SimpleItem{Id = 0, Firstname = "Joe", Lastname = "Doe", Age = 25},
			new SimpleItem{Id = 1, Firstname = "Ivan", Lastname = "Ivanovich", Age = 33},
			new SimpleItem{Id = 2, Firstname = "John", Lastname = "Smith", Age = 388}
		};

		public void Gui(Rect rect, int columnIndex, SimpleItem data){
			switch (columnIndex){
				case 0 : 
					EditorGUI.LabelField(rect, data.Id.ToString());
					break;
				case 1 : 
					EditorGUI.LabelField(rect, data.Firstname);
					break;
				case 2 : 
					EditorGUI.LabelField(rect, data.Lastname);
					break;
				case 3 : 
					EditorGUI.LabelField(rect, data.Age.ToString());
					break;
				default: 
					throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, null);
			}
		}
	}

	public class SimpleItem : TreeViewItem {
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public int Age { get; set; }
	}
}