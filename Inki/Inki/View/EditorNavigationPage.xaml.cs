﻿using Inki.Models;
using Inki.Vendor;
using Inki.View.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Inki.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EditorNavigationPage : Page
    {

        public EditorLayoutModel editorLayout;

        /******** Test Code Start ********/
        public EditorInkCanvasPage primaryEditorPage;
        public EditorInkCanvasPage secondaryEditorPage;
        /********  Test Code End  ********/

        public EditorNavigationPage()
        {
            this.InitializeComponent();
            this.editorLayout = LayoutCacheReader.readEditorLayout();
            this.slider.Value = this.editorLayout.splitRatio * 100;
            this.loadLayout();

            primaryEditor.Navigate(typeof(EditorInkCanvasPage));
            secondaryEditor.Navigate(typeof(EditorInkCanvasPage));
            this.primaryEditorPage = (EditorInkCanvasPage)primaryEditor.Content;
            this.secondaryEditorPage = (EditorInkCanvasPage)secondaryEditor.Content;
            this.inkToolBar.TargetInkCanvas = this.primaryEditorPage.inkCanvas;
        }

        #region 分屏封装处理

        /// <summary>
        /// 加载当前布局
        /// </summary>
        private void loadLayout()
        {
            switch(this.editorLayout.splitStyle)
            {
                case EditorLayoutModel.EditorSplitStyle.None:
                    this.closeSplitLayout();
                    break;
                case EditorLayoutModel.EditorSplitStyle.Horizontal:
                    this.setLeftRightSplitLayout(this.editorLayout.splitRatio);
                    break;
                case EditorLayoutModel.EditorSplitStyle.Vertical:
                    this.setUpDownSplitLayout(this.editorLayout.splitRatio);
                    break;
            }
        }

        /// <summary>
        /// 加载并保存当前布局
        /// </summary>
        private void updateLayout()
        {
            this.loadLayout();
            LayoutCacheWriter.writeEditorLayout(this.editorLayout);
        }

        /// <summary>
        /// 设置左右分屏布局
        /// </summary>
        private void setLeftRightSplitLayout(double splitRatio)
        {
            this.primaryEditor.SetValue(Grid.ColumnProperty, 0);
            this.secondaryEditor.SetValue(Grid.ColumnProperty, 1);
            this.primaryEditor.SetValue(Grid.RowProperty, 0);
            this.secondaryEditor.SetValue(Grid.RowProperty, 0);
            this.primaryGridColumn.Width = new GridLength(splitRatio, GridUnitType.Star);
            this.secondaryGridColumn.Width = new GridLength(1.0 - splitRatio, GridUnitType.Star);
            this.primaryGridRow.Height = new GridLength(1, GridUnitType.Star);
            this.secondaryGridRow.Height = new GridLength(0, GridUnitType.Star);
        }

        /// <summary>
        /// 设置上下分屏布局
        /// </summary>
        private void setUpDownSplitLayout(double splitRatio)
        {
            this.primaryEditor.SetValue(Grid.ColumnProperty, 0);
            this.secondaryEditor.SetValue(Grid.ColumnProperty, 0);
            this.primaryEditor.SetValue(Grid.RowProperty, 0);
            this.secondaryEditor.SetValue(Grid.RowProperty, 1);
            this.primaryGridColumn.Width = new GridLength(1, GridUnitType.Star);
            this.secondaryGridColumn.Width = new GridLength(0, GridUnitType.Star);
            this.primaryGridRow.Height = new GridLength(splitRatio, GridUnitType.Star);
            this.secondaryGridRow.Height = new GridLength(1.0 - splitRatio, GridUnitType.Star);
        }

        /// <summary>
        /// 退出分屏布局
        /// </summary>
        private void closeSplitLayout()
        {
            this.primaryEditor.SetValue(Grid.ColumnProperty, 0);
            this.secondaryEditor.SetValue(Grid.ColumnProperty, 1);
            this.primaryEditor.SetValue(Grid.RowProperty, 0);
            this.secondaryEditor.SetValue(Grid.RowProperty, 1);
            this.primaryGridColumn.Width = new GridLength(1, GridUnitType.Star);
            this.secondaryGridColumn.Width = new GridLength(0, GridUnitType.Star);
            this.primaryGridRow.Height = new GridLength(1, GridUnitType.Star);
            this.secondaryGridRow.Height = new GridLength(0, GridUnitType.Star);
        }

        #endregion

        #region 分屏UI处理

        private void HorizontalSplitButtonClick(object sender, RoutedEventArgs e)
        {
            this.editorLayout.splitStyle = EditorLayoutModel.EditorSplitStyle.Horizontal;
            this.updateLayout();
        }

        private void VerticalSplitButtonClick(object sender, RoutedEventArgs e)
        {
            this.editorLayout.splitStyle = EditorLayoutModel.EditorSplitStyle.Vertical;
            this.updateLayout();
        }

        private void NoSplitButtonClick(object sender, RoutedEventArgs e)
        {
            this.editorLayout.splitStyle = EditorLayoutModel.EditorSplitStyle.None;
            this.updateLayout();
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (this.editorLayout == null) return;
            this.editorLayout.splitRatio = e.NewValue / 100.0;
            this.updateLayout();
        }

        #endregion

        private async void primaryEditor_FocusEngaged(Control sender, FocusEngagedEventArgs args)
        {
            /******** Test Code Start ********/
            await new ConsoleDialog("primary focused").ShowAsync();
            /********  Test Code End  ********/
        }

        private async void secondaryEditor_FocusEngaged(Control sender, FocusEngagedEventArgs args)
        {
            /******** Test Code Start ********/
            await new ConsoleDialog("secondary focused").ShowAsync();
            /********  Test Code End  ********/
        }

        private async void primaryEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            /******** Test Code Start ********/
            await new ConsoleDialog("primary got focused").ShowAsync();
            /********  Test Code End  ********/
        }

        private async void secondaryEditor_GotFocus(object sender, RoutedEventArgs e)
        {
            /******** Test Code Start ********/
            await new ConsoleDialog("secondary got focused").ShowAsync();
            /********  Test Code End  ********/
        }

        private async void primaryEditor_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            /******** Test Code Start ********/
            await new ConsoleDialog("primary pointer pressed").ShowAsync();
            /********  Test Code End  ********/
        }

        private async void secondaryEditor_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            /******** Test Code Start ********/
            await new ConsoleDialog("secondary pointer pressed").ShowAsync();
            /********  Test Code End  ********/
        }

        private async void primaryEditor_Tapped(object sender, TappedRoutedEventArgs e)
        {
            /******** Test Code Start ********/
            await new ConsoleDialog("primary tapped").ShowAsync();
            /********  Test Code End  ********/
        }

        private async void secondaryEditor_Tapped(object sender, TappedRoutedEventArgs e)
        {
            /******** Test Code Start ********/
            await new ConsoleDialog("secondary tapped").ShowAsync();
            /********  Test Code End  ********/
        }

        private void primaryEditor_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.inkToolBar.TargetInkCanvas = this.primaryEditorPage.inkCanvas;
        }

        private void secondaryEditor_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.inkToolBar.TargetInkCanvas = this.secondaryEditorPage.inkCanvas;
        }
    }
}