   M S B A M L   `   `   `  ����H  DServerToolsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ServerToolsUI.ViewServerToolsUI.View  5      (    "ServerToolsUI.View.CredentialsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:ServerToolsUI.View  5         MaterialDesignBody#���   5
        MaterialDesignPaper#���  5      $��Medium"�5      $��14(�5      $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5      .��5      �   � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��15q�6   .��5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�5      .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ ShieldAccount=�5      $
��auto��5      $
��auto��5      $��Center=�5      $��15q�5      �� 5      $��15q�6   $��Center=�5       $
��Left=�5!         MaterialDesignBody1TextBlock#���  5"      Insert Credentials5$      5%      5&      �� 5'   
   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 User��6   $��1��5(      $��15q�5)      $	��200��5*      ��5+      �� *User+G� 5,   
   -   5        $��PasswordText��50      $ Password��5,      $��2��5-      $��15q�5.      $	��200��5/      51      �� 52   
   $ 
Horizontal=�6   $��3��53      $��Center=�54      $��Center=�55      .+�56      �� $��15q�57        MaterialDesignFlatButton#���  58      ��59      �� *CancelCommand+.��63   Cancel5;      �� 5<      $��3��6    �� 	IsDefault . $��15q�5>      #���  5?      ��5@      �� *	OkCommand+.��6/   OK5B      5C      5E      5F      