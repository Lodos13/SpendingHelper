   M S B A M L   `   `   `  ����B  >UiLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:ScottLogic.Controls.PieChartScottLogic.Controls.PieChart  5      )    #ScottLogic.Controls.PieChart.Legend   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      8c*clr-namespace:ScottLogic.Controls.PieChart     5         MaterialDesignBody#���   5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      $	��300��5      $	��300��6     � 	Resources  5
      2   ,ScottLogic.Controls.PieChart.LegendConverter  legendConverter2   ,ScottLogic.Controls.PieChart.ColourConverter  colourConverter �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels �� Triggers P� Property	 P� Value
 �� 
TargetName �� Color��%�  &	       &	      '�� &       5   
   6     5   
   6   �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ border��6!   
������ $��1q�6K   ._�5      �� # z 6/   #��z��6k   #��z��6�   5       5      P� 5       �@��
IsSelected	 . . �5   "   �� $
 border��6@    � �� 5   &   �� 5   &   $ Black��66   5   #   5      5      5!      5"      5#      .��5%      � .+�5&   
   ��  �� Orientation$ Vertical=�6   .+�5'      ��  �� TextAlignment$ Center=�6   $��1��6/   $��20(�6?   $
��Bold"�6M   ��5(      ��  �� Path$3 -(c:PieChartLayout.PlottedPropertyDisplayName)� �� RelativeSource ��  �� AncestorType)   #ScottLogic.Controls.PieChart.Legend"  �� 5+      -   5        $��legend��5+   �   $	��170��6   $��Stretch=�6#   $��0q�6I   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6]   $��0q�6�   ��6�   �� ��5,      �� 5-      .R�5.      � $��Stretch=�6   $��3q�6=    � ColumnDefinitions 52      �� 53   "   $��20 �63   �� 54   "   $	��120 �63   �� 55   "    � RowDefinitions 57      �� 58   "   .+�5<      �� $��0��6(   $��13��68   $��13��6C   
 � Tag 6O   �� ��5=   (   ��  �� *Self+ �� 	Converter# [ �� 5@      $��1��6(   $��3q�68   ��6C   �� $ Title�5G      5H      5J      5K      5L      5M      