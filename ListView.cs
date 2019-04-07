
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Assertions;

namespace ListView {

    public interface IListViewDataSource<T> where T : TreeViewItem {
        MultiColumnHeader Header { get; }
        List<TreeViewItem> GetData();
        List<TreeViewItem> GetSortedData(int columnIndex, bool isAscending);
        void Gui(Rect rect, int columnIndex, T data);
    }

    public class ListView<T> : TreeView where T : TreeViewItem {

        const string sortedColumnIndexStateKey = "ListView_sortedColumnIndex";
        public IListViewDataSource<T> dataSource;

        //private TreeViewItem root;


        public ListView(IListViewDataSource<T> dataSource) : this(new TreeViewState(), dataSource.Header){
            this.dataSource = dataSource;
        }

        protected ListView(TreeViewState state, MultiColumnHeader header) : base(state, header){
            rowHeight = 20;
            showAlternatingRowBackgrounds = true;
            showBorder = true;
            header.sortingChanged += SortingChanged;

            header.ResizeToFit();
            Reload();

            header.sortedColumnIndex = SessionState.GetInt(sortedColumnIndexStateKey, 1);
            Debug.Log(".ctor2");
        }

        protected override TreeViewItem BuildRoot(){
            var root = new TreeViewItem{depth = -1};
            root.children = new List<TreeViewItem>();
            return root;
        }

        public void Refresh(){
            if (dataSource == null){
                //Debug.LogError("Datasource required to refresh");
                return;
            }
            rootItem.children = dataSource.GetData();
            BuildRows(rootItem);
            Repaint();
        }

        private void SortingChanged(MultiColumnHeader header){
            SessionState.SetInt(sortedColumnIndexStateKey, multiColumnHeader.sortedColumnIndex);
            
            if (dataSource == null){
                rootItem.children = new List<TreeViewItem>();
                BuildRows(rootItem);
                return;
            }
           
            var index = multiColumnHeader.sortedColumnIndex;
            var ascending = multiColumnHeader.IsSortedAscending(multiColumnHeader.sortedColumnIndex);

            rootItem.children = dataSource.GetSortedData(index, ascending);
            BuildRows(rootItem);
        }
        
        protected override void RowGUI(RowGUIArgs args){
            var item = args.item as T;

            for (var visibleColumnIndex = 0;
                visibleColumnIndex < args.GetNumVisibleColumns();
                visibleColumnIndex++){
                var rect = args.GetCellRect(visibleColumnIndex);
                var columnIndex = args.GetColumn(visibleColumnIndex);

                var labelStyle = args.selected ? EditorStyles.whiteLabel : EditorStyles.label;
                labelStyle.alignment = TextAnchor.MiddleLeft;
                
                dataSource.Gui(rect, columnIndex, item);
            }
        }
    }
}
