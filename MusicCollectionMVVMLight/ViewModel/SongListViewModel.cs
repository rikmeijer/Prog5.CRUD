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

        public ICommand PlayalongCommand { get; set; }

        public ICommand DeleteSongCommand { get; set; }


        public ICommand PrevCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public SongListViewModel()
        {
            songRepository = new DummySongRepository();
            var songList = songRepository.GetSongs().Select(s => new SongViewModel(s));
            ShowAddSongCommand = new RelayCommand(ShowAddSong, CanShowAddSong);
            PlayalongCommand = new RelayCommand(Playalong, CanPlayalong);
            DeleteSongCommand = new RelayCommand(DeleteSong, CanDeleteSong);

            PrevCommand = new RelayCommand(Prev, CanPrevSong);
            NextCommand = new RelayCommand(Next, CanNextSong);
            Songs = new ObservableCollection<SongViewModel>(songList);
        }

        private void Next()
        {
            if (SelectedSong == Songs.Last())
            {
                SelectedSong = Songs.First();
            }
            else
            {
                SelectedSong = Songs.ElementAt(Songs.IndexOf(_selectedSong) + 1);
            }
        }

        private bool CanNextSong()
        {
            return Songs.Count > 0;
        }

        private void Prev()
        {
            if (SelectedSong == Songs.First())
            {
                SelectedSong = Songs.Last();
            }
            else
            {
                SelectedSong = Songs.ElementAt(Songs.IndexOf(_selectedSong) - 1);
            }
        }

        private bool CanPrevSong()
        {
            return Songs.Count > 0;
        }

        private void Playalong()
        {
            PlayalongWindow _playalongWindow = new PlayalongWindow();
            _playalongWindow.Show();
        }

        private bool CanPlayalong()
        {
            return SelectedSong != null;
        }

        private bool CanDeleteSong()
        {
            return Songs.Contains(SelectedSong);
        }

        private void DeleteSong()
        {
            Songs.Remove(SelectedSong);
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