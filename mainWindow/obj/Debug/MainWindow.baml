   M S B A M L   `   `   `  ����E  AmainWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:mainWindow
mainWindow  5   	       mainWindow.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'dcclr-namespace:mainWindow     5   	   W SOxyPlot.Wpf, Version=2014.1.546.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5   	     � Icon$  Images\favicon.ico��5	   	    � WindowStartupLocation$ Manual=�5
   	    � Left$ 0��5   	   
 � Top$ 0��5   	    	  Title � Title# �   5   	   $	��800��6)   $
��1400��66    � DataContext 5        mainWindow.ViewModel  5   
   6    � 	Resources 5       �� 
TargetType �� Property	 �� Value��%^   '��       �� 5   
   " ��6   .�5      ��  � ��$	 	10,0,10,0q�6'   6;   5      5      .��5      � -   5        
 �Name$

 Main��5      .+�5   
   �� .@�5      ��   ModelTab#���  6   .��5      �  � ColumnDefinitions 5       �� 5!      $	��615 �6+   �� 5"      $
��400* �6+   .+�5&      �� -   5        $��	BorderOne��5&      $��0��6.   $��3q�6?   
�������._�5'      �  5(      �� 5)   "   $	��320 �63   6A   �� 5*   "   65    � RowDefinitions 5,      �� 5-   "   62   �� 5.   "   62   .+�50         mainWindow.Container  $��LeftToRight=�6+   $��0��6G   $��0��6W   .+�51   "   ��   ModelParamerts#���  6(   6U      mainWindow.LabelTextBox  52   "      mainWindow.LabelTextBox   ManipulationCompleted$' !UIElement_OnManipulationCompleted��6�     SuseptibleFraction   Label# �  62   $
��Auto��6�      Text 6_   �� *Data.SuseptipleFaction+ �� StringFormat$ 0.####��6�     53   "   $' !UIElement_OnManipulationCompleted��6�     InfectedFraction# �  62   $
��Auto��6�    6]   �� *Data.InfectedFaction+$ 0.####��6�     54   "   $' !UIElement_OnManipulationCompleted��6�     Beta# �  62   $
��Auto��6�    6Q   �� *	Data.Beta+$ 0.####��6�     55   "     Mu# �  62   $
��Auto��6�    6O   �� *	Data.Mu+$ 	0.#######��6�     56   "    	 Gamma# �  62   $
��Auto��6�    6R   �� *
Data.Gamma+$ 0.####��6�     57   "     R0# �  62      ReadOnly .    TextBoxBackGround
 �����$
��Auto��6�    6O   �� *	Data.R0+$ 0.####��6�     59   "    	 P# � 	 62   $
��Auto��6    6N   �� *Data.P+$ 0.####��6     5:   "    
 TimeStep# � 
 62   $
��Auto��6�    6V   �� *	Data.Step+$ 0.####��6�     5;   "     	TimeLimit# �  62   $
��Auto��6�    6V   �� *Data.TimeLimit+$ 0.####��6�   �� 5<   "   6*   �� 5=   "   -   5          
ChangeLang#���  5=   H   $	��200��6o   6}   �� 5>   "   6*   �� 5?   "     Example1Title#���  6F   $	��200��6p   ��6)   �� *
Example1+6~   �� 5@   "   6*   �� 5A   "     Example2Title#���  6F   $	��200��6p   ��6)   �� *
Example2+6~   5E        5F      $��1��6+   $��0��6;   .+�5G   "   �� $��26��6(   66   �� 5H   "   -   5        $��44��5H   )    � Minimum$ 0Q�65    � Maximum$ 1Q�6A   $��Center=�6�    J� RenderTransformOrigin$ 0.499,0.716&�6�   $
��10,0q�6�    � Value 6M   �� *Data.SuseptipleFaction+6�   �� 5I   "   $��9��6(   65   �� 5J   "   -   5        $ 0Q�5J   )   $ 1Q�65   $��30��6�   $
��10,0q�6�    6A   �� *	Data.Beta+6�   �� 5K   "   -   5        $ 0.001Q�5K   )   $	 0.6Q�69   $��26��6�   $
��10,0q�6�    6G   �� *	Data.Mu+6�   �� 5L   "   -   5        $ 0Q�5L   )   $ 1Q�65   $��24��6�   $
��10,0q�6�    6A   �� *
Data.Gamma+6�   �� 5M   "   $��28��6O   ��6(   �� *Data.R0Assumption+6]   �� 5N   "   -   5        $ 0Q�5N   )   $ 1Q�65   $��26��6�   $��0,0,10,0q�6�    6A   �� *Data.P+6�   �� 5O   "   -	   5        $
 0.01Q�5O   )   $ 1Q�68   $��27��6�   $
��10,0q�6�    6E   �� *	Data.Step+6�   �� 5P   "   -
   5        $ 1Q�5P   )   $ 50Q�65   $��33��6�   $
��10,0q�6�    6B   �� *Data.TimeLimit+6�   5Q      �� 5S      $��1��6%   $��2��62   $��2q�6G   
�������._�5T   "   �� .+�5U   &   ��   
EventLabel#���  6,   6U   �� 5V   &   $
��Wrap=�6O   $
��10,0q�6c   ��60   �� *Data.HelpText+6t   5W   #   5X      5Y      5[      �� 5]      $��1��6   $��3q�6/   
�������._�5^        OxyPlot.Wpf.PlotView  -   5          Name$ Plot1��5^   '      
Controller 66   �� *CustomPlotController+   Model 6b   �� *	PlotModel+5_      5`      5a      �� 5c        	TheoryTab#���  6   .��5d      Z� -   5         Z�Name$ TheoryDocumentViewer��5d   !   5f      5g      �� 5i        AboutTab#���  6   .��5j      Z� -   5        $ GuideDocumentViewer��5j   !   5l      5m      5q      5r      5t      