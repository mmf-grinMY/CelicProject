   M S B A M L   `   `   `  ????G  CCelic, Version=1.0.8270.36791, Culture=neutral, PublicKeyToken=nullclr-namespace:CelicCelic  5   	       Celic.SCalcBWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Celic     5   	   $??14(?5	   	     ? Title$R  LРасчет высоты зоны водопроводящих трещин??5
   	   $	??500??5   	   $	??600??5   	    ? 	Resources 5         PlastDataItem   Celic.PlastDataItem??%2   &	        ?? 5      .R?5        5      5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5       ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??* ?6   .+?5   
      Celic.PlastMenu  $??2??6   ?? 5   
   $	??4,0q?6   $??1??5      .+?5          Celic.ListBoxTitle  ?? 5!      
???   ?$	??0.3q?5"       J? SnapsToDevicePixels . ._?5$      ?? $	??500??6   $
??Auto=?5%      .??5&      ?? $
??-2,0q?5(      $	??200??5)      
?????? #??[  5+      ??5&      ?? *Plasts+ ?? SelectedItem 5'      ?? *SelectedPlast+5+      5,      5-      5.      ?? 5/   
   $??1??6   $??1??50      $??0,0,4,0q?51      $
??Left=?52      .+?53      ?? $??Center=?6   $??14(?55      $#??Выбранный пласт??56      ??54      Z? 56         Celic.PlastDataBox  57      -   5          Name$ selected??57   !   58      59      5:      