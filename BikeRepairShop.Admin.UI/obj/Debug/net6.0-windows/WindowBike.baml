   M S B A M L   `   `   `  ����R  NBikeRepairShop.Admin.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:BikeRepairShop.Admin.UIBikeRepairShop.Admin.UI  5   	   (    "BikeRepairShop.Admin.UI.WindowBike   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:BikeRepairShop.Admin.UI     5   	   -   5          � Title$
  Bike��5   	   $	��200��6   $	��350��6#    � 
ResizeMode$ NoResize=�6/   .��5	      � $��5q�6    � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��Customer��6   $��0��6#   $��0��60   �� 5   
   $��ID��6   $��1��6   $��0��6*   �� 5   
   $��	Bike Type��6   $��2��6$   $��0��61   �� 5   
   $��Purchase Cost��6   $��3��6(   $��0��65   �� 5   
   $��Description��6   $��4��6&   $��0��63   �� 5   
   -   5        $��
CBCustomer��5      $��0��6%   $��1��62   $y�Hidden=�6B   �� 5   
   -   5        $��
TBCustomer��5      $��0��6$   $��1��61   $y�Hidden=�6A    �� 
IsReadOnly$
 True��6U   �� 5   
   -   5        $
��TBId��5      $��1��6   $��1��6+   �� 5   
   -   5        $��
CBBikeType��5      $��2��6%   $��1��62   �� 5   
   -   5        $��TBPurchaseCost��5      $��3��6(   $��1��65   �� 5    
   -   5        $��TBDescription��5       $��4��6'   $��1��64   �� 5!   
   -   5         ��Name$ CancelBikeButton��5!      $��Cancel��6+   $��5��6<   $��0��6I   6Y   �� 5"   
   -	   5        $ SaveBikeButton��5"      $
��Save��6)   $��5��68   $��1��6E   6U   5#      5$      