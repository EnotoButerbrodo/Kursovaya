﻿using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using NovelkaCreationTool.Models;
using NovelkaCreationTool.ViewModels.Base;
using System.Windows.Input;
using NovelkaCreationTool.Commands;
using System.Linq;
using System.IO;
using NovelkaCreationTool.Views;
using System.Windows;

namespace NovelkaCreationTool.ViewModels
{
    public class SlidesViewModel : ViewModelBase
    {
        DirectoryInfo FolderPath = new DirectoryInfo("temp");
        public ObservableCollection<Slide> Slides { get; set; } = new ObservableCollection<Slide>();
        public ObservableCollection<Background> Backgrounds { get; set; } = new ObservableCollection<Background>();
        public ObservableCollection<SlideImage> SlideImages { get; set; } = new ObservableCollection<SlideImage>();
        Slide selectedSlide;
        Background selectedBackground;
        BitmapImage previewImage;
        SlideImage selectedSlideImage;
        public Slide SelectedSlide
        {
            get => selectedSlide;
            set => Set(ref selectedSlide, value);
        }
        public Background SelectedBackground
        {
            get => selectedBackground;
            set => Set(ref selectedBackground, value);
        }
        public SlideImage SelectedSlideImage
        {
            get => selectedSlideImage;
            set => Set(ref selectedSlideImage, value);
        }

        AddSlideImageDialogWindow addSlideImageDialogWindow;


        #region AddSlideCommand

        public ICommand AddSlideCommand { get; }

        private void OnAddSlideCommandExecuted(object p)
        {
            var slide = new Slide
            {
                Id = Slides.Count + 1
            };
            Slides.Add(slide);
            SelectedSlide = slide;

        }
        private bool CanAddSlideCommandExecute(object p)
        {
            return true;
        }

        #endregion
        #region DeleteSlideCommand

        public ICommand DeleteSlideCommand { get; }

        private void OnDeleteSlideCommandExecuted(object p)
        {
            Slides.Remove(SelectedSlide);
            if (Slides.Count > 0)
            {
                for (int i = 0; i < Slides.Count; i++)
                {
                    Slides[i].Id = i + 1;
                }
                SelectedSlide = Slides.Last();
            }
        }
        private bool CanDeleteSlideCommandExecute(object p)
        {
            if (SelectedSlide == null) return false;
            return true;
        }

        #endregion
        #region LoadBackgroundsList

        public ICommand LoadBackgroundsListCommand { get; }

        private void OnLoadBackgroundsListExecuted(object p)
        {
            var files = FolderPath.GetFiles();
            foreach (var file in files)
            {
                Background background = new Background
                {
                    Name = file.Name,
                    FullName = file.FullName
                };
                Backgrounds.Add(background);
            }

        }
        private bool CanLoadBackgroundsListExecute(object p)
        {
            return true;
        }

        #endregion
        #region SetImageAsBackgroundCommand

        public ICommand SetImageAsBackgroundCommand { get; }

        private void OnSetImageAsBackgroundExecuted(object p)
        {
            SelectedSlide.BackgroundImageName = SelectedBackground.FullName;

        }
        private bool CanSetImageAsBackgroundExecute(object p)
        {
            return (SelectedBackground != null && SelectedSlide != null);
        }

        #endregion
        public ICommand AddNewSlideImageCommand { get; }
        private void OnAddNewSlideImageCommandEx(object p)
        {
            var window = new AddSlideImageDialogWindow
            {
                Owner = Application.Current.MainWindow
            };
            addSlideImageDialogWindow = window;
            addSlideImageDialogWindow.ShowDialog();
        }
        public SlidesViewModel()
        {
            #region Commands

            AddSlideCommand = new LambdaCommand(OnAddSlideCommandExecuted, CanAddSlideCommandExecute);
            DeleteSlideCommand = new LambdaCommand(OnDeleteSlideCommandExecuted, CanDeleteSlideCommandExecute);
            LoadBackgroundsListCommand = new LambdaCommand(OnLoadBackgroundsListExecuted, CanLoadBackgroundsListExecute);
            SetImageAsBackgroundCommand = new LambdaCommand(OnSetImageAsBackgroundExecuted, CanSetImageAsBackgroundExecute);
            AddNewSlideImageCommand = new LambdaCommand(OnAddNewSlideImageCommandEx, (obj)=> { return true; });
            #endregion
            Slides.Add(new Slide
            {
                Id = 1
            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 1",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 200,
                Y = 200,
                Height = 200,
                Width = 200

            });
            Slides[0].Images.Add(new SlideImage
            {
                Name = "Image 2",
                ImageName = @"S:\Users\Игорь\source\repos\Kursovaya\NovelkaCreationTool\bin\Debug\net5.0-windows\temp\00769329426A88EBE20E6088C449F46C.jpg",
                X = 300,
                Y = 0,
                Height = 100,
                Width = 100

            });

        }
    }
}
