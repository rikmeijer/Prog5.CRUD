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
            set {
                _selectedSong = value;
                base.RaisePropertyChanged();
            }
        }

        //Commands
        public ICommand ShowAddSongCommand { get; set; }
        public ICommand ShowEditSongCommand { get; set; }
        public ICommand ShowPlaySongCommand { get; set; }
        public ICommand DeleteSongCommand { get; set; }
        public ICommand PreviousSongCommand { get; private set; }
        public ICommand NextSongCommand { get; set; }
        

        public SongListViewModel()
        {
            songRepository = new DummySongRepository();
            var songList = songRepository.GetSongs().Select(s => new SongViewModel(s));
            Songs = new ObservableCollection<SongViewModel>(songList);

            ShowAddSongCommand = new RelayCommand(ShowAddSong, CanShowAddSong);
            DeleteSongCommand = new RelayCommand(DeleteSong);
            ShowPlaySongCommand = new RelayCommand(ShowPlaySong);
            ShowEditSongCommand = new RelayCommand(ShowEditSong);

            NextSongCommand = new RelayCommand(NextSong);
            PreviousSongCommand = new RelayCommand(PreviousSong);
        }

        public void NextSong()
        {
            var nextIndex = Songs.IndexOf(SelectedSong) + 1;
            if(nextIndex < Songs.Count)
                SelectedSong = Songs[nextIndex];
        }

        public void PreviousSong()
        {
            var previousIndex = Songs.IndexOf(SelectedSong) - 1;
            if (previousIndex >= 0)
                SelectedSong = Songs[previousIndex];
        }

        public void ShowEditSong()
        {
            var editSong = new EditSongWindow();
            editSong.Show();
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

        public void ShowPlaySong()
        {
            var play = new PlaySongWindow();
            play.Show();
        }

        public void HideAddSong()
        {
            _addSongWindow.Close();
        }

        private void DeleteSong()
        {
            Songs.Remove(SelectedSong);
        }
    }
}