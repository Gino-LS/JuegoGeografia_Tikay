   M S B A M L   `   `   `  ����   JuegoGeografia    JuegoGeografia.Preguntas   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com    � Title$  	Preguntas��$	��600��$	��900������  �� ImageSource$ Images/FondoPreguntas.png�� j� Stretch$
 Fill=�.��� .+��� -   $��btnSound��$��50��$��50��$��Right=�$	��Top=�$��10q�����  �� 
TargetType" ��.R��� $��Images/Sonido_OFF.png��$��Uniform=��� -   $��imgmapa_latinoamerica��$��Images/final.png��$��UniformToFill=�$��818,400,0,92q���  �� Orientation$ 
Horizontal=�$��Center=�$	��Top=�$��
20,10,20,0q�.+��� $
��15,0q�.+��� $��Images/Tiempo.png��$��50��$��50���� -   $��	txtTiempo��$��Tiempo: 20 seg��
���   �$
��Bold"�$��Center=�$��18(��� $
��15,0q�.+��� $��Images/Puntos.png��$��50��$��50���� -   $��	txtPuntos��$��	Puntos: 0��
���   �$
��Bold"�$��Center=�$��18(��� $
��15,0q�.+��� $ ��Images/NumeroPreguntas.png��$��50��$��50���� -   $��txtNumeroPreguntas��$
��1/15��
���   �$
��Bold"�$��Center=�$��18(���  �� CornerRadius$ 15��$��4q�
�������$��Center=�$	��Top=� �� Padding$ 10q�$��0,95,0,0q�$	��574������  �� Color$ White��._��� $ 10��$	��1.5q����#FF4A4A
�������$ 10q�._��� -   $��txtPregunta��$"��Aquí aparecerá la pregunta��$��22(�$��SemiBold"����#212121$��Center=�$��Center=�$��Arial)�$
��Wrap=� �� TextAlignment$ Center=�	 J� Effect	 3  -System.Windows.Media.Effects.DropShadowEffect  
   Color$
 Black��   
BlurRadius$ 1Q�   ShadowDepth$	 0.5Q��� $ Vertical=�$��Center=�$��Center=�$��	0,70,0,50q�.+��� -   $��opcion1��$	��400��$��50��$��18(�$��0q�$
��Bold"�$	��0,0q����� " ��.R�� .+��� $ 25��$��2q����#D2691E����  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ #FFC107�� � Offset$	 0.0Q�� $ #FF9800��$	 1.0Q��� $��Center=�$��Center=�
�������$��Arial Black)��� -   $��opcion2��$	��400��$��50��$��18(�$��0q�$
��Bold"�$
��0,10q����� " ��.R�� .+��� $ 25��$��2q����#D2691E���� $	 0,0&�$	 0,1&�.��� $ #FFC107��$	 0.0Q�� $ #FF9800��$	 1.0Q��� $��Center=�$��Center=�
�������$��Arial Black)��� -	   $��opcion3��$	��400��$��50��$��18(�$��0q�$
��Bold"�$	��0,0q����� " ��.R�� .+��� $ 25��$��2q����#D2691E���� $	 0,0&�$	 0,1&�.��� $ #FFC107��$	 0.0Q�� $ #FF9800��$	 1.0Q��� $��Center=�$��Center=�
�������$��Arial Black)��� -
   $��AnimatedImage��$	��100��$	��100��$
��Left=�$��Bottom=�$��5,0,0,91q� � Triggers :�  :� RoutedEvent$ Image.Loaded��.[��� .����  g� RepeatBehavior$ Forever��.��T�  �� TargetProperty$ Source� g� Duration$ 0:0:1.6D�..�i�  R� KeyTime$ 0:0:0�� R� Value ��  �� 	UriSource$ Images/pixel_frame1.png?�i� $ 0:0:0.4�� �� $ Images/pixel_frame2.png?�i� $ 0:0:0.8�� �� $ Images/pixel_frame3.png?�i� $ 0:0:1.2�� �� $ Images/pixel_frame4.png?��� -   $��Seleccionar otro país��$��Right=�$��Bottom=�$��	0,0,30,30q�$	��247��$��44��$��20(�
�������$��Comic Sans MS)����� " ��.R�� .+��� $ 40��$��5q����#00BFFF���� $	 0,0&�$	 0,1&�.��� $ #4FC3F7��$	 0.0Q�� $ #0288D1��$	 1.0Q��� #��z��
�������$
��Bold"�$��Center=�$��Center=�$��Comic Sans MS)�$��20(�	   $
 Black��$ 4Q�$ 2Q�