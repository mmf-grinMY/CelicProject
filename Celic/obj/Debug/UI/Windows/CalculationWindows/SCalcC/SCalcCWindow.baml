   M S B A M L   `   `   `  ????G  CCelic, Version=1.0.8270.36791, Culture=neutral, PublicKeyToken=nullclr-namespace:CelicCelic  5   	       Celic.SCalcCWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Celic     5   	     ? Title$:  4Расчет приразломных целиков??5	   	   $	??580??5
   	   $	??380??5   	    ? 	Resources 5       ?? 
TargetType ?? Property ?? Value??%R   '??       ?? 5   
   " ??6   .?5      ??  ? ??$ 14(?6)   5      5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5       ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      .+?5   
      Celic.PlastMenu  $??2??6   ?? 5   
   $??1??6   $	??4,0q?5      .+?5         Celic.ListBoxTitle  ?? 5      $
??-2,0q?5       $	??298??5!      $
??Auto??5"      $	??244??5#      ??5      ?? *Plasts+ ?? SelectedItem 5      ?? *SelectedPlast+??5$      ?? 5%      .R?5&         Celic.SPlastDataItem  5'      5)      5*      ?? 5+   
   $	??Top=?6   $
??Left=?5,      $??1??5-      $??1??5.      .+?5/      ?? .+?50      ?? $??Center=?6   $#??Выбранный пласт??51         Celic.PlastDataBox  52      -   5          Name$ selected??52   %   	 ? DataContext	 53   &   ?? *SelectedPlast+54      ?? 55      $	??0.3q?6   
???   ?$	??318??57      $??0,4,0,0q?58      ._?59      ?? 
 ?? Orientation$
 
Horizontal=?6   .+?5:      ?? $.??(Угол поворота разлома??6    $??2,0,0,0q?5;       $	??200??5<       ?? 5=      $??10,4,0,4q?6   $	??100??5>      ??5?      ??  ?? Path$
 Alfa?5@      5A      5B      5C      5D      