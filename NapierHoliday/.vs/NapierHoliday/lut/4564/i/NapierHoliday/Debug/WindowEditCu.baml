   M S B A M L   `   `   `  ����H  DNapierHoliday, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:NapierHolidayNapierHoliday  5   	        NapierHoliday.WindowEditCu   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:NapierHoliday     5   	     � Title$  WindowEditCu��5   	   $��280.542��6   $��452.203��6/   .��5	      � .+�5
   
   �� -   5         ��Name$ txtName��5
      $
��Left=�6#   $��25��6>    �� TextWrapping$
 Wrap=�6J   $	��Top=�6^   $	��109��6v   $��	71,51,0,0q�6�   �� 5   
   -   5        $ txtAddr��5      $
��Left=�6#   $��25��6>   $
 Wrap=�6J   $	��Top=�6^   $	��109��6v   $��	71,81,0,0q�6�   �� 5   
   -   5         ��Name$ lblName��5      $
��Name��6!   $
��Left=�60   $��	10,51,0,0q�6K   $	��Top=�6^   $��56��6v   $��25��6�   �� 5   
   -   5        $ lblAddr��5      $��Address��6!   $
��Left=�63   $��	10,81,0,0q�6N   $	��Top=�6a   $��25��6y   $��56��6�   �� 5   
   -   5         ��Name$ btnEdit��5      $��Save Changes��6"   $
��Left=�69   $��
45,131,0,0q�6T   $	��Top=�6h   $��97��6�   $��27��6�   $��Center=�6�   $��Center=�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgridBookings��5      $	��157��6*   $��198,16,10,0q�67   $	��Top=�6L      AutoGenerateColumns .     
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Booking No.��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 80 6`   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *BookingRefference+6`     5      $	 Arrival��6%   $
 * 6m    66   �� *	Arrival+ �� StringFormat$ 
dd-MM-yyyy��6m     5      $	 	Departure��6%   $
 * 6q    68   �� *	Departure+$ 
dd-MM-yyyy��6q   5      �� 5   
   -   5        $ btnDel��5      $��Delete Booking��6!   $��
0,180,10,0q�6:   $	��Top=�6N   $��27��6f   $��Right=�6�   $��90��6�   �� 5   
   -   5        $ btnNew��5      $��New Booking��6!   $
��Left=�67   $��198,180,0,0q�6R   $	��Top=�6g   $��90��6   $��27��6�   6�   �� 5   
   -	   5        $ btnArrowBack��5      $��10,14,409,204q�6'   ��5      �� 5      .R�5      �� $��Resources/backArrow.png��6   5      5      �� 5   
   -
   5        $ btnDelCu��5      $��Delete Customer��6#   $
��Left=�6=   $��
45,180,0,0q�6X   $	��Top=�6l   $��97��6�   $��27��6�   6�   5       5!      