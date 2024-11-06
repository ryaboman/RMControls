using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RMControls.ViewNotes {
    /// <summary>
    /// Логика взаимодействия для ViewerNote.xaml
    /// </summary>
    public partial class ViewerNote : UserControl{
        public Dictionary<string, List<Border>> dictionBorders;

        public ViewerNote(){
            InitializeComponent();        
        }

        public void Change(float startBlockTime, float endBlockTime)
        {
            foreach(var diction in dictionBorders)
            {
                foreach(var border in diction.Value)
                {
                    float top = (float)border.Margin.Top + startBlockTime;
                    float left = (float)border.Margin.Left;

                    border.Margin = new Thickness(left, top, 0, 0);
                }
            }
        }

        public void CreateBorders(Dictionary<string, List<INotable>> composition){
            dictionBorders = new Dictionary<string, List<Border>>();

            foreach (var cmp in composition)
            {
                List<Border> borders = new List<Border>();

                foreach(var note in cmp.Value)
                {
                    borders.Add(CreateBorder(note));
                }

                dictionBorders[cmp.Key] = borders;
            }
        }

        Border CreateBorder(INotable note) {
            Border border = new Border();//Create an object of border class.
            border.BorderBrush = new SolidColorBrush(Colors.Black);//change the border color of the border control.
            border.BorderThickness = new Thickness(1);//Change the thickness of the border control.

            if(note.getPitchName() == "Pedal#"){
                border.Width = 1040;

                border.Background = new SolidColorBrush(Colors.LightBlue);
            }
            else
            {
                border.Width = 10;//Setting the width of the border control.

                Color color = new Color();

                color.R = 0;
                color.G = 0;
                color.B = 0;

                UInt32 fd = (UInt32)note.getForceDown() * 2;

                byte clr = (byte)fd; //BitConverter.GetBytes(note.forceDown * 100 / 127)[0];

                byte _clr = (byte)(255 - clr);

                border.Background = new SolidColorBrush(Color.FromArgb(255, clr, _clr, 0));

                //border.Background = new SolidColorBrush(color);//Change the background color of the border control.
            }
            

            

            border.Height = (note.getEndTime() - note.getStartTime()) * 100;

            border.Margin = new Thickness(GetDistance(note), note.getStartTime()*100, 0, 0);

            border.HorizontalAlignment = HorizontalAlignment.Left;
            border.VerticalAlignment = VerticalAlignment.Top;

            display.Children.Add(border);           

            return border;
        }

        float GetDistance(INotable note){
            //A B C D E F G ASharp CSharp DSharp FSharp Gsharp 

            string nameNote = note.getPitchName().Remove(note.getPitchName().Length - 1);

            //Преобразуем актаву в число
            int octava = note.getPitchName().Last() - '0';

            switch (nameNote)
            {
                case "Pedal":
                    return 0F;

                case "A":
                    return (5F + octava * 20 * 7);

                case "B":
                    return (25F + octava * 20 * 7);

                case "C":
                    return (45F + (octava-1) * 20 * 7);

                case "D":
                    return (65F + (octava - 1) * 20 * 7);

                case "E":
                    return (85F + (octava - 1) * 20 * 7);

                case "F":
                    return (105F + (octava - 1) * 20 * 7);

                case "G":
                    return (125F + (octava - 1) * 20 * 7);

                case "ASharp":
                    return (17.5F + octava * 20 * 7);

                case "CSharp":
                    return (57.5F + (octava - 1) * 20 * 7);

                case "DSharp":
                    return (77.5F + (octava - 1) * 20 * 7);

                case "FSharp":
                    return (117.5F + (octava - 1) * 20 * 7);

                case "GSharp":
                    return (137.5F + (octava - 1) * 20 * 7);

                default: return 0;
            }
        }


    }
}
