   M S B A M L   `   `   `  ����H  DServerToolsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ServerToolsUI.ViewServerToolsUI.View  5      W0clr-namespace:ServerToolsUI.ViewModel.Validators"ServerToolsUI.ViewModel.Validators  5      &     ServerToolsUI.View.ScpImportView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:ServerToolsUI.View     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      A
validators0clr-namespace:ServerToolsUI.ViewModel.Validators  5        N� Title$  ScpImportView��5         MaterialDesignBody#���   5        MaterialDesignPaper N� 
Background# �  5      $��Medium"�5      $��14(�5       N� 
FontFamily$X Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5       � 	Resources 5        	BoolToVis��%   &	       �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��5* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   $	��360��5      �� 5      $��* �6   .+�5    
   �� $��1��6   $��3��5!       �� ResizeDirection$ Columns=�5"      $��5��5#      5$      �� 5%   
    �� Orientation$ 
Horizontal=�6   .+�5&      �� $��15q�6   $��Center=�5'        MaterialDesignFlatButton#���  5(      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Voltar��5*      ��5)      �� *BackCommand+.��5+      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]	   Kind$	 ArrowLeftCircleOutline=�6*   $
��auto��5,   *   $
��auto��5-   *   5.      �� 5/      $
��Left=�6   $��28(�50      $��15q�6&      MaterialDesignTitleTextBlock#���  51      $��3��52      Import SCP File54      55      �� 57   
   $��15q�6   $��1��58      $��1��59        MaterialDesignCardGroupBox#���  5:      $��Import Info��5;      
������ .��5=      �  5>      �� 5?      $
��auto �6$   �� 5@      $
��auto �6$   �� 5A      $
��auto �6$    5C      �� 5D      $
��auto �6'   �� 5E      $
��auto �6'   .+�5G      �� $��File:��6      MaterialDesignBody1TextBlock#���  5H       $��5q�5I       $��Center=�5J       $��Right=�5K       �� 5L      $ 
Horizontal=�6   $��1��66   $
��Left=�6F   .+�5M      ��   MaterialDesignTextBox#���  6   $	��200��5N      $��5q�6*   $
 Path��5O      $��Center=�5P      ��5Q      �� *
FilePath+"
 �� ValidatesOnNotifyDataErrors
 . �� 5R      #���  6   $��5q�5S      $��Center=�5T      $ Select Firmware��5V      ��5U      �� *OpenFolderCommand+.��5W        $	 FileFindOutline=�62   $
��auto��5X   2   $
��auto��5Y   2   5Z      5[      �� 5\      $��Target:��6   #���  5]       $��5q�5^       $��1��6+   $��Right=�5_       $��Center=�5`       �� 5a      -   5        $��ModeCombobox��5f       & "MaterialDesignFloatingHintComboBox#���  5a      $	��200��5b      $��5q�6+   $��1��5c      $��1��6,   $
��Left=�5d      $��Center=�5e       �� SelectedIndex 5g      �� *SelectedTarget+.@�5h      �� $	��All��6#   �� 5i      $��System��6#   �� 5j      $
��BIOS��6#   �� 5k      $��IDRAC��6#   �� 5l      $	��NIC��6#   �� 5m      $��FC��6#   �� 5n      $��	Lifecycle��6#   �� 5o      $
��RAID��6#   5p      �� 5q      $��Shutdown Mode:��6   $��2��5r      #���  5s      $��5q�5t      $��Right=�5u      $��Center=�5v      �� 5w      #���  6   $	��200��5x      $��5q�6'   $��2��5y      $��1��6(   $
��Left=�5z      $��Center=�5{       5|      �� *SelectedShutdown+.@�5}      �� $��Graceful��6#   �� 5~      $��Forced��6#   �� 5      $��	No Reboot��6#   5�      5�      5�      �� 5�   
   #���  6   $��1��5�      $��Adicionar Servidores��5�      $��15q�5�      .��5�      �  5�      �� 5�      $
��auto �6$   �� 5�      $
��auto �6$   .+�5�      �� $ 
Horizontal=�6   .+�5�      �� $��5q�6   #���  6*   $
��Left=�5�      $	��200��5�      $ IP or Hostname��5�      $��Center=�5�      ��5�      �� *Server+
 . .��6Y   5�      �� 5�      -   5        $��AddServerButton��5�       &	 "MaterialDesignFloatingActionButton#��� 	 5�      $��40��5�      $��40��6(   $
��Left=�5�       � ToolTip$ 
Add Server��5�       �� 	IsDefault . $��Center=�5�      $��5q�68   ��5�      �� *AddServerCommand+.��5�        $
	 Plus=�62   $
��auto��6>   $
��auto��6K   5�      �� 5�      #���  6   $
��Left=�5�      $��Center=�5�      $ Add List��5�      $��5q�5�      ��5�      �� *OpenServerListCommand+.��5�        $	 FormatListNumbered=�5�      $
��auto��5�      $
��auto��5�      5�      5�      5�      5�      #  MaterialDesignThemes.Wpf.Card  5�   
   $��2��6   $��15q�5�      $
��auto��5�      y�5�      ��  �� Path$ NoServerCardVisible� �� 	Converter# [ .��5�      �� $��Center=�5�      $��15q�5�      No Server Added.5�      5�        5�   
   $��2��6   $��15q�5�      $
��auto��5�      y�5�      �� $ 
HasServers�# [ .��5�      &   System.Windows.Controls.DataGrid  -   5        $��ServersDataGrid��5�         AutoGenerateColumns .  
������    CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     
IsReadOnly . ��5�      �� *	Servers+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�         FontSize$ 12Q�5�      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�      �� 4  .System.Windows.Controls.DataGridTemplateColumn  5�         CellTemplate 5�      �� 5�      .R�5�   "   �� $��DeleteButton��6)   $��Center=�5�   %   #���  5�   %   ��5�   %   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" N�$% DataContext.RemoveServerCommand���5�   %   �� .��5�   &     $	 MinusCircle=�6>   $��14��5�   1   $��14��5�   1   
���  ��5�   1   5�   #   5�      5�         	RowHeight$ 34Q�5�   &   5�      5�      �� 5�   
   $��15q�6   $��3��5�      $��Bottom=�5�      $ Iniciar Import��5�      ��5�      �� *ImportFileCommand+.��5�   A   
Importar5�        5�   
   $��2��5�      $��1��5�      $��15q�5�      y�5�      �� $ NoJobCardVisible�# [ .��5�      �� $��Center=�5�      $��15q�5�      	No Job.5�      5�        5�   
   $��2��6   $��1��5�      $��15q�5�      y�5�      �� $ HasJobs�# [ .��5�      �� .��5�      �� .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� $��15q�6$   $��Center=�5�       Jobs Running5�      �� 5�      $��15q�6!   $ 
Clear Jobs��5�      ��5�      �� *ClearJobsCommand+.��5�   I   
Clear Jobs5�      5�        5      -   5        $��JobsDataGrid��5      $��15q�5     $��Center=�5      .   . ��5     �� *Monitor.Jobs+ 5       5     ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Server��61    6A   �� *Server+  5     $ Serial Number��61    6H   �� *SerialNumber+  5	     $ Job Id��61    6A   �� *JobId+  5
     $
 Name��61    6?   �� *	JobName+  5     $ Status��61    6A   �� *	JobStatus+  5     $ %��61    6<   �� *JobPercentComplete+  5     $ Message��61    6B   �� *
JobMessage+5     5     5     5     5     5     