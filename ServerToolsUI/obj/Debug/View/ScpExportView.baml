   M S B A M L   `   `   `  ����H  DServerToolsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ServerToolsUI.ViewServerToolsUI.View  5      &     ServerToolsUI.View.ScpExportView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:ServerToolsUI.View  5        N� Title$  ScpExportPage��5
         MaterialDesignBody#���   5        MaterialDesignPaper N� 
Background# �  5      $��Medium"�5      $��14(�5       N� 
FontFamily$X Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5       � 	Resources 5        	BoolToVis��%   &	       �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��15q�6   $��Center=�5        MaterialDesignFlatButton#���  5       )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Voltar��5"      ��5!      �� *BackCommand+.��5#      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ ArrowLeftCircleOutline=�6*   $
��auto��5$   *   $
��auto��5%   *   5&      �� 5'      $
��Left=�6   $��Center=�5(      $��28(�5)      $��15q�6&      MaterialDesignTitleTextBlock#���  5*      Export SCP File5,      5-      �� 5.   
   $��Export Information��6   $��1��5/      $��15q�6      MaterialDesignCardGroupBox#���  50      .��51      �  52      �� 53      �� 54      �� 55       57      �� 58      �� 59      .+�5;      ��   MaterialDesignTextBox#���  6   $	��250��5<      $��5q�6&   $��2��5=      $
 Host��5>      $
��Left=�5?      ��5@      �� *Host+�� 5A      $��Itens to export:��6   $��1��5B      $��5q�5C      $
��Left=�5D         MaterialDesignBody1TextBlock#���  5E      $��Center=�5F      �� 5G      $��1��6   $��1��5H      $
��Left=�5I      $��5q�5J      $	��200��5L       & "MaterialDesignFloatingHintComboBox#���  5M      	 �� SelectedIndex	 5K      �� *FileContent+.@�5N      �� $	��All��6#   �� 5O      $��System��6#   �� 5P      $
��BIOS��6#   �� 5Q      $��IDRAC��6#   �� 5R      $	��NIC��6#   �� 5S      $��FC��6#   �� 5T      $��	Lifecycle��6#   �� 5U      $
��RAID��6#   5V      �� 5W      $��Export Mode��6   $��2��5X      $��5q�5Y      #���  5Z      $��Center=�5[      $
��Left=�5\      �� 5]      $��2��6   $��1��5^      $
��Left=�5_      $��5q�5`      $	��200��5b      #���  6'   	 5a      �� *
ExportMode+.@�5c      �� $��Default��6#   �� 5d      $��Clone��6#   �� 5e      $��Replace��6#   5f      5g      5h      �� 5i   
   
 � ToolTip$
 Iniciar Export��6   $ Begin Export��5j      $��2��5k      $��15q�5l      $��Bottom=�5m      ��5n      �� *ExportCommand+.��63   Export5p      #  MaterialDesignThemes.Wpf.Card  5q   
   $��1��6   $��1��5r      $	��Top=�5s      $��15q�5t      y�5u      ��  �� Path$ NoExportCardVisible� �� 	Converter# [ .��5v      �� $��15q�5w      Nenhum Export em execução5y      5z        5{   
   $��1��6   $��1��5|      $��15q�5}      $	��Top=�5~      y�5      �� $ ExportRunning�# [ .��5�      �� $ Vertical=�6   .+�5�      �� $��15q�6   Exporting File...5�      � 5�      $��15q�6    � IsIndeterminate . 5�      5�      5�        5�   
   $��2��6   $��1��5�      $��15q�5�      y�5�      �� $ ExportFinished�# [ .��5�      �� $��15q�6   $ 
Horizontal=�5�      .+�5�        $��15q�6*   $
��auto��5�   *   $
��auto��5�   *   $��Center=�5�   *   ��5�      �� 5�       �� 
TargetType"  6    .�5�      ��  �� Property �@ Kind �� Value$ CheckCircle=�65   �� 5�       � ��
 � � �6;    �� Triggers 5�      �� 5�   "    �� Binding 6.   �� *
ExportFail+ �� Value True6M   .a�5�   &   ��  �@ Kind$ CloseCircle=�6=   �� 5�   &    � ��
 �  ��6C   5�   #   5�      5�      �� 5�      $��15q�5�      $��Center=�5�      ��5�      �� *ExportMessage+5�      5�      5�      5�      5�      