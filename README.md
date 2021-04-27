# Inki

A powerful Notetaker, notebook PDF Editor of Next Generation on UWP platform.

# References

## Optimization

- https://docs.microsoft.com/zh-cn/windows/uwp/debug-test-perf/optimize-gridview-and-listview
- https://docs.microsoft.com/zh-cn/windows/uwp/debug-test-perf/listview-and-gridview-data-optimization
- https://docs.microsoft.com/zh-cn/windows/uwp/debug-test-perf/optimize-xaml-loading
- https://docs.microsoft.com/zh-cn/windows/uwp/debug-test-perf/optimize-your-xaml-layout
- https://docs.microsoft.com/zh-cn/windows/uwp/debug-test-perf/keep-the-ui-thread-responsive

## Ink

### Ink Tutorials

- https://docs.microsoft.com/zh-cn/windows/uwp/design/controls-and-patterns/inking-controls#inktoolbar-interaction
- https://docs.microsoft.com/zh-cn/windows/uwp/design/input/pen-and-stylus-interactions
- https://docs.microsoft.com/zh-cn/windows/uwp/design/input/ink-walkthrough
- https://docs.microsoft.com/zh-cn/windows/uwp/design/input/convert-ink-to-text
- https://docs.microsoft.com/zh-cn/windows/uwp/design/input/save-and-load-ink
- https://docs.microsoft.com/zh-cn/windows/uwp/design/input/ink-toolbar

### Ink Demos

- https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/SimpleInk
  - Scenario 2 - Selection
  - Scenario 3 - Customized Input tool
  - Scenario 4 - Text Detection
  - Scenario 5 - Less Tools
  - Scenario 6 - Split View
  - Scenario 7 - Scroll, Zoom
  - Scenario 8 - CoreWetStrokeUpdateSource
  - Scenario 9 - Replay
  - Scenario 10 - Custome Pen
  - Scenario 11 - High Contrast
- https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/ComplexInk
  - Direct2X
- https://github.com/Microsoft/Windows-appsample-coloringbook
  - Customize Erase Button
- https://github.com/Microsoft/Windows-tutorials-inputs-and-devices/tree/master/GettingStarted-Ink
  - Adding basic ink support
  - Adding an ink toolbar
  - Supporting handwriting recognition
  - Supporting basic shape recognition
  - Saving and loading ink
- https://github.com/microsoft/Windows-tutorials-inputs-and-devices
  - Digital Pen Button -> Selection Tool
- https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/LowLatencyInput
  - Low Latency Input
- https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicInput
  - **Listen for events on XAML elements**: Use events on XAML elements to listen for various types of input, such as pointer pressed / released, pointer enter / exited, tap / double-tap, and right-tap / press-and-hold.
  - **Retrieve properties about a pointer object**: Use a PointerPoint object to retrieve information common to all pointers (such as X/Y coordinates) as well as information specific to the type of input being used (such as mouse wheel information).
  - **Query input capabilities for the system**: Use the KeyboardCapabilities, MouseCapabilities, and TouchCapabilities classes to determine what types of input are available on the current system.
  - **Manipulate a XAML element**: Use the ManipulationMode property to register for specific manipulation events on XAML elements and react to them in order to move and rotate the element.
  - **Manipulate an object using a GestureRecognizer**: Use an instance of a GestureRecognizer to move and rotate an object. This is useful if your app uses its own framework and, thus, cannot use the manipulation events on XAML elements.

### InkToolbar

- https://docs.microsoft.com/zh-cn/uwp/api/windows.ui.xaml.controls.inktoolbar.initialcontrols?view=winrt-19041#Windows_UI_Xaml_Controls_InkToolbar_InitialControls


### InkPresenter

- https://docs.microsoft.com/zh-cn/uwp/api/windows.ui.input.inking.inkpresenter?view=winrt-19041

### Blogs for Ink

- https://blogs.windows.com/windowsdeveloper/2015/09/08/going-beyond-keyboard-mouse-and-touch-with-natural-input-10-by-10/
- https://blogs.windows.com/windowsdeveloper/2016/11/22/windows-ink-2-digging-deeper-with-ink-and-pen/
- https://blogs.windows.com/windowsdeveloper/2016/11/23/windows-ink-3-beyond-doodling/

### Erase Part

- https://stackoverflow.com/questions/53632049/erasing-portion-of-a-stoke-in-uwp

## Touch Interactions

- https://docs.microsoft.com/zh-cn/windows/uwp/design/input/touch-interactions

## WinUI

- https://docs.microsoft.com/zh-cn/windows/uwp/design/controls-and-patterns/
- https://docs.microsoft.com/zh-cn/windows/apps/winui/winui2/
- https://github.com/Microsoft/Xaml-Controls-Gallery

## Win2D

- https://github.com/Microsoft/Win2D
- http://microsoft.github.io/Win2D/html/Introduction.htm
- http://microsoft.github.io/Win2D/WinUI3/html/Introduction.htm (Note: We plan to migrate to WinUI3 in the future)

## User Custom Control

- https://social.technet.microsoft.com/wiki/contents/articles/32828.uwp-how-to-create-and-use-custom-control.aspx
- https://almirvuk.blogspot.com/2016/08/uwp-create-custom-ui-control.html
