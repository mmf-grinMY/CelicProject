   M S B A M L   `   `   `  ????G  CCelic, Version=1.0.8270.36791, Culture=neutral, PublicKeyToken=nullclr-namespace:CelicCelic  5   	       Celic.SCalcDWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Celic     5   	     ? Title$>  8Расчет междубурьерных целиков??5	   	   $	??384??5
   	   $	??344??5   	   $	??800??5   	   $	??800??5   	    ? 	Resources 5       ?? 
TargetType ?? Property ?? Value??%  '??       '?? ?     '??     '?? r    ?? 5   
   " ??6   .?5      ??  ? ??$ 14(?6)   ?? 5       ? ??$	 200??6&   ?? 5       ? ??$ 4,0,0,0q?6'   5      ?? 5   
   " ??6   .?5      ??  ? ??$
 10,2q?6'   ?? 5       ? ??$ 14(?6)   5      ?? 5   
   " ??6   .?5      ??  ? ??$	 100??6&   ?? 5       ? ??$	 0,2q?6'   5      ?? 5   
   " ??6   .?5      ??  ? ??$	 100??6&   ?? 5       ? ??$ 0,0,0,2q?6'   5      5       .??5!      ?  ? RowDefinitions 5"   
   ?? 5#      $
??Auto ?6   ?? 5$      $	??320 ?6    ? ColumnDefinitions 5&   
   ?? 5'      $
??Auto ?6   ?? 5(      $??322.4 ?6   ?? 5)      $
??Auto ?6   ?? 5*      .+?5,   
      Celic.PlastMenu  $??4??6   ?? 5.   
   $	??Top=?6   $??1??5/      $??1??50      .+?51      ?? $??4,0,0,0q?6   .+?52      ?? $??Center=?6   $??14(?54      $#??Выбранный пласт??55      ??53      Z? 55         Celic.PlastDataBox  56      -   5          Name$ selected??56   %    ? DataContext 57   %   ?? *SelectedPlast+?? 58      $??Center=?6   $??4q?59      $%??Поменять местами??5:      ??5;      ?? *SwapCommand+5<      5=      ?? 5?   
   $??1??6   .+?5@      ?? $??Center=?6   $??14(?6F   $??6,0,0,0q?6T   $@??:Список разрабатываемых пластов??6e   ??65   Z? 	 ? ContextMenu	 5A      ?? 5B      .@?5C      m? $??Добавить??6#   
 m? Command
 5D   #   ?? *
AddCommand+ m? CommandParameter 5E   #   ?? *
LeftPlasts+m? 5F      $??Удалить??6#   
 5G   #   ?? *RemoveCommand+ 5H   #   ?? *LeftPlastSelected+5I      5K      ?? 5L      $	??298??5N      $
??-2,0q?5O      $??4,0,0,0q?5P      $
??Auto??5Q      ??5L      ?? *
LeftPlasts+ ?? SelectedItem 5M      ?? *LeftPlastSelected+??5R      ?? 5S      .R?5T         Celic.SPlastDataItem  5U      5W      5X      ?? 5Z   
   $??1??6   $??2??6"   .+?5[      ?? $??Center=?6   $??14(?6F   $??6,0,0,0q?6T   $@??:Список разрабатываемых пластов??6e   ??65   Z? 	 5\      ?? 5]      .@?5^      m? $??Добавить??6#   
 5_   #   ?? *
AddCommand+ 5`   #   ?? *RightPlasts+m? 5a      $??Удалить??6#   
 5b   #   ?? *RemoveCommand+ 5c   #   ?? *RightPlastSelected+5d      5f      ?? 5g      $	??298??5i      $
??-2,0q?5j      $??4,0,0,0q?5k      ??5g      ?? *RightPlasts+ 5h      ?? *RightPlastSelected+??5l      ?? 5m      .R?5n        5o      5q      5r      5s      5t      