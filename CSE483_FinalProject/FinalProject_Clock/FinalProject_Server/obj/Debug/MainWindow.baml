   M S B A M L   `   `   `  ����N  JFinalProject_Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FinalProject_ServerFinalProject_Server  5   	   $    FinalProject_Server.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:FinalProject_Server     5   	     � Title$  
MainWindow��5   	   $	��300��6   $	��450��6)   .��5	      � .+�5
   
   �� -   5        $��setTimeButton��5
   �   $��Set Time��6   $
��Left=�6$   $��178,127,0,0q�6?   $	��Top=�6T   $��76��6l   6�   �� 5   
   -   5        $��setNowButton��5   �   $��NOW Time��6   $
��Left=�6$   $��179,161,0,0q�6?   $	��Top=�6T   $��75��6l   6�   �� 5   
   -   5        $��setAlarmButton��5   �   $��	Set Alarm��6   $
��Left=�6%   $��180,197,0,0q�6@   $	��Top=�6U   $��74��6m   ���	#FFD821216�   �� 5   
   -   5        $��timeFormatBox��5   �   $#��Display Clock in 24 Hour Time��6   $��Center=�6;   $��	0,244,0,0q�6X   $	��Top=�6k    d� 	IsChecked 6�   �� *
TimeFormat+�� 5   
   $
��Left=�6   $��23��6-   $��
152,83,0,0q�69    �� TextWrapping$
 Wrap=�6M   $	��Top=�6|   $��37��6�   ��6a   �� *	HourInput+6�   �� 5   
   $
��Left=�6   $��23��6-   $��
194,83,0,0q�69   $
 Wrap=�6M   $	��Top=�6~   $��37��6�   ��6a   �� *MinuteInput+6�   �� 5   
   $
��Left=�6   $��23��6-   $��
236,83,0,0q�69   $
 Wrap=�6M   $	��Top=�6~   $��37��6�   ��6a   �� *SecondInput+6�   �� 5   
   $�� Hr   :   Min   :    Sec��6   $
��Left=�63   $��
152,52,0,0q�6N   $	��Top=�6b   5      5      