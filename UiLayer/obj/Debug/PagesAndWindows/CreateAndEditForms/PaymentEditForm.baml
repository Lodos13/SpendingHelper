   M S B A M L   `   `   `  ����B  >UiLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UiLayerUiLayer  5   	       UiLayer.PaymentEditForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:UiLayer     5   	   S OMaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  AddPaymentForm��5	   	   $	��220��6    $	��300��6-   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��Center=�6!   Date6B   �� 5   
   $��1��6   $��Center=�6!   
Category6F   �� 5   
   $��2��6   $��Center=�6!   SubCategory6I   �� 5   
   $��3��6   $��Center=�6!   Sum6A   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��
DatePicker��5      $��0��5      $��1��5      $	��100��5       $��Center=�5!      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	Pick Date��5"       (  $MaterialDesignFloatingHintDatePicker#��[  5#      5$      �� 5&   
   -   5        $��CategoryBox��5'       �� DisplayMemberPath$ Title��5*      $��1��5+      $��1��5,      $ Category��5-      $��Center=�5.      ��5(      �� *
Categories+ �� SelectedItem 5)      �� *
Category+ �� Mode$ TwoWay=�5/      �� 51   
   -   5        $��SubCategoryBox��52      $ Title��55      $��2��56      $��1��57      $ Sub Category��58      $��Center=�59      ��53      ��  �� ElementName$ CategoryBox�� �� Path$  SelectedItem.SubCategories� 54      �� *SubCategory+$ TwoWay=�5:      �� 5<   
   -   5        $��SumBox��5=      $��3��5>      $��1��5?      $	 Sum��5@       % !MaterialDesignFloatingHintTextBox#��[ 5A      $	��Top=�5B      .��6%   5C      � 5E   
   $��4��6   $��2��6!   $��Center=�67   $��	0,15,0,15q�6T   .+�5F      �� -   5        	 �� 	IsDefault	 . $��60��5F   ;   $��0,0,10,0q�6I   .��6[   OK6_   �� 5G      
 �� IsCancel
 . $��60��6%   .��63   Отмена6;   5H      5J      5K      