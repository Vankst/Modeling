   M S B A M L   `   `   `  ����C  ?Modeling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Modeling.WindowsModeling.Windows  5   	   %    Modeling.Windows.searchMaterial   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Modeling.Windows     5   	     � Title$%  Поиск материалов��5   	   $	��200��6"   $	��300��6/    � 
ResizeMode$ NoResize=�6;    � WindowStartupLocation$ CenterScreen=�6Q    � Icon$ /Windows/logo.png��6v   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��2* �6   �� 5      .+�5   
   �� -   5        $��1��5      $��Задать��6!   ���	#FFE0E0E0}�. $��1,0,0,0q�6^   ��5      �� 5       �� EndPoint$	 1,3&�6&    �� 
StartPoint$	 0,3&�65    � SpreadMethod$	 Pad=�6F    � MappingMode$ RelativeToBoundingBox=�6Y   	 �� Opacity$
	 0.53Q�6}   
 �� RelativeTransform
 5      V� 5      .~�5      ��  �� CenterY$	 0.5Q�6-    �� CenterX$	 0.5Q�6;   �� 5       �� CenterX$	 0.5Q�6,    �� CenterY$	 0.5Q�6:   �� 5       �� Angle$ 	-1840.236Q�6.    �� CenterX$	 0.5Q�6@    �� CenterY$	 0.5Q�6N   T� 5      5      .��5      �  � Color$ White��6#    � Offset$ 0Q�61   � 5      $ 	#FFA9FFFF��6#   $ 1Q�65   5      5      �� 5   
   $��
Поиск��6   $��0��6    $��
 0 0 75 75q�60   $��25��6D   $��75��6P   �� 5    
   -   5         ��Name$ tb_searchMaterial��5       $	��150��6-   $��20��69   $��0 0 0 35q�6E   $��0��6W   6�   �� 5!   
   -   5         ��Name$ cmb_material��5!      $	��150��6)   $��20��65   $��0 35 0 0q�6A   $��0��6S   6�   5#      5$      