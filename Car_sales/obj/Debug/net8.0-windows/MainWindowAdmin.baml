   M S B A M L   `   `   `  ����D  @Car_sales, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Car_sales	Car_sales  5   	       Car_sales.MainWindowAdmin   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Car_sales     5   	     � Title$K  EГлавное окна администаратора системы��5   	   $	��450��66   $	��800��6C   .��5	      � .+�5
   
   �� -   5        $A��;Поcмотреть список пользователей��5
      $
��Left=�6;   $��
27,283,0,0q�6V   $	��Top=�6j   $��40��6�   $	��200��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��CarsDataGrid��5      $��	400,0,0,0q�6'      AutoGenerateColumns$
 True��6:   �� 5   
   $%��Марка автомобиля��6   $
��Left=�6+   $��	27,91,0,0q�6F   $	��Top=�6Y   �� 5   
   $��Дата начала��6   $
��Left=�6&   $��
27,133,0,0q�6A   $	��Top=�6U   �� 5   
   $!��Дата окончания��6   $
��Left=�6)   $��
27,172,0,0q�6D   $	��Top=�6X   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��EndDatePicker��5      $
��Left=�6*   $��168,173,0,0q�6E   $	��Top=�6Z   $��25��6r   $	��200��6~     5   
   -   5          Name$ StartDatePicker��5      $
��Left=�6.   $��168,134,0,0q�6I   $	��Top=�6^   $��25��6v   $	��200��6�   �� 5   
   -   5        $��BrandComboBox��5      $
��Left=�6(   $��
168,91,0,0q�6C   $	��Top=�6W   $	��200��6o   $��25��6{   �� 5   
   -   5        $��
Поиск��5      $
��Left=�6!   $��
27,232,0,0q�6<   $	��Top=�6P   $��40��6h   $	��200��6t   6�   �� 5   
   $:��4Окно администратора системы��6   $
��Left=�66   $��	66,30,0,0q�6Q   $	��Top=�6d   $
��Auto��6|   $��16(�6�   $��Heavy"�6�   5      5      