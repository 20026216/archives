   M S B A M L   `   `   `  ����J  FAssessment_test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Assessment_testAssessment_test  5   	        Assessment_test.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Assessment_test     5   	     � Title$  
MainWindow��5   	   $	��700��6   $
��1000��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ Adder��5
      $��Add Contractor��6    $��296,230,0,0q�69   $
��Left=�6N   $��48��6j   $	��Top=�6v   $	��148��6�   6�   �� 5   
   -   5         ��Name$ AvailableContractorList��5      $��24,87,726,73q�63   6I   �� 5   
   $��Available Contractors��6   $��24,24,828,602q�60   �� 5   
   -   5         ��Name$ textBoxLastName��5      $
��Left=�6+   $��45��6F   $��296,161,0,0q�6R    �� TextWrapping$
 Wrap=�6g   $��	Last Name��6{   $	��Top=�6�   $	��148��6�    J� RenderTransformOrigin$ -0.669,1.624&�6�   �� 5   
   -   5        $ textBoxFirstName��5      $
��Left=�6,   $��47��6G   $��
296,87,0,0q�6S   $
 Wrap=�6g   $��
First Name��6{   $	��Top=�6�   $	��148��6�   �� 5   
   -   5        $ 	Job_Adder��5      $��
Add to Job��6$   $��296,444,0,0q�69   $
��Left=�6N   $��46��6j   $	��Top=�6v   $	��148��6�   6�   �� 5   
   -   5        $ Remover��5      $��Remove Contractor��6"   $��296,303,0,0q�6>   $
��Left=�6S   $��46��6o   $	��Top=�6{   $	��148��6�   $ 1.793,-1.228&�6�   �� 5   
   -   5        $ Job_list��5      $��673,87,77,73q�6$   �� 5   
   $
��Jobs��6   $��673,29,179,597q�6   �� 5   
   -   5        $ Complete_Job��5      $��Complete Job��6'   $��296,504,0,0q�6>   $
��Left=�6S   $��46��6o   $	��Top=�6{   $	��148��6�   6�   �� 5   
   -	   5        $ 
Search_Job��5      $��Search Jobs by Cost��6%   $��517,555,0,0q�6C   $
��Left=�6X   $��46��6t   $	��Top=�6�   $	��148��6�   $ -1.322,-1.096&�6�   �� 5   
   -
   5        $ textBoxMinCost��5      $
��Left=�6*   $��46��6E   $��531,504,0,0q�6Q   $
 Wrap=�6f   $��Min Cost��6z   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ textBoxMaxCost��5      $
��Left=�6*   $��46��6E   $��531,444,0,0q�6Q   $
 Wrap=�6f   $��Max Cost��6z   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ textBoxJobName��5      $
��Left=�6*   $��46��6E   $��
531,87,0,0q�6Q   $
 Wrap=�6e   $��Job Name��6y   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ textBoxJobCost��5      $
��Left=�6*   $��46��6E   $��531,160,0,0q�6Q   $
 Wrap=�6f   $��Job Cost��6z   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ textBoxHourlyRate��5      $
��Left=�6-   $��46��6H   $��531,230,0,0q�6T   $
 Wrap=�6i   $��Hourly Rate��6}   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ Add_Job��5      $��Add Job��6"   $��531,303,0,0q�64   $
��Left=�6I   $��46��6e   $	��Top=�6q   $	��120��6�   6�   �� 5   
   -   5        $ 
Remove_Job��5      $��
Remove Job��6%   $��531,379,0,0q�6:   $
��Left=�6O   $��46��6k   $	��Top=�6w   $	��120��6�   6�   �� 5   
   $4��.Select a contractor,
then a job from the list.��6   $��296,361,556,240q�6N   �� 5   
   -   5        $ Show_All_Jobs��5      $��Show All Jobs��6(   $��724,611,0,0q�6@   $
��Left=�6U   $��46��6q   $	��Top=�6}   $	��148��6�   $ -1.322,-1.096&�6�   5!      5"      