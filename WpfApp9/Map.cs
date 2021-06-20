using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp9
{
    public class Map 
    {
        private MapState _state;

        public MapState State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        private class MapState
        {
        }
    }
}
