   M S B A M L   `   `   `  ����H  DServerToolsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ServerToolsUIServerToolsUI  5      W0clr-namespace:ServerToolsUI.ViewModel.Validators"ServerToolsUI.ViewModel.Validators  5      +    %ServerToolsUI.View.FirmwareUpdateView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      'localclr-namespace:ServerToolsUI  5      A
validators0clr-namespace:ServerToolsUI.ViewModel.Validators  5        N� Title$  FirmwareUpdatePage��5         MaterialDesignBody#���   5        MaterialDesignPaper N� 
Background# �  5      $��Medium"�5      $��14(�5       N� 
FontFamily$X Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5       � 	Resources 5        	BoolToVis��%   &	       �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��5* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   $	��360��5      �� 5      $��* �6   .+�5    
   �� $��1��6   $��3��5!       �� ResizeDirection$ Columns=�5"      $��5��5#      5$      �� 5%   
    �� Orientation$ 
Horizontal=�6   .+�5&      �� $��15q�6   $��Center=�5'        MaterialDesignFlatButton#���  5(       � ToolTip$ Voltar��5*      ��5)      �� *BackCommand+.��5+      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]	   Kind$	 ArrowLeftCircleOutline=�6*   $
��auto��5,   *   $
��auto��5-   *   5.      �� 5/      $
��Left=�6   $��Center=�50      $��28(�51      $��15q�6"      MaterialDesignTitleTextBlock#���  52      $��3��53      Firmware Update69   54      �� 56   
   $��15q�6   $��1��57      $��1��58        MaterialDesignCardGroupBox#���  59      $��Select Firmware File��5:      
������ .��5<      �  5=      �� 5>      $
��auto �6$   �� 5?      $
��auto �6$    5A      �� 5B      $
��auto �6'   �� 5C      $
��auto �6'   .+�5E      �� $��File:��6      MaterialDesignBody1TextBlock#���  5F       $��5q�5G       $��Center=�5H       $��Right=�5I       �� 5J      $ 
Horizontal=�6   $��1��5K      $
��Left=�5L      .+�5M      ��   MaterialDesignTextBox#���  6   $	��200��5N   "   $��5q�6.   )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$

 Path��5O   "   $��Center=�5P   "   ��5Q      �� *FirmwarePath+" �� ValidatesOnNotifyDataErrors . �� 5R      -   5        $��OpenFolderButton��5U      #���  5R      $��5q�5S      $��Center=�5T      $ Select File��5W      ��5V      �� *OpenFolderCommand+.��5X        $	 FileFindOutline=�62   $
��auto��5Y   2   $
��auto��5Z   2   5[      5\      �� 5]      $��Install Mode:��6   #���  5^       $��5q�5_       $��1��6+   $��Right=�5`       $��Center=�5a       �� 5b      -   5        $��ModeCombobox��5g       & "MaterialDesignFloatingHintComboBox#���  5b      $	��200��5c      $��5q�6+   $��1��5d      $��1��6,   $
��Left=�5e      $��Center=�5f       �� SelectedIndex 5h      �� *SelectedMode+.@�5i      �� $	��Now��6#   �� 5j      $��Now and Reboot��6#   �� 5k      $��	Next boot��6#   5l      5m      5n      �� 5o   
   #���  6   $��1��5p      $��Add Servers��5q      $��15q�5r      .��5s      �  5t      �� 5u      $
��auto �6$   �� 5v      $
��auto �6$   .+�5x      �� $ 
Horizontal=�6   .+�5y      �� $��5q�6   #���  6*   $
��Left=�5z      $	��200��5{      $
 IP or Hostname��5|      $��Center=�5}      ��5~      �� *Server+.��67   5      �� 5�       &	 "MaterialDesignFloatingActionButton#��� 	 6   $��40��5�      $��40��6(   $
��Left=�5�       �� 	IsDefault . $��Center=�5�      $��5q�68   $ Add Servers��5�      ��5�      �� *AddServerCommand+.��5�        $
	 Plus=�62   $
��auto��6>   $
��auto��6K   5�      �� 5�      #���  6   $
��Left=�5�      $��Center=�5�      $ Add List��5�      $��5q�5�      ��5�      �� *OpenServerListCommand+.��5�        $	 FormatListNumbered=�5�      $
��auto��5�      $
��auto��5�      5�      5�      5�      5�      #  MaterialDesignThemes.Wpf.Card  5�   
   $��2��6   $��15q�5�      $
��auto��5�      y�5�      ��  �� Path$ NoServerCardVisible� �� 	Converter# [ .��5�      �� $��Center=�5�      $��15q�5�      No Server Added.5�      5�        5�   
   $��2��6   $��15q�5�      $
��auto��5�      y�5�      �� $ 
HasServers�# [ .��5�      &   System.Windows.Controls.DataGrid  -   5        $��ServersDataGrid��5�         AutoGenerateColumns .  
������    CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     
IsReadOnly . ��5�      �� *	Servers+ .    Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�         FontSize$ 12Q�5�      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�      �� 4  .System.Windows.Controls.DataGridTemplateColumn  5�         CellTemplate 5�      �� 5�      .R�5�   "   �� $��DeleteButton��6)   $��Center=�5�   %   #���  5�   %   ��5�   %   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" N�$% DataContext.RemoveServerCommand���5�   %   �� .��5�   &     $	 MinusCircle=�6>   $��14��5�   1   $��14��5�   1   
���  ��5�   1   5�   #   5�      5�         	RowHeight$ 34Q�5�   &   5�      5�      �� 5�   
   -   5        $��UpdateButton��5�      $��15q�5�      $��3��5�      $��Bottom=�5�      $ Update��5�      ��5�      �� *UpdateFirmwareCommand+.��6;   Update5�        5�   
   $��2��5�      $��1��5�      $��2��5�      $��15q�5�      y�5�      �� $ NoJobCardVisible�# [ .��5�      �� $��Center=�5�      $��15q�5�      	No Job.5�      5�        5�   
   $��2��6   $��1��5�      $��15q�5�      y�5�      �� $ HasJobs�# [ .��5�      �� .��5�      �� .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� $��15q�6$   $��Center=�5�       Jobs Running5�      �� 5�      $��15q�6!   $
 Cancelar��5�      ��5�      �� *CancelCommand+.��5�   G   Cancel5�      �� 5�      $��15q�6!   $
 Limpar Jobs��5�      ��5�      �� *ClearJobsCommand+.��5�   J   
Clear Jobs5�      5�        5�      -   5        $��JobsDataGrid��5�      $��15q�5�      $��Center=�5�       .   . ��5�      �� *Monitor.Jobs+ 5�        5�      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Server��61    6A   �� *Server+  5�      $ Serial Number��61    6H   �� *SerialNumber+  5�      $ Job Id��61    6A   �� *JobId+  5�      $
 Name��61    6?   �� *	JobName+  5�      $ Status��61    6A   �� *	JobStatus+  5�      $ %��61    6<   �� *JobPercentComplete+  5�      $ Message��61    6B   �� *
JobMessage+5     5     5     5     5     5     