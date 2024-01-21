using System;

using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Linq;
using KioskRestoration.Model;
using KioskRestoration.View;

using System.Windows.Controls.Primitives;
using KioskRestoration.ViewModel;
using System.IO;
using System.Linq.Expressions;
using System.Diagnostics;

namespace KioskRestoration
{
    /// <summary>
    /// Логика взаимодействия для Exhibit.xaml
    /// </summary>
    public partial class Exhibit : Page
    {
        public int idExhibit;
        public string[] imgFiles;
        public int indexImage;
        public int lastIndexImage;

        public int IdExhibit {
            get { return idExhibit; }
            set
            {
                idExhibit = value;
            }
        }

        public void Consultar()
        {
            DataContext db = new DataContext();
            string sqlExpression = "SELECT * FROM exhibit WHERE id='"+ IdExhibit + "'";
            SQLiteCommand command = new SQLiteCommand(sqlExpression, db.Connect());
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    this.TitleExhibit.Text = reader.GetString(2);
                    this.TextExhibit.Text = reader.GetString(3);



                    //ImageBrush ElipsImage = new ImageBrush();
                    //ElipsImage.ImageSource = new BitmapImage(new Uri(Path.Combine("exhibit\\", reader.GetString(1), reader.GetString(2)), UriKind.Relative));

                    //Path.GetFullPath(Convert.ToString(ElipsImage.ImageSource));
                    //Uri uriAddress = new Uri("exhibit/" + reader.GetString(1) + "/" + reader.GetString(2), UriKind.Relative);

                    string com = Path.Combine("exhibit\\", reader.GetString(1), reader.GetString(2));
                    Uri uriAddress = new Uri(com, UriKind.Relative);
                    imgFiles = Directory.GetFiles(Path.GetFullPath(uriAddress.ToString()), "*.jpg");
                    lastIndexImage = imgFiles.Length - 1;
                    this.ListExhibit.ItemsSource = imgFiles;

                    //this.image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(txtFiles[1]); 
                }
            }
        }

        public Exhibit()
        {
            InitializeComponent();
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }
        private void Button_MouseEnter_1(object sender, RoutedEventArgs e)
        {
            string pach = (string)((Button)sender).CommandParameter;

            indexImage = Array.IndexOf(imgFiles, pach);


            Popup1.IsOpen = true;
            imagePOPup.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(pach); 
        }

        //private void img_MouseWheel(object sender, TouchEventArgs e)
        //{

        //    Point centerPoint = e.GetTouchPoint(imagePOPup);
        //    this.sfr.CenterX = centerPoint.X;
        //    this.sfr.CenterY = centerPoint.Y;
        //    if (sfr.ScaleX < 0.3 && sfr.ScaleY < 0.3 && e.Delta < 0)
        //    {
        //        return;
        //    }
        //    sfr.ScaleX += (double)e.Delta / 3500;
        //    sfr.ScaleY += (double)e.Delta / 3500;
        //}

        private void Image_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            UIElement element = (UIElement)e.Source;

            // Манипуляция с внешним видом элемента и его позицией
            Matrix matrix = ((MatrixTransform)element.RenderTransform).Matrix;
            ManipulationDelta md = e.DeltaManipulation;
        }

        private void Image_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            // Указываем базовый контейнер
            e.ManipulationContainer = imagePOPup;

            // Указание разрешенных манипуляций
            e.Mode = ManipulationModes.All;
        }

        private void Clouse_Click(object sender, RoutedEventArgs e)
        {
            Popup1.IsOpen = false;
        }

        private void PrevImage_Click(object sender, RoutedEventArgs e)
        {
            if (indexImage == 0)
            {
                indexImage = lastIndexImage;
            }
            indexImage = indexImage - 1;
            imagePOPup.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(imgFiles[indexImage]);
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (indexImage == lastIndexImage)
            {
                indexImage = 0;
            }
            indexImage = indexImage + 1;
            imagePOPup.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(imgFiles[indexImage]);

        }


        void Window_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
            e.Handled = true;
        }

        void Window_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {

            // Get the Rectangle and its RenderTransform matrix.
            Image rectToMove = e.OriginalSource as Image;
            Matrix rectsMatrix = ((MatrixTransform)rectToMove.RenderTransform).Matrix;

            // Rotate the Rectangle.
            //rectsMatrix.RotateAt(e.DeltaManipulation.Rotation,
            //                     e.ManipulationOrigin.X,
            //                     e.ManipulationOrigin.Y);

            // Resize the Rectangle.  Keep it square
            // so use only the X value of Scale.
            rectsMatrix.ScaleAt(e.DeltaManipulation.Scale.X,
                                e.DeltaManipulation.Scale.X,
                                e.ManipulationOrigin.X,
                                e.ManipulationOrigin.Y);

            // Move the Rectangle.
            rectsMatrix.Translate(e.DeltaManipulation.Translation.X,
                                  e.DeltaManipulation.Translation.Y);

            // Apply the changes to the Rectangle.
            rectToMove.RenderTransform = new MatrixTransform(rectsMatrix);

            Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

            Rect ImageContayner = rectToMove.RenderTransform.TransformBounds(new Rect(rectToMove.RenderSize));

            
            // Check if the rectangle is completely in the window.
            // If it is not and intertia is occuring, stop the manipulation.
            if (e.IsInertial && !containingRect.Contains(ImageContayner))
            {
                e.Complete();
            }

            e.Handled = true;
        }

        //void Window_InertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        //{

        //    // Decrease the velocity of the Rectangle's movement by
        //    // 10 inches per second every second.
        //    // (10 inches * 96 pixels per inch / 1000ms^2)
        //    e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);

        //    // Decrease the velocity of the Rectangle's resizing by
        //    // 0.1 inches per second every second.
        //    // (0.1 inches * 96 pixels per inch / (1000ms^2)
        //    e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / (1000.0 * 1000.0);

        //    // Decrease the velocity of the Rectangle's rotation rate by
        //    // 2 rotations per second every second.
        //    // (2 * 360 degrees / (1000ms^2)
        //    e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);

        //    e.Handled = true;
        //}


    }
}
