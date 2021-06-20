using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static WpfApp9.Data;
using Point = WpfApp9.Data.Point;

namespace WpfApp9
{
    class Game
    {
        private const int _delay = 300;

        private readonly MainViewModel _viewModel; 
        private readonly Snake _snake; // червь
        private readonly Food _food; // Еда
        private Direction _direction; // Куда червь ползёт
        public List<List<Cell>> Arena { get; }
        private CancellationTokenSource _cts = null; // остановка на паузу 
        private bool _addDelay; // добавить паузу после смены напраления

        public List<List<Cell>> Arena { get; } // игровое поле

        public Direction Direction
        {
            get => _direction;
            set
            {
                
                if (value != _direction && (int)value % 2 != (int)_direction % 2)
                {
                    _direction = value;
                    _addDelay = true;
                    Update(); // немедленная реакция на нажатие
                }
            }
        }

        
        public Game(MainViewModel viewModel) // создание новой игры
        {
            _viewModel = viewModel;
            int width = 40;
            int height = 30;
            Arena = new List<List<Cell>>();
            for (int i = 0; i < height; i++)
            {
                List<Cell> row = new List<Cell>();
                for (int j = 0; j < width; j++)
                {
                    row.Add(new Cell());
                }
                Arena.Add(row);
            }
            _food = new Food(Arena, 10, 2);
            _snake = new Snake(this, _food, new point(Arena[0].Count / 2, Arena.Count / 2), 1, Direction.Right);
        }

        
        public void Start() // запустк игры
        {
            if (_cts == null)
                Run();
        }

        public void Stop()  // остановка игры
        {
            _cts?.Cancel();
        }

        
        private async void Run()
        {
            using (_cts = new CancellationTokenSource())
            {
                try
                {
                    while (true) // повтор
                    {
                        if (_snake.Died) // червь умер
                        {
                            _viewModel.EndGame(); // Game Over
                            break;
                        }
                        else
                            Update(); // Обновить игровое состояние

                        await Task.Delay(_delay, _cts.Token); 
                        if (_addDelay)
                        {
                            _addDelay = false;
                            await Task.Delay(_delay / 2, _cts.Token);
                        }
                    }
                }
                catch (OperationCanceledException) { } // была остановка
                catch (Exception ex) // была другая ошибка
                {
                    MessageBox.Show(ex.Message);
                }
            }
            _cts = null;

        }

       
        public void GiveScore()// начисляет 5 очков за каждую найденную еду
        {
            _viewModel.Score += 5;
        }

        
        public void Update() // обновляет игровое состояние
        {
            _snake.Move(Direction); // подвинуть червя 
            _food.Update(); // попросить еду добавиться на поле
        }

    }
    }
