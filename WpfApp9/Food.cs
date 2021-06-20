using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp9
{
    internal class Food
    {
    private readonly int _foodDelay; // задержка между появлением еды в игровых ходах
    private readonly int _maxFood; // максимальное количество еды на поле
    private readonly Random _rnd;
   //int point; 
    public readonly List<List<Cell>> _arena; // ссылка на игровое поле

    private int tick; // сколько ходов прошло с момента последнего появления еды

    public int FoodCount { get; set; } // количество еды на поле 

    public Food(List<List<Cell>> arena, int foodDelay, int maxFood)
    {
        _rnd = new Random();
        _arena = arena;
        _foodDelay = foodDelay;
        _maxFood = maxFood;
    }

    
    public void Update()  // добавить еду
        {
        if (tick >= _foodDelay && FoodCount < _maxFood)
        {
            tick = 0;
            while (true) 
            {
                Point point = new Point(_rnd.Next(_arena[0].Count), _rnd.Next(_arena.Count)); // выбрать случайную клетку
                if (_arena[point.Y][point.X].State == CellState.Empty) // если пусто
                {
                    _arena[point.Y][point.X].State = CellState.Food; // нарисовать еду
                    FoodCount++; // еды стало больше
                    break;
                }
            }
        }
        else
            tick++; // +1 ход не было еды
    }
}

    internal class Food
    {
        private List<List<object>> arena;
        private int v1;
        private int v2;

        public Food(List<List<object>> arena, int v1, int v2)
        {
            this.arena = arena;
            this.v1 = v1;
            this.v2 = v2;
        }

        
    }
}