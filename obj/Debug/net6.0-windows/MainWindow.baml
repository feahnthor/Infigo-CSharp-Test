   M S B A M L   `   `   `  ����T  PInfigo-api-sucks-solution, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Infigo_api_sucks_solutionInfigo_api_sucks_solution  5   	   a5clr-namespace:Infigo_api_sucks_solution.MVM.ViewModel'Infigo_api_sucks_solution.MVM.ViewModel  5   	   *    $Infigo_api_sucks_solution.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Infigo_api_sucks_solution     5   	   K	viewModel5clr-namespace:Infigo_api_sucks_solution.MVM.ViewModel     5   	   -   5        $	��588��5	   	   $
��1038��6     � WindowStyle$
  None=�5
   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      ;  5Infigo_api_sucks_solution.MVM.ViewModel.MainViewModel  5   
   .��5      �� ���#272537 �� CornerRadius$ 10��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5      �� 5      $��75 �6    �� 5       .+�5$      �� -   5        $��2��5$      ._�5'      � $��Stretch=�6   .+�5(      �� $
��Left=�6!    �� Orientation$ 
Horizontal=�5)   !   $��	982,0,0,0q�6:   $��38��6M   .+�5*      �� -   5        $��38��5*   !   $��30��6,   $	��╳��5+   !   $��Center=�5,   !   $��	0,0,10,50q�5-   !   
������ $��0q�5/   !   
�������$
��Bold"�51   !   52   !   55      56      58      �� 5:      $��Infigo API Sucks��6   $��Center=�5;      $
��Left=�5<      
�������$��22(�5>      $��20,0,0,0q�5?      �� 5A      $��1��6   .+�5C      	� $
��Home��6   $��50��5D      
�������$��14(�5F         MenuButtonTheme#��[  5G       d� 	IsChecked$
 TrueY�5H      ��5I      �� *HomeViewCommand+	� 5K      $��	Discovery��6   $��50��5L      
�������$��14(�5N      #��[  5O      ��5P      �� *DiscoveryViewCommand+	� 5R      $��Test2��6   $��50��5S      
�������$��14(�5U      #��[  5V      5Y      �� 5[      $	��250��6   $��40��5\      $��Center=�5]      $
��Left=�5^      $��5q�5_      $��1��5`        ModernTextBox#��[ 5a      �� 5d      $��1��6   $��1��5e      $��10q�5f      ��5g      �� *CurrentView+	� 5i      -   5        	 	�Name$	 CsvToJsonRadioButton��5i      $��Convert CSV to JSON��5j      $��1��5k      $��1��5l      $��Right=�5m      
�������$��
0,316,24,0q�5o      $	��Top=�5p      $��25��5q      $	��126��5r      $��11(�5s      $ FalseY�5t      �� 5u      -   5        
 ��Name$
 StartButton��5u      $��Start��5v      $��1��5w      $��1��5x      $��40��5y      $��75��5z      $��
10,0,50,50q�5{      $��Bottom=�5|      $��Right=�5}      
����������	#FF34883D � 	Resources 5�       �� 
TargetType �� Property �� Value��%_   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 10��69   5�      5�      5�      �� 5�      -   5        $
 
TestButton��5�      $��GoToVariant��5�      $��1��5�      $��1��5�      $��40��5�      $��75��5�      $��	713,0,0,0q�5�      $��Center=�5�      $
��Left=�5�      
����������	#FF0056FF$y�Visible=�5�       5�      ��%_   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 10��69   5�      5�      5�      �� 5�      -   5        $
 
StopButton��5�      $
��Stop��5�      $��1��5�      $��1��5�      $��40��5�      $��75��5�      $��10,0,50,100q�5�      $��Bottom=�5�      $��Right=�5�      
�������
���  ��$y�Visible=�5�       5�      ��%_   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 10��69   5�      5�      5�      � 5�      -   5        $��StartProgressBarStatus��5�       � Minimum$ 0Q�5�       � Maximum$	 100Q�5�      $��1��5�      $��1��5�      $��Bottom=�5�      $��Right=�5�      $��15��5�      $	��316��5�      $��
0,0,180,10q�6&   �� 5�      -	   5         ��Name$ 
listStatus��5�      $��1��5�      $��1��5�      
���   �$��29,222,335,41q�5�      
����� �$��Calibri Light)�5�       5�      ��%_   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 10��69   5�      5�      5�      �� 5�      -
   5        $��	Randomize��5�      $
��Left=�6)   $��496,187,0,0q�6D   $	��Top=�6Y   $��50��6q   $��91��6}   6�   5�      5�      5�      