   M S B A M L   `   `   `  ����
  Sudoku    Sudoku.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F     � Title$  Sudoku��5   	   $	��420��6   $	��400��6%   .��5      � -   5         �Name$ MainGrid��5      $	��400��6   $	��400��6*   $	��Top=�66   $
��Left=�6N    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5	      $
��Auto �6   6-    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6/   .+�5   
   o� -   5        $��
SudokuMenu��5   -   $��0��6   $��0��6   $��Center=�6?   $��20��6\   $	��Top=�6h   $	��400��6�   .@�5      m� $
��Game��6   .@�5      m� -   5        $	��New��5      $
��Left=�6(   $	��200��6C    m� InputGestureText$ Ctrl+N��6O   6i   m� 5      -   5        $��Solve��5      $
��Left=�6*   $	��200��6E   6Q   m� 5      -   5        $
��Exit��5      $
��Left=�6)   $	��200��6D   $ Alt+F4��6P   6j   5      5      5      5      