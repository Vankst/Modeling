   M S B A M L   `   `   `  ����C  ?Modeling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Modeling.WindowsModeling.Windows  5   	   )    #Modeling.Windows.generateSparWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Modeling.Windows     5   	     � Title$)  #Создание лонжерона��5   	   $	��675��6$   $	��500��61    � 
ResizeMode$ NoResize=�6=    � WindowStartupLocation$ CenterScreen=�6T    � Icon$ /Windows/logo.png��6y   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��125 �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#    5      �� 5      $��* �6    .+�5      � $��0��6    5      �� 5      $��* �6$   �� 5      $��* �6$   .+�5      �� -   5         ��Name$ cmb_typeSpar��5      $��0��61   $��0��6A   $��25��6N   $	��160��6Z   $��0 0 0 0q�6f   $��Center=�6w    �� SelectedIndex$ 0��6�   .@�5      �� 
 � Tag$ 0��6#   .��5      �� $(��"Н-образный цельный��6$   6A   5       �� 5!      $ 1��6#   .��5"      �� $��Н-образный��6$   69   5#      5$      �� 5%      $��Материал��6   $��25��6+   $��1��68   $��Center=�6E   $��Center=�6i   $	��158��6�   $��0 0 0 30q�6�   $y�Visible=�6�   �� 5&      -   5        	 ��Name$	 tb_selectedMaterial��5&      $��1��68   $��25��6E   $	��125��6Q   $��	0 20 34 0q�6^   $��Center=�6q   
 �� 
IsReadOnly
 . 6�   �� 5'      -   5         ��Name$ 
btn_search��5'   '   $
��🔍��6   $��1��6<   $��25��6I   $��25��6U   $��
135 20 0 0q�6a   ���	#FFABADB36�   5(      �� 5-      -   5         ��Name$ cb_assembly��5-      $��Режим сборки��6-   $��1��6E   $��
130 15 0 0q�6U   $
��Left=�6i   $	��Top=�6�   ���	#FFABADB36�   �� 5/      -   5         ��Name$ lb_countSpar��5/      $&�� Кол-во лонжеронов��6*   $��1��6G   $��25��6W   $	��125��6c   $��
125 0 0 25q�6p   $y�	Collapsed=�6�   $
��Left=�6�   �� 50      -   5        $	 tb_countSpar��50      $��1��6-   $��25��6=   $	��115��6I   $��
0 0 125 24q�6U   $y�	Collapsed=�6i   $��Center=�6�   6�   �� 52      -   5        $ lb_stepMissing��52      $��Шаг отсутпа��6,   $��25��6C   $	��125��6O   $��1��6[   $��125 82 0 18q�6k   $y�	Collapsed=�6�   $
��Left=�6�   �� 53      -   5        $	 tb_stepMissing��53      $��1��6/   $��25��6?   $	��115��6K   $��
0 64 125 0q�6W   $y�	Collapsed=�6k   $��Center=�6�   6�   54       � 58   
   -	   5          �Name$ FrmMain��58      $��1��6!     � NavigationUIVisibility$ Hidden=�6.   $��2��6N   6b   59      5:      