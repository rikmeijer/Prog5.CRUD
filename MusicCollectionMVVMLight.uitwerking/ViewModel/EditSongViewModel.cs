using MusicCollectionMVVMMVVMLight.Model;
using System.Windows.Input;

namespace MusicCollectionMVVMLight.ViewModel
{
    public class EditSongViewModel
    {
        //Todo: Afmaken van deze klasse
        public SongViewModel Song { get; set; }

        public ICommand SaveCommand { get; set; }

        public EditSongViewModel(SongViewModel selectedSong)
        {
            this.Song = selectedSong;
        }

        public void Save()
        {

        }
    }
}