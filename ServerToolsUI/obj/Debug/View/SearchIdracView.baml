   M S B A M L   `   `   `  ����H  DServerToolsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:ServerToolsUI.ViewModel.Validators"ServerToolsUI.ViewModel.Validators  5      7 clr-namespace:ServerToolsUI.ViewServerToolsUI.View  5      (    "ServerToolsUI.View.SearchIdracView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      A
validators0clr-namespace:ServerToolsUI.ViewModel.Validators  5      ,local clr-namespace:ServerToolsUI.View  5         MaterialDesignBody#���   5
        MaterialDesignPaper  N� 
Background#  �  5       N� Title$ SearchIdracView��5      $��Medium"�5      $��14(�5       N� 
FontFamily$X Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5       � 	Resources 5        	BoolToVis��%   &	       �� 5   
   6'   5      .��5      �� .��5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $��2* �6    �� 5      $��2* �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $��2* �6#   �� 5      $��2* �6#   �� 5      $��3* �6#   .+�5!      ��  �� Orientation$ 
Horizontal=�6   $��3��5"      .+�5#      �� $��15q�6   $��Center=�5$        MaterialDesignFlatButton#���  5%      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Voltar��5'      ��5&      �� *BackCommand+.��5(      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ ArrowLeftCircleOutline=�6.   $
��auto��5)   *   $
��auto��5*   *   5+      �� 5,      $
��Left=�6   $��Center=�5-      $��28(�5.      $��15q�6&      MaterialDesignTitleTextBlock#���  5/      DHCP Deploy51      52      �� 53        MaterialDesignCardGroupBox#���  6   $��1��54      $��Network Information��55      $��15q�56      .��57      �  58      �� 59      �� 5:       5<      �� 5=      $
��auto �6(   �� 5>      $
��auto �6(   �� 5?      $
��auto �6(   �� 5@      $
��auto �6(   .+�5B      �� $��5q�6     MaterialDesignTextBox#���  6)   $
��Left=�5C      $��2��5D      $	��200��5E      $ IP��5F      $��Center=�5G      ��5H      �� *	NetworkIp+"	 �� ValidatesOnNotifyDataErrors	 . �� 5I      $��5q�6   #���  6)   $
��Left=�5J      $��2��5K      $��1��5L      $	��200��5M      $
 Mask��5N      $��Center=�5O      ��5P      �� *NetworkMask+	 . �� 5Q      $��5q�6   $��2��5S      $ Search Idracs��5T      ��5R      �� *SearchIdracsCommand+.��5T   H   Search5V      �� 5W      $��5q�6   $��2��5Y      $��1��5Z      $ 
Clear Jobs��5[      $��Bottom=�6E   ��5X      �� *CancelCommand+.��5[   `   Stop5]      #  MaterialDesignThemes.Wpf.Card  5^      $��4��5_      $��2��5`      $��5q�5a      $	��Top=�5b      y�5c      �� 
 �� Path$
 	Searching� �� 	Converter# [ .��5d      �� $ Vertical=�6%   .+�5e      �� $��5q�6(   Searching...5g      � 5h      $��5q�6*    � IsIndeterminate . 5i      5j      5k      5l      5m      �� 5n      $��2��5o      $	��Top=�5p      $	��350��5q      $��2��5r      $��15q�5s      $��Idracs Found��5t      .��5u      &   System.Windows.Controls.DataGrid  $��Center=�5v         AutoGenerateColumns .     
IsReadOnly . ��5y      �� *Idracs+   Columns 5z      0  *System.Windows.Controls.DataGridTextColumn  5{      ,  &System.Windows.Controls.DataGridColumn   Header$ IP��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Server+  5|      $ Serial Number��6-    6D   �� *SerialNumber+4	  .System.Windows.Controls.DataGridTemplateColumn	  5}       	  CellTemplate 5~      �� 5   "   .R�5�   &   �� $��DeleteButton��6-   $��Center=�5�   %   #���  5�   %   ��5�   %   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" N�$%
 DataContext.RemoveServerCommand���5�   %   �� .��5�   *     $ MinusCircle=�6B   $��14��5�   1   $��14��5�   1   
���  ��5�   1   5�   '   5�   #   5�      5�      5�      �� 5�      $��1��6   $��0��5�      $��5��5�      $��3��5�      �� 5�      $��1��6   $��1��5�      $��5��5�      $��3��5�      �� 5�      $��15q�6   $��1��5�      $��1��5�      #���  5�      $��
SCP Import��5�      
������ .��5�      �  5�      �� 5�      $
��auto �6(   �� 5�      $
��auto �6(   �� 5�      $
��auto �6(   �� 5�      $
��auto �6(    5�      �� 5�      $��* �6+   �� 5�      $��2* �6+   .+�5�      �� $��File:��6       MaterialDesignBody1TextBlock#���  5�       $��5q�5�       $��Center=�5�       $
��Left=�5�       �� 5�      $ 
Horizontal=�6!   $��1��6:   $
��Left=�6J   .+�5�      �� #���  6"   $
��auto��5�      $��5q�6+   $	��150��5�   "   $	��100��5�   "   $
 Path��5�      $��Center=�5�      ��5�      �� *ScpFilePath+	 . �� 5�      #���  6!   $��5q�5�      $��Center=�5�      $ Select Firmware��5�      ��5�      �� *OpenScpFileCommand+.��5�        $ FileFindOutline=�66   $
��auto��5�   2   $
��auto��5�   2   5�      5�      �� 5�      $��Target:��6    #���  5�       $��5q�5�       $��1��6+   $
��Left=�5�       $��Center=�5�       �� 5�      -   5        $��ModeCombobox��5�       & "MaterialDesignFloatingHintComboBox#���  5�      $
��auto��5�      $��5q�6,   $��1��5�      $��1��6,   $
��Left=�5�      $��Center=�5�       �� SelectedIndex 5�      �� *SelectedTarget+.@�5�      �� $	��All��6'   �� 5�      $��System��6'   �� 5�      $
��BIOS��6'   �� 5�      $��IDRAC��6'   �� 5�      $	��NIC��6'   �� 5�      $��FC��6'   �� 5�      $��	Lifecycle��6'   �� 5�      $
��RAID��6'   5�      �� 5�      $��Shutdown Mode:��6    $��2��5�      #���  5�      $��5q�5�      $
��Left=�5�      $��Center=�5�      �� 5�      #���  6   $
��auto��5�      $��5q�6(   $��2��5�      $��1��6(   $
��Left=�5�      $��Center=�5�       5�      �� *SelectedShutdown+.@�5�      �� $��Graceful��6'   �� 5�      $��Forced��6'   �� 5�      $��	No Reboot��6'   5�      �� 5�      $��5q�6   $��3��5�      $��2��5�      $ Procurar Idracs��5�      ��5�      �� *ImportScpFileCommand+.��5�   J   Import5�      5�      5�      �� 5�      $��15q�6   $��2��5�      $��1��5�      #���  5�      $��Firmware Update��5�      
������ .��5�      �  5�      �� 5�      $
��auto �6(   �� 5�      $
��auto �6(   �� 5�      $
��auto �6(    5�      �� 5�      $
��auto �6+   �� 5�      $
��auto �6+   .+�5�      �� $��File:��6    #���  5�       $��5q�5       $��Center=�5      $
��Left=�5      �� 5     $ 
Horizontal=�6!   $��1��5     $
��Left=�5     .+�5     �� #���  6"   $
��auto��5  "   $��5q�6/   $	��100��6:   $	��150��6I   $
 Path��5  "   $��Center=�5	  "   ��5
     �� *FirmwarePath+	 . �� 5     -   5        $��OpenFolderButton��5     #���  5  !   $��5q�5     $��Center=�5      � ToolTip$ Select File��5     ��5     �� *OpenFirmwareFileCommand+.��5       $ FileFindOutline=�66   $
��auto��5  2   $
��auto��5  2   5     5     �� 5     $��Install Mode:��6    #���  5      $��5q�5      $��1��6+   $
��Left=�5      $��Center=�5      �� 5     #���  6   $
��auto��5     $��5q�6,   $��1��5     $��1��6,   $
��Left=�5     $��Center=�5      5      �� *SelectedMode+.@�5!     �� $	��Now��6'   �� 5"     $��Now and Reboot��6'   �� 5#     $��	Next boot��6'   5$     �� 5%     $��5q�6   $��2��5'     $��2��5(     $ Search Idracs��5)     ��5&     �� *UpdateFirmwareCommand+.��5)  H   Update5+     5,     5-     �� 5.     $��15q�6   $��3��5/     $��1��50     #���  51     $��Script��52     
������ .��54     �  55     �� 56     $
��auto �6(   �� 57     $
��auto �6(    59     �� 5:     $
��auto �6+   �� 5;     $
��auto �6+   .+�5=     �� $��File:��6    #���  5>      $��5q�5?      $��Center=�5@      $��Right=�5A      �� 5B     $ 
Horizontal=�6!   $��1��6:   $
��Left=�6J   .+�5C     �� #���  6"   $
��auto��5D     $��5q�6+   $	��100��66   $	��150��6E   $
 Path��5E     $��Center=�5F     ��5G     �� *ScriptFilePath+	 . �� 5H     #���  6!   $��5q�5I     $��Center=�5J     $ Select Firmware��5L     ��5K     �� *OpenScriptFileCommand+.��5M       $ FileFindOutline=�66   $
��auto��5N  2   $
��auto��5O  2   5P     5Q     �� 5R     $��5q�6   $��2��5T     $��2��5U     $ Procurar Idracs��5V     ��5S     �� *RunScriptCommand+.��5V  J   
Run Script5X     5Y     5Z       5[     $��1��6"   $��3��5\     $��2��5]     $��15q�5^     .��5_     �� .��5`     �� .+�5a     �� $ 
Horizontal=�6%   .+�5b     �� $��15q�6(   $��Center=�5c      Jobs Running5e     �� 5f     $��15q�6%   $ 
Clear Jobs��5h     ��5g     �� *ClearJobsCommand+.��5h  I   
Clear Jobs5j     5k       5l     -   5        $��JobsDataGrid��5l  #   $��15q�5m     $	��500��5n  #   $��Center=�5o      .   . ��5r     �� *Monitor.Jobs+ 5s       5t  "   $ Server��65    6E   �� *Server+  5u  "   $ Serial Number��65    6L   �� *SerialNumber+  5v  "   $ Job Id��65    6E   �� *JobId+  5w  "   $
 Name��65    6C   �� *	JobName+  5x  "   $ Status��65    6E   �� *	JobStatus+  5y  "   $ %��65    6@   �� *JobPercentComplete+  5z  "   $ Message��65    6F   �� *
JobMessage+5|     5}     5~     5     5�     5�     5�     