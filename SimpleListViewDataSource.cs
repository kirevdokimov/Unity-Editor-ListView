using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ListView {
	
	public class PersonItem : TreeViewItem {
		public int Pin { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public int Age { get; set; }
		
		public PersonItem(int id) : base(id){ }
	}

	public class SimpleListViewDelegate : IListViewDelegate<PersonItem> {
		public MultiColumnHeader Header => new MultiColumnHeader(new MultiColumnHeaderState(new[]{
			new MultiColumnHeaderState.Column{headerContent = new GUIContent("Pin"), width = 20},
			new MultiColumnHeaderState.Column{headerContent = new GUIContent("First name"), width = 20},
			new MultiColumnHeaderState.Column{headerContent = new GUIContent("Last name"), width = 20},
			new MultiColumnHeaderState.Column{headerContent = new GUIContent("Age"), width = 10},
		}));

		public void Add(){
			raw.Add(PersonGenerator.Generate());
		}
		
		public void Remove(){
			raw.RemoveAt(raw.Count-1);
		}

		public List<TreeViewItem> GetData(){
			return raw.Cast<TreeViewItem>().ToList();
		}

		public List<TreeViewItem> GetSortedData(int columnIndex, bool isAscending)
			=> GetSortedData0(columnIndex, isAscending).Cast<TreeViewItem>().ToList();

		private IEnumerable<PersonItem> GetSortedData0(int columnIndex, bool isAscending){
			switch (columnIndex){
				case 0:
					return isAscending
						? raw.OrderBy(item => item.Pin)
						: raw.OrderByDescending(item => item.Pin);
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

		private readonly List<PersonItem> raw = 
			Enumerable.Repeat(0,7).Select(PersonGenerator.Generate).ToList();
		
		public void Draw(Rect rect, int columnIndex, PersonItem data, bool selected){
			var labelStyle = selected ? EditorStyles.whiteLabel : EditorStyles.label;
			labelStyle.alignment = TextAnchor.MiddleLeft;
			
			switch (columnIndex){
				case 0 : 
					EditorGUI.LabelField(rect, data.Pin.ToString(), labelStyle);
					break;
				case 1 : 
					EditorGUI.LabelField(rect, $"{data.Firstname} selected {selected}", labelStyle);
					break;
				case 2 : 
					EditorGUI.LabelField(rect, data.Lastname, labelStyle);
					break;
				case 3 : 
					EditorGUI.LabelField(rect, data.Age.ToString(), labelStyle);
					break;
				default: 
					throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, null);
			}
		}
	}
}