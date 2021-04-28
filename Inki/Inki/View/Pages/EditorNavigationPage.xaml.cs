using Inki.Models;
using Inki.Vendor;
using Inki.View.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Inki.View.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EditorNavigationPage : Page
    {

        public EditorLayoutModel editorLayout;

        public EditorSinglePage primaryEditorPage;
        public EditorSinglePage secondaryEditorPage;

        public EditorNavigationPage()
        {
            this.InitializeComponent();
            this.editorLayout = LayoutCacheReader.readEditorLayout();
            this.slider.Value = this.editorLayout.splitRatio * 100;
            this.loadLayout();

            primaryEditor.Navigate(typeof(EditorSinglePage));
            secondaryEditor.Navigate(typeof(EditorSinglePage));

            this.primaryEditorPage = (EditorSinglePage)primaryEditor.Content;
            this.secondaryEditorPage = (EditorSinglePage)secondaryEditor.Content;

            this.primaryEditorPage.pageUnitView.inkCanvas.InkPresenter.UnprocessedInput.PointerEntered += UnprocessedInput_PointerEntered;
            this.secondaryEditorPage.pageUnitView.inkCanvas.InkPresenter.UnprocessedInput.PointerEntered += UnprocessedInput_PointerEntered1;

            // InkToolBar初始化绑定到Primary (Border颜色已在Xaml中设置)
            this.inkToolBar.TargetInkCanvas = this.primaryEditorPage.pageUnitView.inkCanvas;
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
            this.primaryBorder.SetValue(Grid.ColumnProperty, 0);
            this.secondaryBorder.SetValue(Grid.ColumnProperty, 1);
            this.primaryBorder.SetValue(Grid.RowProperty, 0);
            this.secondaryBorder.SetValue(Grid.RowProperty, 0);
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
            this.primaryBorder.SetValue(Grid.ColumnProperty, 0);
            this.secondaryBorder.SetValue(Grid.ColumnProperty, 0);
            this.primaryBorder.SetValue(Grid.RowProperty, 0);
            this.secondaryBorder.SetValue(Grid.RowProperty, 1);
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
            this.primaryBorder.SetValue(Grid.ColumnProperty, 0);
            this.secondaryBorder.SetValue(Grid.ColumnProperty, 1);
            this.primaryBorder.SetValue(Grid.RowProperty, 0);
            this.secondaryBorder.SetValue(Grid.RowProperty, 1);
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

        #region 分屏间切换处理

        private void activatePrimaryCanvas() {
            primaryBorder.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
            secondaryBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
            inkToolBar.TargetInkCanvas = this.primaryEditorPage.pageUnitView.inkCanvas;
        }

        private void activateSecondaryCanvas()
        {
            primaryBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
            secondaryBorder.BorderBrush = new SolidColorBrush(Colors.MediumVioletRed);
            inkToolBar.TargetInkCanvas = this.secondaryEditorPage.pageUnitView.inkCanvas;
        }

        private void UnprocessedInput_PointerEntered(InkUnprocessedInput sender, PointerEventArgs args)
        {
            activatePrimaryCanvas();
        }

        private void UnprocessedInput_PointerEntered1(InkUnprocessedInput sender, PointerEventArgs args)
        {
            activateSecondaryCanvas();
        }

        #endregion


        // TODO: 下面的内容和XAML中的这个模块全部分装起来，然后要专门存储drawingAttributes，方便一开始显示/存储/etc.
        // TODO: 记得要处理切换到这个工具的情况（设置好ToggleButton的值），尝试封装到一起。
        // TODO: 思考了一下，原来的工具就不要这个功能了，把新的customInkButton封装一下就好。
        private void PenPressureSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (inkToolBar.TargetInkCanvas != null)
            {
                InkDrawingAttributes drawingAttributes = inkToolBar.TargetInkCanvas.InkPresenter.CopyDefaultDrawingAttributes();
                drawingAttributes.IgnorePressure = !((ToggleSwitch)sender).IsOn;
                inkToolBar.TargetInkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);
            }
        }

        private void PenSmoothSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (inkToolBar.TargetInkCanvas != null)
            {
                InkDrawingAttributes drawingAttributes = inkToolBar.TargetInkCanvas.InkPresenter.CopyDefaultDrawingAttributes();
                drawingAttributes.FitToCurve = ((ToggleSwitch)sender).IsOn;
                inkToolBar.TargetInkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);
            }
        }

        private void PenHighlightSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (inkToolBar.TargetInkCanvas != null)
            {
                InkDrawingAttributes drawingAttributes = inkToolBar.TargetInkCanvas.InkPresenter.CopyDefaultDrawingAttributes();
                drawingAttributes.DrawAsHighlighter = ((ToggleSwitch)sender).IsOn;
                inkToolBar.TargetInkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);
            }
        }

        private void PenTiltSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (inkToolBar.TargetInkCanvas != null)
            {
                InkDrawingAttributes drawingAttributes = inkToolBar.TargetInkCanvas.InkPresenter.CopyDefaultDrawingAttributes();
                drawingAttributes.IgnoreTilt = !((ToggleSwitch)sender).IsOn;
                inkToolBar.TargetInkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);
            }
        }
    }
}
