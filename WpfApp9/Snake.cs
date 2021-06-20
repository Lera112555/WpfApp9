using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using static WpfApp9.Data;
using Object = WpfApp9.Data.Object;
using Point = WpfApp9.Data.Point;

namespace WpfApp9
{
    internal class Snake
    {
        private readonly Queue<Point> _tail; // хвост червя
        private readonly Food _food; // червь взаимодействует с едой
        private readonly Game _game; //  игровое поле

        private Point _head; // голова червя
        public int _length; // длина червя без учета головы
        public bool Died { get; private set; } // показывает, жив ли червь

        public Point Head // голова
        {
            get => _head;
            private set
            {
                _head = value;
                
                _game.Arena[value.Y][value.X].Object  = Object.Snake; // отобразить голову на поле
            }
        }

        // создать нового червя 
       
        public Snake(int size) // вторая попытка рисования червя
        {
            Ellipse elp = new Ellipse();
            elp.Width = size;
            elp.Height = size;
            elp.Fill = Brushes.Red;
            UIElement = elp;
            for (int i = 0; i < len; i++) ; 
        }

        
        public void Move(Direction direction)  // ползти на одну клетку в направлении direction
        {
            Point point = Head;
            switch (direction)
            {
                case Direction.Right:
                    point = new Point(point.X + 1, point.Y);
                    break;
                case Direction.Down:
                    point = new Point(point.X, point.Y + 1);
                    break;
                case Direction.Left:
                   point = new Point(point.X - 1, point.Y);
                    break;
                case Direction.Up:
                    point = new Point(point.X, point.Y - 1);
                    break;
            }
            if (!CheckMove(point)) // проверка возможности движения 
                return;
            _tail.Enqueue(Head); // старая голова стала началом хвоста
            Head = point; // новая голова

            while (_tail.Count > _length) // если хвост длиннее, чем нужно
            {
                 Point tail = _tail.Dequeue(); // уменьшить хвост 
                _game.Arena[tail.Y][tail.X].State = CellState.Empty; // отобразить это на игровом поле
            }
        }

        // проверка следующего хода
        private bool CheckMove(Point coords)
        {
            // если выползем за пределы или врежемся в себя
            if (coords.X >= _game.Arena[0].Count || coords.X < 0 || coords.Y >= _game.Arena.Count || coords.Y < 0 || _game.Arena[coords.Y][coords.X].State == CellState.Snake)
                Died = true; // то умрём
            else
            if (_game.Arena[coords.Y][coords.X].State == CellState.Food) // иначе может попасться еда
            {
                _food.FoodCount--; //еды стало меньше 
                _length++; // вырастить хвост
                _game.GiveScore(); // добавить очки 
            }
            return !Died;
        }





        internal bool Died;
        public Ellipse UIElement { get; }
        private Game game;
        private Food food;
        int len; //длина червя 
        private Data.Point point;
        private int v;
        private Data.Direction right;

        public Snake(Game game, Food food, Data.Point point, int v, Data.Direction right)
        {
            this.game = game;
            this.food = food;
            this.point = point;
            this.v = v;
            this.right = right;
        }

        internal void Move(Data.Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}