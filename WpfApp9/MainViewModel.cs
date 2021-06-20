using System;
using System.Collections.Generic;
using System.Windows.Input;
using static WpfApp9.Data;

namespace WpfApp9
{
    internal class MainViewModel
    {
        private int _score;
        private int _highScore; 
        private Game _game;
        private bool _gameRunning;
        private bool _gameOver;
        private ICommand _moveCommand;
        private ICommand _startCommand;

        public int Score // Очки
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public int HighScore // Лучший результат
        {
            get => _highScore;
            set
            {
                _highScore = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        public List<List<Cell>> Arena // Ссылка на игровое поле для интерфейса
        {
            get => _arena;
            set
            {
                _arena = value;
                OnPropertyChanged();
            }
        }

        public bool GameRunning // Сейчас игра играет
        {
            get => _gameRunning;
            set
            {
                _gameRunning = value;
                OnPropertyChanged();
            }
        }

        public bool GameOver // Игра проиграна
        {
            get => _gameOver;
            set
            {
                _gameOver = value;
                OnPropertyChanged();
            }
        }

        
        public ICommand StartCommand => _startCommand = new RelayCommand(parameter =>  // Начало игры, привязка кнопок Старт и пауза на интерфейсе и клавиша пробел
        {
            if (!GameRunning)
            {
                if (GameOver)
                    NewGame();
                else
                {
                    GameRunning = true;
                    _game.Start();
                }
            }
            else
            {
                GameRunning = false;
                _game.Stop();
            }
        });

       
        public ICommand MoveCommand => _moveCommand = new RelayCommand(parameter =>  // привязка стрелок для управления 
        {
            if (GameRunning && Enum.TryParse(parameter.ToString(), out Direction direction))
            {
                _game.Direction = direction;
            }
        });

       
        public void EndGame() // червь умер, игра окончена 
        {
            GameRunning = false;
            GameOver = true;
            if (HighScore < Score)
                HighScore = Score;
        }

        
        private void NewGame() // Создать новой игры
        {
            if (GameRunning)
                _game.Stop();
            GameOver = false;
            Score = 0;
            _game = new Game(this);
            Arena = _game.Arena;
        }

        
        public MainViewModel()// Здесь всё начинается
        {
            NewGame();
        }
    } }