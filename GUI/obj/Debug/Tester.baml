   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5   J    GUI   
GUI.Tester   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   J     � Title$  Graph��6	   $
��1109��6   $	��855��6%   
����ح�.��5      �� $
��Auto=�6   $
��Auto=�68   $��
0,-28,0,19q�6[   .��5	   
   � $	��920��6    � ColumnDefinitions 5
      �� 5      $
��520* �6#   �� 5      $
��327* �6#   .+�5      >   8System.Windows.Controls.DataVisualization.Charting.Chart  -   5          Name$ columnChartA��5   a   $	��262��6%   $
��Left=�62   $��
33,0,0,555q�6M      Title$6 0Avarage price of commodities (last 1000 Trades):��6w   $��Bottom=�6�   $	��360��6�   $��2��6�      Series. 5      E   ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��6/   ?   9System.Windows.Controls.DataVisualization.Charting.Series   Title$ Avarage��6J      IndependentValuePath$	 Key��6Z      ItemsSource 6u   �� 5        5      -   5        $	 Hot��5   %   $+ %'HOT' commodities (last 1000 trades):��62   $	��Top=�6`   $��38,104,43,0q�6x   $	��262��6�   $��1��6�   . 5      B   <System.Windows.Controls.DataVisualization.Charting.PieSeries  $ Value��6,   $	 Key��6G   	   IsSelectionEnabled	 . $��10q�6�    6b   �� 6�   5        5      -   5        $ columnChartG��5   %   $&  Ask and Bid of each commodities:��6;   $	��Top=�6d   $��33,396,454,0q�6|   $	��262��6�   $��2��6�   . 5        -   5        $
��askC��5   /   $	 ask��6;   $ Value��6G   $	 Key��6b   	 .  6}   �� *[1]+
 �� Mode$
 oneWay=�6�     5      -   5        $
��bidC��5   /   $	 bid��6;   $ Value��6G   $	 Key��6c   	 .  6~   �� *[0]+$
 oneWay=�6�   5        5      -   5        $	 Cou��5   %   $ Commodities Of User:��62   $	��Top=�6O   $��38,394,43,0q�6g   $	��262��6|   $��1��6�   . 5        $ Value��6-   $	 Key��6H   	 .  6c   �� 6{   5      �� 5"      $��Statistics Graphs:��6   $
��Left=�61   $��
307,29,0,0q�6L   $	��Top=�6`   $��35(�6x   $	��271��6�   $��50��6�   $��2��6�   �� 5#      -   5        $��Refresh��5#      $��1��6'   $
��Left=�67   
�������$��30(�6j   $��115,768,0,0q�6x   $	��Top=�6�   $	��184��6�   $��80��6�   6�   �� 5$      $"��to search MAX and MIN price:��6   $��16(�6;   $
��Left=�6J   $��68��6e   $��
39,752,0,0q�6q   $	��Top=�6�   $	��230��6�   �� 5%      $ �� Enter TOP trades number  ��6   $��16(�69   $
��Left=�6H   $��60��6c   $��
33,727,0,0q�6o   $	��Top=�6�   $	��236��6�   �� 5&      -   5        $��Se��5&   �   $
��Left=�6   $��46��61   $��272,735,0,0q�6i    �� TextWrapping$
 Wrap=�6~   $	��Top=�6�   $	��140��6�   �� 5'      
���   �$��1q�6)   $
��Left=�6=   $	��214��6X   $��
33,707,0,0q�6e   $	��Top=�6y   $	��391��6�   ._�5(      �� -	   5        $��Search��5(      
�������$
��Left=�6A   $��25(�6\   $��58��6k   $��
64,123,0,0q�6w   $	��Top=�6�   $	��222��6�   5)      5*      5+      5-      