   M S B A M L   `   `   `  ����  AppTimer    AppTimer.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   D @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5   	   0  *ModernWpf.Controls.Primitives.WindowHelper    UseModernWindowStyle$
  True��5   	    � Title$ 
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   .+�5   
   � $��Center=�6   $��Center=�6,    � ColumnDefinitions 5      �� 5      $��* �6#   6/   �� 5      $
��Auto �6#   62   .+�5      �� -   5         ��Name$ ActiveAppsList��5      $��5q�60   $	��400��6;   �� 5      -   5        $��1��5      $��5q�6@   .��6K   Select6S   5      )  #ModernWpf.Controls.SimpleStackPanel  5    
   $��1��6   .+�5!      �� -   5         ��Name$ AppName��5!      $��24(�6%   $��Center=�63   $��Center=�5"      5#      �� 5$      -   5        $ 	TimerText��5$      $��24(�6'   $��Center=�65   $��Center=�5%      5&      5'      � 5)   
   $��2��6   $��Center=�6   $��Center=�69    5*      �� 5+      $��* �6#   6/   �� 5,      $��* �6#   6/   .+�5/      �� -   5         ��Name$ ResetTimerButton��5/      $��0 0 5 0q�6P   .��6a   Reset Timer6n   �� 50      -   5        $��1��50      $��5 0 0 0 q�6A   .��6S   Pause6Z   51        53   
   $��3��6   $��Center=�6+   $��Center=�6H   .+�54      �� -   5        $ 
SaveButton��54   '   $��0 20 0 0q�6   .��6V   Stop & Save6g   55      �� 57   
   -   5         ��Name$ HistoryList��57       $��4��6   $��10 10q�65    �� View 58      �� 59      	 �� AllowsColumnReorder$
	 True��6   .P�5:      �� $��App Name��6%   
 �� DisplayMemberBinding
 67   �� *Name+6_   �� 5;      $��Duration��6%   
 67   �� *
Duration+6c   �� 5<      $
��Date��6%   
 63   �� *	DateAdded+6`   �� 5=      $��Delete��6%    �� Width$	 150��65    �� CellTemplate 5>      �� 5?      .R�5@   "   �� $ DeleteButton��6)   -	   5        
������ 
 � Tag 5@   ?   �� *Id+.��6�   #  ModernWpf.Controls.SymbolIcon     Symbol$ Delete=�6�   6�   6�   5A      5C      5D      5F      5G      5H      