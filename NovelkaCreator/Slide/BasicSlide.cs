﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NovelkaCreator.Slide
{
    class BasicSlide
    {
        protected Image image;
        protected int id;
        protected Grid appearance;
        protected TextBlock idTextBlock;
        protected Border selectBorder;
        protected MouseButtonEventHandler SlideClickEventHandler;
        public BasicSlide(int id)
        {
            this.id = id;
            ImageSetup();
            IdTextBlockSetup();
            SelectBorderSetup();
            GridSetup();
            
        }
        void ImageSetup()
        {
            image = new Image();
            image.Stretch = Stretch.Uniform;
            Panel.SetZIndex(image, 1);
           
        }
        void GridSetup()
        {
            appearance = new Grid();
            ColumnDefinition idColumn = new ColumnDefinition
            {
                Width = new GridLength(20, GridUnitType.Pixel)
            };
            ColumnDefinition MainColumn = new ColumnDefinition
            {
                Width = new GridLength(200, GridUnitType.Star)
            };
            appearance.ColumnDefinitions.Add(idColumn);
            appearance.ColumnDefinitions.Add(MainColumn);
            appearance.Margin = new System.Windows.Thickness(20, 10, 20, 10);

            Grid.SetColumn(image, 1);
            appearance.Children.Add(image);


            Grid.SetColumn(idTextBlock, 0);
            appearance.Children.Add(idTextBlock);

            Grid.SetColumn(selectBorder, 1);
            appearance.Children.Add(selectBorder);

            appearance.PreviewMouseLeftButtonUp += SlideClick;

        }
        protected virtual void IdTextBlockSetup()
        {
            idTextBlock = new TextBlock();
            idTextBlock.Text = id.ToString();
            idTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            idTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            idTextBlock.FontSize = 20;
            idTextBlock.Foreground = Brushes.Black;

            
        }
        protected virtual void SelectBorderSetup()
        {
            selectBorder = new Border();
            selectBorder.Background = Brushes.Transparent;
            selectBorder.BorderBrush = Brushes.Red;
            selectBorder.BorderThickness = new Thickness(0);
            Panel.SetZIndex(selectBorder,0);
        }

        protected virtual void SlideLeftClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            selectBorder.BorderThickness = new Thickness(3);
            image.Margin = new Thickness(3);
            MessageBox.Show($"Выбранный слайд {id}");
            
        }
        public Grid GetAppearance() => appearance;
        public int GetNumber() => id;
        public void SetId(int id)
        {
            this.id = id;
            idTextBlock.Text = id.ToString();
        }
        public void SetSlideClickEventHandler(MouseButtonEventHandler handler)
        {
            SlideClickEventHandler += handler;
        }

        protected void SlideClick(object sender, MouseButtonEventArgs e)
        {
            SlideClickEventHandler?.Invoke(this, e);
        }

        public void ActiveteSlide()
        {
            idTextBlock.Foreground = Brushes.Red;
            image.Margin = new Thickness(3);
            selectBorder.BorderThickness = new Thickness(3);
        }

        public void DeactivateSlide()
        {
            idTextBlock.Foreground = Brushes.Black;
            image.Margin = new Thickness(0);
            selectBorder.BorderThickness = new Thickness(0);
        }

    }
}
