   M S B A M L   `   `   `  ����B  >UiLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:UiLayer.ControlsUiLayer.Controls  5      K*clr-namespace:ScottLogic.Controls.PieChartScottLogic.Controls.PieChart  5      +    %UiLayer.Controls.StatisticShowControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:UiLayer.Controls     5      :pie*clr-namespace:ScottLogic.Controls.PieChart     5        � 	Resources  5      �� 5   
    �� Type" ��6      Keybrushes  .s�5      ��  �� Color$ #9F15C3��6   �� 5      $ #FF8E01��6   �� 5      $ #339933��6   �� 5      $ #00AAFF��6   �� 5      $ #818183��6   �� 5      $ #000033��6   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   .+�5    
   &   System.Windows.Controls.DataGrid  -   5          Name$ CategoriesDataGrid��5          AutoGenerateColumns .     
IsReadOnly . $��0��5$      $��0��5%      ��5#      ��    Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Spending category��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6@   �� *Title+  5(      $	 Total spending��6%   
 6=   �� *SpendedForAllTimes+  5)      $	 Spending this month��6%   
 6B   �� *SpendedThisMonth+  5*      5,        5.   
   -   5        $ SubCategoriesDataGrid��5.      $ �� IsSynchronizedWithCurrentItem$
 TrueY�50       .   . $��1��53      $��0��54      ��5/      ��  �� ElementName$ CategoriesDataGrid�� �� Path$  SelectedItem.SubCategories� 55        56      $	 Spending subcategory��6%   
 6C   �� *Title+  57      $	 Description��6%   
 6:   �� *Description+  58      $	 Total spending��6%   
 6=   �� *Sum+  59      $	 Spending this month��6%   
 6B   �� *SumThisMonth+5;      1   +ScottLogic.Controls.PieChart.PieChartLayout  5=   
   $��0��6   $��1��5>         PlottedProperty$	 Sum��5@      !   PlottedPropertyDisplayName$ Общая сумма��5A       � DataContext 5?      �� $ CategoriesDataGrid��$  SelectedItem.SubCategories� �� UpdateSourceTrigger$ PropertyChanged=�   ColorSelector 5B      8   2ScottLogic.Controls.PieChart.IndexedColourSelector  5C        brushes   Brushes# [ 6,   5E        5G   
   $��1��6   $��1��5H      $ SumThisMonth��5J      $  Сумма за месяц��5K       5I      �� $ CategoriesDataGrid��$  SelectedItem.SubCategories�$ PropertyChanged=� 5L        5M      # [ 6,   5O      5Q      5R      