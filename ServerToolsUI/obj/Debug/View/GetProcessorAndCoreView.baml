   M S B A M L   `   `   `  ����H  DServerToolsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ServerToolsUI.ViewServerToolsUI.View  5      0    *ServerToolsUI.View.GetProcessorAndCoreView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:ServerToolsUI.View     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
        N� Title$  GetProcessorAndCoreView��5	         MaterialDesignBody#���   5        MaterialDesignPaper N� 
Background# �  5      $��Medium"�5      $��14(�5       N� 
FontFamily$X Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5       � 	Resources 5        	BoolToVis��%   &	       �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��5* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   $	��360��5      �� 5      $��* �6   .+�5    
   �� $��1��6   $��3��5!       �� ResizeDirection$ Columns=�5"      $��5��5#      5$      �� 5%   
    �� Orientation$ 
Horizontal=�6   $��3��5&      .+�5'      �� $��15q�6   $��Center=�5(        MaterialDesignFlatButton#���  5*      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Voltar��5+      ��5)      �� *BackCommand+.��5,      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]	   Kind$	 ArrowLeftCircleOutline=�6*   $
��auto��5-   *   $
��auto��5.   *   5/      �� 50      $
��Left=�6   $��28(�51      $��15q�6&      MaterialDesignTitleTextBlock#���  52      Processor and Core Count54      55      �� 57   
   $��15q�6   $��1��58      $��1��59        MaterialDesignCardGroupBox#���  5:      $��Manufacturer��5;      
������ .��5=      �  5>      �� 5?      $
��auto �6$    5A      �� 5B      $
��auto �6'   �� 5C      $
��auto �6'   .+�5E      �� -   5        $��ModeCombobox��5J       & "MaterialDesignFloatingHintComboBox#���  5E      $	��200��5F      $��5q�6+   $��1��5G      $��1��6,   $
��Left=�5H      $��Center=�5I      
 �� SelectedIndex
 5K      �� *SelectedManufacturer+.@�5L      �� $
��Dell��6#   �� 5M      $	��HPE��6#   5N      5O      5P      �� 5Q   
   #���  6   $��1��5R      $��Add Servers��5S      $��15q�5T      .��5U      �  5V      �� 5W      $
��auto �6$   �� 5X      $
��auto �6$   .+�5Z      �� $ 
Horizontal=�6   .+�5[      �� $��5q�6     MaterialDesignTextBox#���  6*   $
��Left=�5\      $	��200��5]      $ IP or Hostname��5^      $��Center=�5_      ��5`      �� *Server+" �� ValidatesOnNotifyDataErrors . .��6Y   5a      �� 5b      -   5        $��AddServerButton��5b       & "MaterialDesignFloatingActionButton#���  5c      $��40��5d      $��40��6(   $
��Left=�5e       � ToolTip$ 
Add Server��5f       �� 	IsDefault . $��Center=�5i      $��5q�68   ��5h      �� *AddServerCommand+.��5j        $
	 Plus=�62   $
��auto��6>   $
��auto��6K   5k      �� 5l      #���  6   $
��Left=�5m      $��Center=�5n      $ Add List��5p      $��5q�5q      ��5o      �� *OpenServerListCommand+.��5r        $	 FormatListNumbered=�5s      $
��auto��5t      $
��auto��5u      5v      5w      5x      5y      #  MaterialDesignThemes.Wpf.Card  5z   
   $��2��6   $��15q�5{      $
��auto��5|      y�5}      ��  �� Path$ NoServerCardVisible� �� 	Converter# [ .��5~      �� $��Center=�5      $��15q�5�      No Server Added.5�      5�        5�   
   $��2��6   $��15q�5�      $
��auto��5�      y�5�      �� $ 
HasServers�# [ .��5�      &   System.Windows.Controls.DataGrid  -   5        $��ServersDataGrid��5�         AutoGenerateColumns .  
������    CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     
IsReadOnly . ��5�      �� *	Servers+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�         FontSize$ 12Q�5�      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�      �� 4  .System.Windows.Controls.DataGridTemplateColumn  5�         CellTemplate 5�      �� 5�      .R�5�   "   �� $��DeleteButton��6)   $��Center=�5�   %   #���  5�   %   ��5�   %   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" N�$% DataContext.RemoveServerCommand���5�   %   �� .��5�   &     $	 MinusCircle=�6>   $��14��5�   1   $��14��5�   1   
���  ��5�   1   5�   #   5�      5�         	RowHeight$ 34Q�5�   &   5�      5�      �� 5�   
   $��15q�6   $��3��5�      $��Bottom=�5�      $ Executar Scipt��5�      ��5�      �� *RunScriptCommand+.��5�   A   	Execute5�        5�   
   $��2��5�      $��1��5�      $��15q�5�      y�5�      �� $ NoJobCardVisible�# [ .��5�      �� $��Center=�5�      $��15q�5�      	No Job.5�      5�        5�   
   $��2��6   $��1��5�      $
��auto��5�      $��15q�5�      y�5�      �� $ HasJobs�# [ .��5�      �� .��5�      �� .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� $��15q�6$   $��Center=�5�       Jobs Running5�      �� 5�      $��15q�6!   $ Cancel��5�      ��5�      �� *CancelCommand+.��5�   E   Cancel5�      �� 5�      $��15q�6!   $ 
Clear Jobs��5�      ��5�      �� *ClearJobsCommand+.��5�   I   
Clear Jobs5�      5�        5�      -   5        $��JobsDataGrid��5�      $��15q�5�      $��Center=�5�       .   . ��5�      �� *Jobs+ 5�        5�      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Server��61    6A   �� *Server+  5�      $ Status��61    6A   �� *	JobStatus+  5�      $ Message��61    6B   �� *
JobMessage+5�      5�      5�      5�      5�      5�      