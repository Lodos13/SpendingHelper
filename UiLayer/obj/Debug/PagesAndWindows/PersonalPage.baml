   M S B A M L   `   `   `  ����B  >UiLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UiLayerUiLayer  5   	   3clr-namespace:UiLayer.ControlsUiLayer.Controls  5   	       UiLayer.PersonalPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:UiLayer     5   	   3controlsclr-namespace:UiLayer.Controls     5   	      MaterialDesignBody#���   5	   	   $��Regular"�5
   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper N� 
Background# �  5   	     MaterialDesignFont N� 
FontFamily# �  5   	   $��Stretch=�5   	   $��Stretch=�5   	    N� Title$ PersonalPage��5   	    N� WindowTitle$ Personal page��6    � 	Resources 5        InFamilyObject �� 
TargetType  MaterialDesignRaisedButton �� BasedOn	 �� Triggers
 �� Binding �� Value �� Property �� Value  NotInFamilyObject��%@  &	       7  &	     7  �� 5   
   " ��6'   8   	 5      �� 5      
 6   �� *
IsInFamily+ True6=   .a�5      ��  � y�$ Visible=�6;   5      �� 5      
 6   �� *
IsInFamily+	 False6=   .a�5      ��  � y�$ 	Collapsed=�6;   5      5      �� 5   
   " ��6*   8   	 5       �� 5!      
 6   �� *
IsInFamily+ True6=   .a�5"      ��  � y�$ 	Collapsed=�6;   5#      �� 5$      
 6   �� *
IsInFamily+	 False6=   .a�5%      ��  � y�$ Visible=�6;   5&      5(      5)      .��5+      � .+�5,   
   �� .+�5-      ��  �� Orientation$ 
Horizontal=�6   .+�5.      �� $��Name - ��6   �� 5/      ��6   ��  �� Path$
 Name�50      �� 52      $ 
Horizontal=�6   .+�53      �� $��	Family - ��6   $��Center=�54      �� 55      $��Center=�56      $��8q�57      ��55      �� *
FamilyName+ J� InputBindings 58      d� 59       d� MouseAction$ 	LeftClicke�5:   '    �� Command 59   '   �� *RedirectToMyFamilyPageCommand+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �5:   '   5<      �� 5=      #��[ 6   $��Create a family��5?      ��5>      �� *CreateFamilyCommand+5?      �� 5@      #��[ 6   $��Join to a family��5B      ��5A      �� *JoinFamilyCommand+5B      �� 5C      #��[ 6   $��Leave the family��5E      ��5D      �� *LeaveFamilyCommand+5E      5F      �� 5H      $ 
Horizontal=�6   ��5I      �� 5J      	 5K      �� 5L      
 6*   �� *IsBudgetExceeded+ True6O   .a�5M   "   ��  � ��
 �  ��6J   5N      �� 5O      
 6*   �� *IsBudgetExceeded+	 False6O   .a�5P   "   ��  � ��
 � � �6J   5Q      5S      .+�5U      �� $��	Budget - ��6   �� 5V      ��6   �� *Budget+5W      �� 5Y      $ 
Horizontal=�6   .+�5Z      �� $��Spended this month - ��6   �� 5[      ��6   �� *SpendedThisMonth+5\      �� 5^      #���  6   #���  5_      $��Stretch=�5`      $��Stretch=�5a      .@�5b      �� $��
Categories��6   .��5c      �� $ Vertical=�6!   .+�5e      �� $ 
Horizontal=�6%   .+�5f      �� $��Add new sub category��6%   ��5g   !   �� *AddNewSubCategory+�� 5h      $��Edit sub category��6%   ��5i   !   �� *EditSubCategory+��5j   !   ��  �� ElementName$ CategoryListView��$&  SelectedItem.SelectedSubCategory��� 5k      $��Delete sub category��6%   ��5l   !   �� *DeleteSubCategory+��5m   !   �� $ CategoryListView��$&  SelectedItem.SelectedSubCategory�5n      �� 5p      -   5        $��CategoryListView��5p   #   $	��500��5q   #   ��5r   #   �� *
Categories+��5s      �� 5t   "   .R�5u   &   Q� ��63   �� *Title+.@�5v   *   ��  �� DisplayMemberPath$ Title��5x   3   ��5v   3   �� *SubCategories+ �� SelectedItem 5w   3   �� *SelectedSubCategory+5y   +   5z   '   5{   #   5}      5~      5      �� 5�      $��	Spendings��6   .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $ Vertical=�6!   .+�5�      �� $ 
Horizontal=�6%   .+�5�      �� $��Add new spending��6%   ��5�   !   �� *AddNewPaymentCommand+�� 5�      $��Edit spending��6%   ��5�   !   �� *EditPaymentCommand+��5�   !   �� $ SpendingsControl��$ SelectedPayment��� 5�      $��Delete spending��6%   ��5�   !   �� *DeletePaymentCommand+��5�   !   �� $ SpendingsControl��$ SelectedPayment�5�      *   $UiLayer.Controls.SpendingShowControl  5�      -   5          Name$ SpendingsControl��5�   !    � DataContext 5�   !   �� *
Payments+5�      +   %UiLayer.Controls.StatisticShowControl  5�       5�      �� *
Categories+5�      5�      5�      5�      5�      5�      