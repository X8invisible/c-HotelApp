   M S B A M L   `   `   `  ����H  DNapierHoliday, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:NapierHolidayNapierHoliday  5   	       NapierHoliday.WindowNewCu   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:NapierHoliday     5   	     � Title$  Napier Holiday��5   	   $��282.542��6    $��504.203��61   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��57* �6   �� 5      $	��67* �6   .+�5   
   �� -   5         ��Name$ txtName��5      $
��Left=�6#   $��25��6>    �� TextWrapping$
 Wrap=�6J   $	��Top=�6^   $	��142��6v   $��	70,63,0,0q�6�   �� 5   
   -   5        $ txtAddr��5      $
��Left=�6#   $��25��6>   $
 Wrap=�6J   $	��Top=�6^   $	��142��6v   $��	70,93,0,0q�6�   �� 5   
   -   5         ��Name$ lblName��5      $
��Name��6!   $
��Left=�60   $��9,63,0,0q�6K   $	��Top=�6]   $��56��6u   $��25��6�   �� 5   
   -   5        $ lblAddr��5      $��Address��6!   $
��Left=�63   $��9,93,0,0q�6N   $	��Top=�6`   $��25��6x   $��56��6�   �� 5   
   -   5         ��Name$ 	btnCreate��5      $��Create Customer��6$   $
��Left=�6>   $��
58,137,0,0q�6Y   $	��Top=�6m   $	��105��6�   $��30��6�   6�   �� 5   
   $T��NCreate a customer or double click one from the list to edit and make a booking��6   $
��Left=�6i   $��	10,10,0,0q�6�   $	��Top=�6�   $��2��6�   $	��446��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgridCustomers��5      $��1��6+   $
��Left=�6;   $	��201��6V   $��	10,41,0,0q�6c   $	��Top=�6v   $	��248��6�      AutoGenerateColumns .     
IsReadOnly . 	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Customer No.��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
CustomerNo+6Z     5      $

 Name��6%   $ * 6L    63   �� *Name+6L   5      5      5      