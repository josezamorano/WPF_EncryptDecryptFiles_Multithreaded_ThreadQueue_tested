   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:PresentationLayer.MVVM.ViewsPresentationLayer.MVVM.Views  5   	   U/clr-namespace:PresentationLayer.MVVM.ViewModels!PresentationLayer.MVVM.ViewModels  5   	   1    +PresentationLayer.MVVM.Views.MainWindowView   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;view*clr-namespace:PresentationLayer.MVVM.Views     5   	   E	viewModel/clr-namespace:PresentationLayer.MVVM.ViewModels     5   	   X TFontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	     � Title$  MainWindowView��5
   	   $	��500��5   	   $	��800��5   	   $	��500��5   	   $	��600��5   	    � WindowStartupLocation$ centerScreen=�5   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency$
 True��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	    � 	Resources 5       �� DataType=   7PresentationLayer.MVVM.ViewModels.EncryptFilesViewModel3   -PresentationLayer.MVVM.Views.EncryptFilesView=   7PresentationLayer.MVVM.ViewModels.DecryptFilesViewModel3   -PresentationLayer.MVVM.Views.DecryptFilesView��%�   (��       5      *, +)(�� 2     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5$      .��5&      ��  �� CornerRadius$ 5��6   $��3q�5'       J� Opacity$ 1Q�5(      ��5*   
   �� 5+      	 �� 
StartPoint$		 0,0&�6"   
 �� EndPoint$	
 1,1&�63   .��5,      �    windowBorderOrange_I � Color# [  6    � Offset$ 0Q�6M   � 5-        windowBorderFuchsia_III# [ 6   $
 0.75Q�6P   � 5.        windowBorderBlue_V# [ 6   $ 1Q�6K   5/      ._�53   
   �   panelBaseColorDark_V#��[ 54       � ColumnDefinitions 56      �� 57      $��1* �6#   �� 58      $��1* �6#    � RowDefinitions 5:      �� 5<      $��35 �6    �� 5>      $��55 �6    �� 5@      $��* �6    .+�5D      �� -   5         ��Name$ panelControlBar��5D      $��0��5E      $��0��5F      $��2��5G       �� Orientation$ 
Horizontal=�5H      $��RightToLeft=�5I      
������ $��0,0,0,0q�5K      .+�5N      �� -   5         ��Name$ btnClose��5N        controlButton#��[ 5O   !     ButtonCloseOrange_V
 � Tag# [ 5P   !    K� ZIndex$ 1��5R      .��5T      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Xmark=�5U        controlButtonIcon#��[ 6&   5V      �� 5W      -   5        $ btnMaximize��5W      #��[ 5X        ButtonMaximizeFuchsia_IV# [ 5Y      .��5[        $ Square=�5\      #��[ 6'   5]      �� 5_      -   5        $ btnMinimize��5_      #��[ 5`        ButtonMinimizeBlue_IV# [ 5a      .��5d        $ WindowMinimize=�5e      $��12��6/   #��[ 5f      5g      5h      �� 5k      $ 
Horizontal=�5l      $��1��5m      $��0��5n      $��2��5o      
������ $��leftToRight=�5q      $��Center=�5r      .+�5v      	� -   5         	�Name$ btnEncryptFilesInFolder��5w       	 EncryptionRadioButton#��[	 5x      $	��230��5y      $��50��5z       d� 	IsChecked$
 trueY�5{      ��5|      �� *" OpenChildViewEncryptFilesCommand+.��5~      �� $ 
Horizontal=�6!   $��LeftToRight=�6:   .+�5        $
 Lock=�6'    
 EncryptionIcon#��[
 63   �� 5�      $��Encrypt Files In Folder��6$   $��18(�6C   $��10,0,0,0q�6Q   5�      5�      	� 5�      -   5        $ btnDecryptFilesInFolder��5�        DecryptionRadioButton#��[ 5�      $	��230��5�      $��50��5�      ��5�      �� *" OpenChildViewDecryptFilesCommand+.��5�      �� $ 
Horizontal=�6!   $��LeftToRight=�6:   .+�5�        $ LockOpen=�6'     DecryptionIcon#��[ 67   �� 5�      $��Decrypt Files In Folder��6$   $��18(�6C   $��10,0,0,0q�6Q   5�      5�      5�      �� 5�      $��2��5�      $��0��5�      $��2��5�      ��5�      �� *CurrentChildView+5�      5�      5�      5�      