   M S B A M L   `   `   `  ����B  >UiLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UiLayerUiLayer  5   	   !    UiLayer.SubCategoryEditForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:UiLayer     5   	   S OMaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  SubCategoryEditForm��5	   	   $	��190��6%   $	��300��62   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��Center=�6!   
Category6F   �� 5   
   $��1��6   $��Center=�6!   Sub category title6P   �� 5   
   $��2��6   $��Center=�6!   Description6I   �� 5   
   -   5        $��CategoryBox��5       �� DisplayMemberPath$ Title��5      $��0��5      $��1��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Category��5       $��Center=�5!      ��5      �� 5"      �� 5$   
   -   5        $��SubCategoryTitleBox��5%      $��1��5&      $��1��5'      $ Title��5(       %  !MaterialDesignFloatingHintTextBox#��[  5)      $	��Top=�5*      .��6%   5+      �� 5-   
   -   5        $��DescriptionBox��5.      $��2��5/      $��1��50      $ Description��51      #��[  52      $	��Top=�53      .��6%   54      � 57   
   $��3��6   $��2��6!   $��Center=�67   $��	0,15,0,15q�6T   .+�58      �� -   5         �� 	IsDefault . $��60��58   ;   $��0,0,10,0q�6I   .��6[   OK6_   �� 59       �� IsCancel . $��60��6%   .��63   Отмена6;   5:      5<      5=      