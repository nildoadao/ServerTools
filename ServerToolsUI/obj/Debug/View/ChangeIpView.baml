   M S B A M L   `   `   `  ����H  DServerToolsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ServerToolsUI.ViewServerToolsUI.View  5      %    ServerToolsUI.View.ChangeIpView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:ServerToolsUI.View  5        N� Title$  ChangeIpView��5	         MaterialDesignBody#���   5
        MaterialDesignPaper N� 
Background# �  5      $��Medium"�5      $��14(�5       N� 
FontFamily$X Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5       � 	Resources 5        	BoolToVis��%   &	       �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��15q�6   $��Center=�5        MaterialDesignFlatButton#���  5       )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Voltar��5"      ��5!      �� *BackCommand+.��5#      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ ArrowLeftCircleOutline=�6*   $
��auto��5$   *   $
��auto��5%   *   5&      �� 5'      $
��Left=�6   $��Center=�5(      $��28(�5)      $��15q�6&      MaterialDesignTitleTextBlock#���  5*      Change Idrac IP5,      5-      �� 5.   
     MaterialDesignCardGroupBox#���  6   $��1��5/      $
��auto��50      $��Network Information��51      $��15q�52      .��53      �  54      �� 55      $
��auto �6'   �� 56      $
��auto �6'    58      �� 59      $
��auto �6$   �� 5:      $
��auto �6$   �� 5;      $
��auto �6$   �� 5<      $
��auto �6$   .+�5>      �� $��5q�6     MaterialDesignTextBox#���  6%   $
��Left=�5?      $��2��5@      $	��200��5A      $ IP��5B      $��Center=�5C      ��5D      �� *	NetworkIp+"	 �� ValidatesOnNotifyDataErrors	 . �� 5E      $��5q�6   #���  6%   $
��Left=�5F      $��2��5G      $��1��5H      $	��200��5I      $
 Mask��5J      $��Center=�5K      ��5L      �� *NetworkMask+	 . �� 5M      $��5q�6   $��Bottom=�5O      $��2��5P      $ Search Idracs��5Q      ��5N      �� *SearchIdracsCommand+.��5Q   H   Search5S      �� 5T      $��5q�6   $��2��5V      $��1��5W      $ 
Clear Jobs��5X      $��Bottom=�6E   ��5U      �� *CancelCommand+.��5X   `   Stop5Z      #  MaterialDesignThemes.Wpf.Card  5[      $��4��5\      $��2��5]      $��5q�5^      $	��Top=�5_      y�5`      �� 
 �� Path$
 	Searching� �� 	Converter# [ .��5a      �� $ Vertical=�6!   .+�5b      �� $��15q�6$   $��Bottom=�5c   $   Searching...5e      � 5f      $��5q�6&    � IsIndeterminate . 5g      5h      5i      5j      5k      �� 5l   
   $��1��5m      $��1��5n      $
��auto��5o      $��15q�5p      $��Idracs Found��5q      .��5r      �� $ Vertical=�5s      .+�5t      �� $	��200��5u      .��5v      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .  ��5x      �� *Idracs+   Columns 5y      0  *System.Windows.Controls.DataGridTextColumn  5z      ,  &System.Windows.Controls.DataGridColumn   Header$ IP��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Ip+  5{      $ Serial Number��61    6H   �� *SerialNumber+  5|      $ New IP��61    6A   �� *NewIp+	 .   5}      $ New Mask��61    6C   �� *	NewMask+	 .   5~      $ New Gateway��61    6F   �� *
NewGateway+	 . 5�      5�      �� 5�      $��15q�5�      $��2��5�      $��Bottom=�5�      $
��Left=�5�      $ Procurar Idracs��5�      ��5�      �� *ChangeIpCommand+.��5�   E   	Change IP5�      5�      5�        5�   
   $��2��6   $��2��5�      $��15q�5�      .��5�      �� .��5�      �� .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� $��15q�6$   $��Center=�5�       Jobs Running5�      �� 5�      $��15q�6!   $ Cancel��5�      ��5�      �� *CancelExecutionCommand+.��5�   E   Cancel5�      �� 5�      $��15q�6!   $ 
Clear Jobs��5�      ��5�      �� *ClearJobsCommand+.��5�   I   
Clear Jobs5�      5�        5�      -   5        $��JobsDataGrid��5�      $��15q�5�      $��Center=�5�       .     
IsReadOnly . ��5�      �� *Jobs+ 5�        5�      $ Server��61    6A   �� *Server+  5�      $ Status��61    6A   �� *	JobStatus+  5�      $ Message��61    6B   �� *
JobMessage+5�      5�      5�      5�      5�      5�      