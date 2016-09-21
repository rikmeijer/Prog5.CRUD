﻿/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MusicCollectionMVVMLight.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MusicCollectionMVVMMVVMLight.Model;

namespace MusicCollectionMVVMLight.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Hier registreren we een SongListViewModel
            //Waarom doen we dit? Denk hier alvast over na.
            //Het antwoord op deze vraag komt over een tijdje.
            SimpleIoc.Default.Register<SongListViewModel>();
        }

        public SongListViewModel SongList
        {
            get
            {
                //De service locator gebruikt een 'singleton' patroon. 
                //Het maakt niet uit hoevaak je een SongList aanvraagt, je krijgt altijd het zelfde object terug. 
                return ServiceLocator.Current.GetInstance<SongListViewModel>();
            }
        }

        public AddSongViewModel AddSong
        {
            get
            {
                //De service locator gebruikt een 'singleton' patroon. 
                //Het maakt niet uit hoevaak je een SongList aanvraagt, je krijgt altijd het zelfde object terug. 
                return new AddSongViewModel(this.SongList);
            }
        }

        public EditViewModel UpdateSong
        {
            get
            {
                //Hmmm... Wat moeten we hier nu returnen? Denk er maar eens goed over na!
                return new EditViewModel(this.SongList.SelectedSong);
            }
        }

        public static void Cleanup()
        {
        }
    }
}