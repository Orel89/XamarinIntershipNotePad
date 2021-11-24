using MapNotepad.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MapNotepad.Model.Pin
{
    public class PinViewModel : BindableBase, IPin
    {
        private int _Id;
        public int Id 
        {
            get => _Id;
            set => SetProperty(ref _Id, value);
        }

        private int _UserId;
        public int UserId
        {
            get => _UserId;
            set => SetProperty(ref _UserId, value);
        }

        private string _Label;
        public string Label
        {
            get => _Label;
            set => SetProperty(ref _Label, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        private double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        private DateTime _CreationTime;
        public DateTime CreationTime
        {
            get => _CreationTime;
            set => SetProperty(ref _CreationTime, value);
        }

        private bool _IsFavorite;
        public bool IsFavorite
        {
            get => _IsFavorite;
            set => SetProperty(ref _IsFavorite, value);
        }

        private ICommand _TapCommand;
        public ICommand TapCommand
        {
            get => _TapCommand;
            set => SetProperty(ref _TapCommand, value);
        }
        private ICommand _EditCommand;
        public ICommand EditCommand
        {
            get => _TapCommand;
            set => SetProperty(ref _EditCommand, value);
        }

        private ICommand _moveToPinLocationCommand;
        public ICommand MoveToPinLocationCommand
        {
            get => _moveToPinLocationCommand;
            set => SetProperty(ref _moveToPinLocationCommand, value);
        }

        private ICommand _IsFavoriteSwitchCommand;
        public ICommand IsFavoriteSwitchCommand
        {
            get => _IsFavoriteSwitchCommand;
            set => SetProperty(ref _IsFavoriteSwitchCommand, value);
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get => _deleteCommand;
            set => SetProperty(ref _deleteCommand, value);
        }
    }
}
