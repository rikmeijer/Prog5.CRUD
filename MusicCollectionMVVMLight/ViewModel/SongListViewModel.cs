using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MusicCollectionMVVMLight.Model;
using MusicCollectionMVVMMVVMLight.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using MusicCollectionMVVMLight.View;

namespace MusicCollectionMVVMLight.ViewModel
{
    public class SongListViewModel : ViewModelBase
    {
        private AddSongWindow _addSongWindow;

        ISongRepository songRepository;

        public ObservableCollection<SongViewModel> Songs { get; set; }

        //Dit model heeft de waarde van het liedje dat geselecteerd is. 
        //Misschien kun je dit model wel gebruiken voor de opdracht?
        private SongViewModel _selectedSong;

        public SongViewModel SelectedSong
        {
            get { return _selectedSong; }
            set
            {
                _selectedSong = value;
                base.RaisePropertyChanged();
            }
        }

        //Commands
        public ICommand ShowAddSongCommand { get; set; }

        //Commands
        public ICommand DeleteSongCommand { get; set; }

        public SongListViewModel()
        {
            songRepository = new DummySongRepository();
            var songList = songRepository.GetSongs().Select(s => new SongViewModel(s));
            ShowAddSongCommand = new RelayCommand(ShowAddSong, CanShowAddSong);
            DeleteSongCommand = new RelayCommand(DeleteSong, CanDeleteSong);
            Songs = new ObservableCollection<SongViewModel>(songList);
        }

        private bool CanDeleteSong()
        {
            return Songs.Contains(_selectedSong);
        }

        private void DeleteSong()
        {
            Songs.Remove(_selectedSong);
        }

        public void ShowAddSong()
        {
            _addSongWindow = new AddSongWindow();
            _addSongWindow.Show();
        }

        public bool CanShowAddSong()
        {
            return _addSongWindow != null ? !_addSongWindow.IsVisible : true;
        }

        public void HideAddSong()
        {
            _addSongWindow.Close();
        }
    }
}