using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MusicCollectionMVVMMVVMLight.Model;

namespace MusicCollectionMVVMLight.ViewModel
{
    public class EditViewModel
    {
        public SongViewModel Song { get; set; }
        
        public ICommand EditSongCommand { get; set; }


        public EditViewModel(SongViewModel songViewModel)
        {
            this.Song = songViewModel;
        }
    }
}
