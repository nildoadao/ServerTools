   M S B A M L   `   `   `  ����H  DServerToolsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ServerToolsUI.ViewServerToolsUI.View  5      )    #ServerToolsUI.View.CustomScriptView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:ServerToolsUI.View     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
        N� Title$  CustomScriptView��5	         MaterialDesignBody#���   5        MaterialDesignPaper N� 
Background# �  5      $��Medium"�5      $��14(�5       N� 
FontFamily$X Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5       � 	Resources 5        	BoolToVis��%   &	       �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��5* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   $	��360��5      �� 5      $��* �6   .+�5   
   �� $��1��6   $��3��5        �� ResizeDirection$ Columns=�5!      $��5��5"      5#      �� 5$   
    �� Orientation$ 
Horizontal=�6   $��3��5%      .+�5&      �� $��15q�6   $��Center=�5'        MaterialDesignFlatButton#���  5)      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Voltar��5*      ��5(      �� *BackCommand+.��5+      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]	   Kind$	 ArrowLeftCircleOutline=�6*   $
��auto��5,   *   $
��auto��5-   *   5.      �� 5/      $
��Left=�6   $��28(�50      $��15q�6&      MaterialDesignTitleTextBlock#���  51      Custom Script53      54      �� 56   
   $��15q�6   $��1��57      $��1��58        MaterialDesignCardGroupBox#���  59      $��Script��5:      
������ .��5<      �  5=      �� 5>      $
��auto �6$    5@      �� 5A      $
��auto �6'   �� 5B      $
��auto �6'   .+�5D      �� $��File:��6      MaterialDesignBody1TextBlock#���  5E       $��5q�5F       $��Center=�5G       $��Right=�5H       �� 5I      $ 
Horizontal=�6   $��1��66   $
��Left=�6F   .+�5J      ��   MaterialDesignTextBox#���  6   $	��200��5K      $��5q�6*   $
 Path��5L      $��Center=�5M      ��5N      �� *
FilePath+"
 �� ValidatesOnNotifyDataErrors
 . �� 5O      #���  6   $��5q�5P      $��Center=�5Q      $ Select Firmware��5S      ��5R      �� *OpenFolderCommand+.��5T        $	 FileFindOutline=�62   $
��auto��5U   2   $
��auto��5V   2   5W      5X      5Y      5Z      �� 5[   
   #���  6   $��1��5\      $��Add Servers��5]      $��15q�5^      .��5_      �  5`      �� 5a      $
��auto �6$   �� 5b      $
��auto �6$   .+�5d      �� $ 
Horizontal=�6   .+�5e      �� $��5q�6   #���  6*   $
��Left=�5f      $	��200��5g      $ IP or Hostname��5h      $��Center=�5i      ��5j      �� *Server+
 . .��6Y   5k      �� 5l      -   5        $��AddServerButton��5l       & "MaterialDesignFloatingActionButton#���  5m      $��40��5n      $��40��6(   $
��Left=�5o       � ToolTip$ 
Add Server��5p       �� 	IsDefault . $��Center=�5s      $��5q�68   ��5r      �� *AddServerCommand+.��5t        $
	 Plus=�62   $
��auto��6>   $
��auto��6K   5u      �� 5v      #���  6   $
��Left=�5w      $��Center=�5x      $ Add List��5z      $��5q�5{      ��5y      �� *OpenServerListCommand+.��5|        $	 FormatListNumbered=�5}      $
��auto��5~      $
��auto��5      5�      5�      5�      5�      #  MaterialDesignThemes.Wpf.Card  5�   
   $��2��6   $��15q�5�      $
��auto��5�      y�5�      ��  �� Path$ NoServerCardVisible� �� 	Converter# [ .��5�      �� $��Center=�5�      $��15q�5�      No Server Added.5�      5�        5�   
   $��2��6   $��15q�5�      $
��auto��5�      y�5�      �� $ 
HasServers�# [ .��5�      &   System.Windows.Controls.DataGrid  -   5        $��ServersDataGrid��5�         AutoGenerateColumns .  
������    CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     
IsReadOnly . ��5�      �� *	Servers+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�         FontSize$ 12Q�5�      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�      �� 4  .System.Windows.Controls.DataGridTemplateColumn  5�         CellTemplate 5�      �� 5�      .R�5�   "   �� $��DeleteButton��6)   $��Center=�5�   %   #���  5�   %   ��5�   %   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" N�$% DataContext.RemoveServerCommand���5�   %   �� .��5�   &     $	 MinusCircle=�6>   $��14��5�   1   $��14��5�   1   
���  ��5�   1   5�   #   5�      5�         	RowHeight$ 34Q�5�   &   5�      5�      �� 5�   
   $��15q�6   $��3��5�      $��Bottom=�5�      $ Executar Scipt��5�      ��5�      �� *RunScriptCommand+.��5�   A   	Execute5�        5�   
   $��2��5�      $��1��5�      $��15q�5�      y�5�      �� $ NoJobCardVisible�# [ .��5�      �� $��Center=�5�      $��15q�5�      	No Job.5�      5�        5�   
   $��2��6   $��1��5�      $
��auto��5�      $��15q�5�      y�5�      �� $ HasJobs�# [ .��5�      �� .��5�      �� .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� $��15q�6$   $��Center=�5�       Jobs Running5�      �� 5�      $��15q�6!   $ Cancel��5�      ��5�      �� *CancelCommand+.��5�   E   Cancel5�      �� 5�      $��15q�6!   $ 
Clear Jobs��5�      ��5�      �� *ClearJobsCommand+.��5�   I   
Clear Jobs5�      5�        5�      -   5        $��JobsDataGrid��5�      $��15q�5�      $��Center=�5�       .   . ��5�      �� *Jobs+ 5�        5�      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Server��61    6A   �� *Server+  5�      $ Status��61    6A   �� *	JobStatus+  5�      $ Message��61    6B   �� *
JobMessage+5�      5�      5�      5�      5�      5�      